using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AlarmActions))]

public class AlarmTrigger : MonoBehaviour
{
    private AlarmActions _alarmActions;
    private bool _isReached;

    private void Start()
    {
        _isReached = false;
        _alarmActions = GetComponent<AlarmActions>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_isReached)
        {
            return;
        }

        if (collider.TryGetComponent(out Swindler swindler))
        {
            _isReached = true;
            _alarmActions.StartUpVolume();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (_isReached==false)
        {
            return;
        }

        if (collider.TryGetComponent(out Swindler swindler))
        {
            _isReached = false;
            _alarmActions.StartDownVolume();
        }
    }
}
