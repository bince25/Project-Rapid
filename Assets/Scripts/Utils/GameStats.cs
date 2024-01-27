using UnityEngine;

[System.Serializable]
public class GameStats
{
    public int numberOfBirths = 0;
    public int numberOfKilledFathers = 0;
    public float satisfactionLevel = GameConstants.DEFAULT_SATISFACTION_LEVEL; // You can start with a max value and decrease based on game events
    public float totalCryingDuration = 0f; // Total duration of all babies crying
    public int numberOfDiaperChanges = 0;
    public float passedTime = GameConstants.DEFAULT_TIMER_DURATION;

    public float score = 0f;

    public void CalculateFinalScore()
    {
        score += numberOfBirths * GameConstants.SCORE_PER_BIRTH;
        score += numberOfKilledFathers * GameConstants.SCORE_PER_KILLED_FATHER;
        score += satisfactionLevel * GameConstants.SCORE_PER_SATISFACTION_LEVEL;
        score -= totalCryingDuration * GameConstants.SCORE_PER_CRYING_DURATION;
        score += numberOfDiaperChanges * GameConstants.SCORE_PER_DIAPER_CHANGE;
        score -= passedTime * GameConstants.SCORE_PER_PASSED_TIME;
    }

    /// <summary>
    /// Updates the satisfaction level by the given amount.
    /// </summary>
    /// <param name="change"></param>
    public void UpdateSatisfaction(float change)
    {
        satisfactionLevel += change;
        satisfactionLevel = Mathf.Clamp(satisfactionLevel, 0, 100); // Keep satisfaction level within bounds
    }

    /// <summary>
    /// Updates the total crying duration by the given amount.
    /// </summary>
    /// <param name="change"></param>
    public void UpdateCryingDuration(float change)
    {
        totalCryingDuration += change;
    }

    /// <summary>
    /// Updates the number of diaper changes by the given amount.
    /// </summary>
    /// <param name="change"></param>
    public void UpdateDiaperChanges(int change)
    {
        numberOfDiaperChanges += change;
    }

    /// <summary>
    /// Updates the number of killed fathers by the given amount.
    /// </summary>
    /// <param name="change"></param>
    public void UpdateNumberOfKilledFathers(int change)
    {
        numberOfKilledFathers += change;
    }

    /// <summary>
    /// Updates the number of births by the given amount.
    /// </summary>
    /// <param name="change"></param>
    public void UpdateNumberOfBirths(int change)
    {
        numberOfBirths += change;
    }

    /// <summary>
    /// Resets all stats to their default values.
    /// </summary>
    public void ResetStats()
    {
        numberOfBirths = 0;
        numberOfKilledFathers = 0;
        satisfactionLevel = 100f;
        totalCryingDuration = 0f;
        numberOfDiaperChanges = 0;
    }
}
