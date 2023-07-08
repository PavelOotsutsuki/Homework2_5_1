using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    public bool IsReached { get; private set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsReached)
        {
            return;
        }

        if (collider.TryGetComponent(out Swindler swindler))
        {
            IsReached = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (IsReached==false)
        {
            return;
        }

        if (collider.TryGetComponent(out Swindler swindler))
        {
            IsReached = false;
        }
    }
}
