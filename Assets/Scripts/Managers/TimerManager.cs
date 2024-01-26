using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    public TMPro.TextMeshProUGUI TimerText;

    private float _timeRemaining = 0;
    private bool _timerIsRunning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (_timerIsRunning)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                if (_timeRemaining < 0)
                {
                    _timeRemaining = 0;
                }
                if (TimerText != null)
                {
                    TimerText.text = $"{Mathf.FloorToInt(_timeRemaining / 60):00}:{Mathf.FloorToInt(_timeRemaining % 60):00}";
                }
            }
            else
            {
                _timeRemaining = 0;
                _timerIsRunning = false;
            }
        }
    }

    /// <summary>
    /// Starts the timer with a default time of 180 seconds (3 minutes)
    /// </summary>
    public void StartTimer()
    {
        _timeRemaining = 180;
        _timerIsRunning = true;
    }

    /// <summary>
    /// Starts the timer with a custom time
    /// </summary>
    /// <param name="time">The time to start the timer with</param>
    /// <example>
    /// This sample shows how to call the <see cref="StartTimer(float)"/> method.
    /// <code>
    /// TimerManager.Instance.StartTimer(60);
    /// </code>
    /// </example>
    public void StartTimer(float time)
    {
        _timeRemaining = time;
        _timerIsRunning = true;
    }

    /// <summary>
    /// Stops the timer
    /// </summary>
    public void StopTimer()
    {
        _timerIsRunning = false;
    }

    /// <summary>
    /// Returns the time remaining on the timer
    /// </summary>
    public float GetTimeRemaining()
    {
        return _timeRemaining;
    }
}