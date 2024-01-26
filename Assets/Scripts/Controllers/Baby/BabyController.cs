using UnityEngine;

public class BabyController : MonoBehaviour
{
    public int OriginalIncubatorId = -1;

    /// <summary>
    /// Called when the baby is placed in an incubator.
    /// </summary>
    public void OnPlacedInIncubator()
    {
        Debug.Log("Baby is now in the incubator.");
    }
}
