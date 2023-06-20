using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached = new UnityEvent();
    [SerializeField] private UnityEvent _gone = new UnityEvent();

    private float _countTimeVolume;
    private AudioSource _audioSource;
    private float _startVolume;
    private float _maxVolume;
    private Coroutine _upVolumeInJob;
    private Coroutine _downVolumeInJob;
    private float _duration;

    public bool IsReached { get; private set; }

    private void Start()
    {
        _countTimeVolume = 0;
        _audioSource = GetComponent<AudioSource>();
        _startVolume = 0;
        _maxVolume = 1f;
        _duration = 10;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsReached)
        {
            return;
        }

        if (collider.TryGetComponent(out Swindler swindler))
        {
            IsReached = true;
            _reached?.Invoke();

            if (_downVolumeInJob != null)
            {
                StopCoroutine(_downVolumeInJob);
            }

            _upVolumeInJob =StartCoroutine(UpVolume(_duration));
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

            if (_upVolumeInJob != null)
            {
                StopCoroutine(_upVolumeInJob);
            }

            _downVolumeInJob = StartCoroutine(DownVolume(_duration));
        }
    }

    private IEnumerator UpVolume(float duration)
    {
        for (float i = _countTimeVolume; i < duration; i += Time.deltaTime)
        {
            _countTimeVolume = i + Time.deltaTime;

            if (_countTimeVolume<0)
            {
                _countTimeVolume = 0;
            }

            if (_countTimeVolume > duration)
            {
                _countTimeVolume = duration;
            }

            _audioSource.volume = Mathf.MoveTowards(_startVolume, _maxVolume, i / duration);

            yield return true;
        }

        _audioSource.volume = _maxVolume;
    }

    private IEnumerator DownVolume(float duration)
    {
        for (float i = _countTimeVolume; i > 0; i -= Time.deltaTime)
        {
            _countTimeVolume = i - Time.deltaTime;

            if (_countTimeVolume < 0)
            {
                _countTimeVolume = 0;
            }

            if (_countTimeVolume > duration)
            {
                _countTimeVolume = duration;
            }

            _audioSource.volume = Mathf.MoveTowards(_startVolume, _maxVolume, i / duration);

            yield return true;
        }

        _audioSource.volume = _startVolume;
        _gone?.Invoke();
    }
}
