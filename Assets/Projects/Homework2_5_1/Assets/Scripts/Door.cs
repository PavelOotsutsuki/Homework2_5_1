using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Door : MonoBehaviour
{
    private readonly float _openedDoorScaleCoefficient = 1.4656f;

    private bool _isOpen;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _isOpen = false;
        _boxCollider = GetComponent<BoxCollider2D>();
    }
     
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out PersonMovement personMovement) && _isOpen == false)
        {
            transform.localScale = new Vector3(Modul(transform.localScale.y / _openedDoorScaleCoefficient), transform.localScale.y);
            transform.Translate(transform.localScale.x * _boxCollider.size.x * -1 / 2 , 0, 0);
            _boxCollider.enabled = false;
            _isOpen = true;

        }
    }

    private float Modul(float number)
    {
        if (number < 0)
        {
            return number * -1;
        }

        return number;
    }

}
