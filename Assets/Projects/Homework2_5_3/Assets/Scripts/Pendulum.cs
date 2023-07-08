using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _rotationValue = 0;

    private void FixedUpdate()
    {
        _rotationValue++;
        _rotationValue %= 360 / _speed;

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _rotationValue * _speed);
    }
}
