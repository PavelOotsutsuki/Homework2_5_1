using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]

public class Finish : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public bool _isEnable;

    [SerializeField] private UnityEvent _endGame = new UnityEvent();
    [SerializeField] private UnityEvent _colorSetter = new UnityEvent();

    private void Start()
    {
        _isEnable = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_isEnable)
        {
            _colorSetter?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && _isEnable)
        {
            _endGame?.Invoke();
            Debug.Log("Success!");
        }
    }

    public void OnIsEnable()
    {
        _isEnable = true;
    }
}
