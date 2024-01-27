using TMPro;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{

    private GameStats gameStats = new GameStats();

    [SerializeField]
    private GameObject scoreboardPanel;
    [SerializeField]
    private TMP_Text birthCountText;
    [SerializeField]
    private TMP_Text deathFatherCountText;
    [SerializeField]
    private TMP_Text satisfactionLevelText;
    [SerializeField]
    private TMP_Text timeRemainingText;
    [SerializeField]
    private TMP_Text cryDurationText;
    [SerializeField]
    private TMP_Text diaperCountText;
    [SerializeField]
    private TMP_Text finalScoreText;
    [SerializeField]
    private TMP_Text finalStatusText;


    private void Start()
    {
        SetTexts();
    }

    private void Update()
    {
    }

    public void SetGameStats(GameStats stats)
    {
        gameStats = stats;
        gameStats.CalculateFinalScore();
        SetTexts();
    }

    public void SetTexts()
    {
        SetBirthCount();
        SetDeathFatherCount();
        SetSatisfactionLevel();
        SetTimeRemaining();
        SetCryDuration();
        SetDiaperCount();
        SetFinalScore();
        SetFinalStatus();
    }

    public void OpenScoreboard()
    {
        scoreboardPanel.SetActive(true);
    }

    public void CloseScoreboard()
    {
        scoreboardPanel.SetActive(false);
    }

    public void SetFinalScore()
    {
        finalScoreText.text = gameStats.score.ToString();
    }

    public void SetFinalStatus()
    {
        if (gameStats.score >= 300)
        {
            finalStatusText.text = "Are you sure that you are not a human?";
        }
        else if (gameStats.score >= 150)
        {
            finalStatusText.text = "You should do better!";
        }
        else
        {
            finalStatusText.text = "You are not a slave anymore, you will be killed :)";
        }
    }

    public void SetBirthCount()
    {
        birthCountText.text = gameStats.numberOfBirths.ToString();
    }

    public void SetDeathFatherCount()
    {
        deathFatherCountText.text = gameStats.numberOfKilledFathers.ToString();
    }

    public void SetSatisfactionLevel()
    {
        satisfactionLevelText.text = gameStats.satisfactionLevel.ToString() + " %";
    }

    public void SetTimeRemaining()
    {
        timeRemainingText.text = $"{Mathf.FloorToInt(gameStats.passedTime / 60):00}:{Mathf.FloorToInt(gameStats.passedTime % 60):00}";
    }

    public void SetCryDuration()
    {

        cryDurationText.text = ((int)gameStats.totalCryingDuration).ToString() + " seconds";
    }

    public void SetDiaperCount()
    {
        diaperCountText.text = gameStats.numberOfDiaperChanges.ToString();
    }
}