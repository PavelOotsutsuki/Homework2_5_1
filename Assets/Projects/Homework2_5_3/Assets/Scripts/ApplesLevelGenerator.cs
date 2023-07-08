using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ApplesLevelGenerator : MonoBehaviour
{
    [SerializeField] private int _count = 12;
    [SerializeField] private float _distance = 1f;
    [SerializeField] private Apple _template;

    private void OnEnable()
    {
        for (int i = 0; i < _count; i++)
        {
            float xPosition = transform.position.x + _distance * i;
            float yPosition = transform.position.y;
            Apple createdApple = Instantiate(_template, new Vector3(xPosition, yPosition, 0), Quaternion.identity, gameObject.transform);
        }
    }
}
