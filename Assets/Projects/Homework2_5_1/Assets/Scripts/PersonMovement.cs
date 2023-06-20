using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class PersonMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Sprite _rightMovementSprite;
    [SerializeField] private Sprite _leftMovementSprite;

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _renderer.sprite = _rightMovementSprite;
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _renderer.sprite = _leftMovementSprite;
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
        }
    }
}
