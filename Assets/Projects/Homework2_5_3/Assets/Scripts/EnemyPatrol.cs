using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class EnemyPatrol : MonoBehaviour
{
    private const float MinMoveDistance = 0.001f;
    private const float ShellRadius = 0.01f;

    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _duration;

    private bool _flipX;
    private Vector2 _targetVelocity;
    private Vector2 _groundNormal;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    private float _moveHorizontal;
    private float _moveDuration = 1f;
    private bool _isRightMove;
    private float _moveTime;

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;

        _moveTime = _moveDuration / 2;
        _isRightMove = true;
    }

    private void Update()
    {
        //if (_isRightMove)
        //{
        //    _moveHorizontal = 1;

        //    if (_moveTime < _moveDuration)
        //    {
        //        _moveTime += Time.deltaTime;
        //    }
        //    else
        //    {
        //        _moveTime = 0;
        //        _isRightMove = false;
        //    }
        //}
        //else
        //{
        //    _moveHorizontal = -1;

        //    if (_moveTime < _moveDuration)
        //    {
        //        _moveTime += Time.deltaTime;
        //    }
        //    else
        //    {
        //        _moveTime = 0;
        //        _isRightMove = true;
        //    }
        //}
        DefineMovementPosition();

        float moveX = _moveHorizontal * _duration;

        _targetVelocity = new Vector2(moveX, 0);

        if (moveX > 0)
        {
            _flipX = false;
        }
        else if (moveX < 0)
        {
            _flipX = true;
        }
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);

        _spriteRenderer.flipX = _flipX;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Destroy(player.gameObject);
            Debug.Log("You lose");
        }
    }

    private void DefineMovementPosition()
    {
        _moveHorizontal = Convert.ToInt32(_isRightMove) * 2 - 1;

        if (_moveTime < _moveDuration)
        {
            _moveTime += Time.deltaTime;
        }
        else
        {
            _moveTime = 0;
            _isRightMove = _isRightMove == false;
        }
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = _rigidbody2D.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;
                if (currentNormal.y > _minGroundNormalY)
                {
                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);

                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidbody2D.position = _rigidbody2D.position + move.normalized * distance;
    }
}
