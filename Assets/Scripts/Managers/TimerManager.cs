using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    public TMPro.TextMeshProUGUI TimerText;

    private float _timeRemaining = 0;
    private bool _timerIsRunning = false;
    private bool _tickTockStarted = false;

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

                if (_timeRemaining <= 10 && !_tickTockStarted)
                {
                    AudioManager.Instance.PlayMusic(MusicTrack.TickTock, true, 1);
                    _tickTockStarted = true;
                }
                if (_timeRemaining > 10 && _tickTockStarted)
                {
                    AudioManager.Instance.StopMusic(MusicTrack.TickTock);
                    _tickTockStarted = false;
                }

                if (_timeRemaining <= 0)
                {
                    AudioManager.Instance.StopMusic(MusicTrack.TickTock);
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
        _timeRemaining = GameConstants.DEFAULT_TIMER_DURATION;
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

    /// <summary>
    /// Adds time to the timer
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public float AddTime(float time)
    {
        _timeRemaining += time;
        return _timeRemaining;
    }

    /// <summary>
    /// Subtracts time from the timer
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public float SubtractTime(float time)
    {
        _timeRemaining -= time;
        return _timeRemaining;
    }
}