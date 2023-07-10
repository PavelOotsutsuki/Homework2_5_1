using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplesGenerator : MonoBehaviour
{
    [SerializeField] private Finish _finish;

    private ApplesLevelGenerator[] _applesLevelGenerators;
    private int _counter;

    private void Start()
    {
        _counter = 0;
        _applesLevelGenerators = GetComponentsInChildren<ApplesLevelGenerator>();
        _applesLevelGenerators[_counter++].enabled = true;
    }

    public void CheckLastApple()
    {
        int applesCount = GetComponentsInChildren<Apple>().Length;

        if (_counter < _applesLevelGenerators.Length)
        {
            if (applesCount <= 1)
            {
                _applesLevelGenerators[_counter++].enabled = true;
            }
        }
        else if (applesCount <= 1)
        {
            _finish.OnIsEnable();
        }
    }
}
