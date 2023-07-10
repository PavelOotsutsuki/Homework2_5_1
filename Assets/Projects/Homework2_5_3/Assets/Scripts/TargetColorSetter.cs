using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class TargetColorSetter : MonoBehaviour
{
    [SerializeField] private Color _targetColor = new Color(255, 255, 255, 255);

    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Change()
    {
        _renderer.color = _targetColor;
    }
}
