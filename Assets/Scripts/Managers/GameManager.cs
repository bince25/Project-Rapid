using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TimerManager TimerManager;
    public GameStats stats = new GameStats();
    public BarManager barManager;
    public ScoreboardManager scoreboardManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple GameManagers in the scene");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TimerManager.StartTimer();

        AudioManager.Instance.PlayMusic(MusicTrack.BackgroundMusic, true, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        TimerManager.StartTimer();
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        stats.timeRemaining = TimerManager.GetTimeRemaining();
        TimerManager.StopTimer();
        if (scoreboardManager != null)
        {
            scoreboardManager.SetGameStats(stats);
            scoreboardManager.OpenScoreboard();
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Updates the satisfaction level by the given value
    /// </summary>
    /// <param name="value"></param>
    public void UpdateSatisfactionLevel(float value)
    {
        stats.UpdateSatisfaction(value);
        barManager.SetSliderValue(stats.satisfactionLevel);
    }

    /// <summary>
    /// Increases the satisfaction level by the given value
    /// </summary>
    /// <param name="value"></param>
    public void IncreaseSatisfactionLevel(float value)
    {
        stats.UpdateSatisfaction(value);
        barManager.SetSliderValue(stats.satisfactionLevel);
    }

    /// <summary>
    /// Decreases the satisfaction level by the given value
    /// </summary>
    /// <param name="value"></param>
    public void DecreaseSatisfactionLevel(float value)
    {
        stats.UpdateSatisfaction(-value);
        barManager.SetSliderValue(stats.satisfactionLevel);
    }

    /// <summary>
    /// Records the birth of the baby
    /// </summary>
    public void RecordBirth()
    {
        stats.numberOfBirths++;
    }

    /// <summary>
    /// Records the death of the baby
    /// </summary>
    public void RecordFatherDeath()
    {
        stats.numberOfKilledFathers++;
    }

    /// <summary>
    /// Records the duration of the crying of the baby
    /// </summary>
    public void RecordDiaperChange()
    {
        stats.numberOfDiaperChanges++;
    }

    /// <summary>
    /// Records the duration of the crying of the baby
    /// </summary>
    /// <param name="cryDuration"></param>
    public void RecordCryDuration(float cryDuration)
    {
        stats.UpdateCryingDuration(cryDuration);
    }
}
