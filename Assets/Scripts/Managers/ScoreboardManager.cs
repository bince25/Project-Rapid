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
    }

    public void OpenScoreboard()
    {
        scoreboardPanel.SetActive(true);
    }

    public void CloseScoreboard()
    {
        scoreboardPanel.SetActive(false);
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
        timeRemainingText.text = $"{Mathf.FloorToInt(gameStats.timeRemaining / 60):00}:{Mathf.FloorToInt(gameStats.timeRemaining % 60):00}";
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