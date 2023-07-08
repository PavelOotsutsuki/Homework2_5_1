using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Finish : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    public bool IsEnable { get; set; }
    public bool IsSuccess { get; private set; }

    private void Start()
    {
        IsEnable = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (IsEnable)
        {
            _spriteRenderer.color = new Color(255, 255, 255, 255);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && IsEnable)
        {
            IsSuccess = true;
            Debug.Log("Success!");
        }
    }
}
