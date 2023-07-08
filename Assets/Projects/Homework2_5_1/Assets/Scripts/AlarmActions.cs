using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AlarmTrigger))]

public class AlarmActions : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached = new UnityEvent();
    [SerializeField] private UnityEvent _gone = new UnityEvent();

    private float _countTimeVolume;
    private AudioSource _audioSource;
    private float _startVolume;
    private float _maxVolume;
    private Coroutine _volumeInJob;
    private float _duration;
    private AlarmTrigger _alarmTrigger;
    private bool _lastReached;

    private void Start()
    {
        _countTimeVolume = 0;
        _audioSource = GetComponent<AudioSource>();
        _alarmTrigger = GetComponent<AlarmTrigger>();
        _startVolume = 0;
        _maxVolume = 1f;
        _duration = 10;
        _lastReached = false;
    }

    private void Update()
    {
        if (_alarmTrigger.IsReached == true && _lastReached != _alarmTrigger.IsReached)
        {
            _reached?.Invoke();

            if (_volumeInJob != null)
            {
                StopCoroutine(_volumeInJob);
            }

            _lastReached = _alarmTrigger.IsReached;
            _volumeInJob = StartCoroutine(UpVolume());
        }

        if (_alarmTrigger.IsReached == false && _lastReached != _alarmTrigger.IsReached)
        {
            if (_volumeInJob != null)
            {
                StopCoroutine(_volumeInJob);
            }

            _lastReached = _alarmTrigger.IsReached;
            _volumeInJob = StartCoroutine(DownVolume());
        }
    }

    private IEnumerator UpVolume()
    {
        for (float i = _countTimeVolume; i < _duration; i += Time.deltaTime)
        {
            _countTimeVolume = i + Time.deltaTime;

            ChangeVolume(i);

            yield return true;
        }

        _audioSource.volume = _maxVolume;
    }

    private IEnumerator DownVolume()
    {
        for (float i = _countTimeVolume; i > 0; i -= Time.deltaTime)
        {
            _countTimeVolume = i - Time.deltaTime;

            ChangeVolume(i);

            yield return true;
        }

        _audioSource.volume = _startVolume;
        _gone?.Invoke();
    }

    private void ChangeVolume(float countTimeVolume)
    {
        if (_countTimeVolume < 0)
        {
            _countTimeVolume = 0;
        }

        if (_countTimeVolume > _duration)
        {
            _countTimeVolume = _duration;
        }

        _audioSource.volume = Mathf.MoveTowards(_startVolume, _maxVolume, countTimeVolume / _duration);
    }
}
