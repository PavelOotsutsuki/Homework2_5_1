using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Apple : MonoBehaviour
{
    private ApplesGenerator _applesGenerator;

    private void Start()
    {
        _applesGenerator = GetComponentInParent<ApplesGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.TryGetComponent(out Player player))
        {
            _applesGenerator.CheckLastApple();
            Destroy(gameObject);
        }
    }
}
