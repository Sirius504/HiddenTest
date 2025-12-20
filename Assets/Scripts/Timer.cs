using System;
using UnityEngine;
using VContainer;

public class Timer : MonoBehaviour
{
    [Inject] private LevelSettings _levelSettings;

    private float _startTime;
    private bool _isRunning;

    public float TotalTime => _levelSettings.TimerInSeconds;
    public float TimeRemaining => Math.Max(0f, _levelSettings.TimerInSeconds - (Time.time - _startTime));

    public bool IsRunning => _isRunning;

    public event Action OnElapsed;

    public void TimerStart()
    {
        _startTime = Time.time;
        _isRunning = true;
    }

    private void Update()
    {
        if (!_isRunning)
        {
            return;
        }

        if (TimeRemaining > 0f)
        {
            return;
        }

        OnElapsed?.Invoke();
        _isRunning = false;
    }
}