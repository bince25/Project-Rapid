using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    public TMPro.TextMeshProUGUI TimerText;

    private float _timePassed = 0f; // Time passed since the timer started
    private bool _timerIsRunning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (_timerIsRunning)
        {
            _timePassed += Time.deltaTime;

            // Update timer display
            if (TimerText != null)
            {
                TimerText.text = $"{Mathf.FloorToInt(_timePassed / 60):00}:{Mathf.FloorToInt(_timePassed % 60):00}";
            }

            // Additional logic based on time passed can be added here
            // For example, trigger an event when the timePassed reaches a certain threshold
        }
    }

    /// <summary>
    /// Starts or resumes the timer.
    /// </summary>
    public void StartOrResumeTimer()
    {
        _timerIsRunning = true;
    }

    /// <summary>
    /// Stops the timer.
    /// </summary>
    public void StopTimer()
    {
        _timerIsRunning = false;
    }

    /// <summary>
    /// Resets the timer to zero.
    /// </summary>
    public void ResetTimer()
    {
        _timePassed = 0f;
    }

    /// <summary>
    /// Gets the time passed since the timer started.
    /// </summary>
    public float GetTimePassed()
    {
        return _timePassed;
    }
}
