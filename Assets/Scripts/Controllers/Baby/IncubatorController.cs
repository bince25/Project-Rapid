using System.Collections;
using UnityEngine;

public class IncubatorController : MonoBehaviour
{
    public GameObject storedBaby = null; // Reference to the stored baby
    public int incubatorId; // Unique identifier for each incubator
    private bool incubatorTaken = false;

    /// <summary>
    /// Tries to place a baby in the incubator.
    /// </summary>
    /// <param name="baby"></param>
    /// <returns></returns>
    public bool TryPlaceBaby(GameObject baby)
    {
        BabyController babyController = baby.GetComponent<BabyController>();

        if (storedBaby == null && (babyController.OriginalIncubatorId == incubatorId || (!incubatorTaken && babyController.OriginalIncubatorId == -1)))
        {
            AudioManager.Instance.PlaySFX(SFX.PlaceBaby);
            incubatorTaken = true;
            storedBaby = baby;
            baby.transform.SetParent(transform);
            baby.transform.localPosition = Vector3.zero;
            //baby.SetActive(false); // Hide the baby when in the incubator

            // Set the baby's original incubator ID if it's not already set
            if (babyController.OriginalIncubatorId == -1)
            {
                babyController.OriginalIncubatorId = incubatorId;
            }

            // Sound effect: Baby placed

            if (storedBaby != null)
            {
                Debug.Log("Baby placed successfully.");
                StartBabyTimer(storedBaby);
            }
            return true;
        }
        else
        {
            // Sound effect: Error or denied
            Debug.Log("Incubator is already occupied or does not match the baby's original incubator.");
            return false;
        }
    }

    /// <summary>
    /// Starts the timer for the baby to cry.
    /// </summary>
    /// <param name="baby"></param>
    public void StartBabyTimer(GameObject baby)
    {
        StartCoroutine(BabyTimer(baby, 5f)); // 10 seconds timer
    }

    /// <summary>
    /// The timer for the baby to cry.
    /// </summary>
    /// <param name="baby"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator BabyTimer(GameObject baby, float time)
    {
        yield return new WaitForSeconds(time);

        // Trigger the baby's cry or any other action here
        baby.GetComponent<BabyController>().Cry();
        Debug.Log("The baby is crying!");
    }

    /// <summary>
    /// Picks up the baby from the incubator.
    /// </summary>
    /// <returns></returns>
    public GameObject PickUpBaby()
    {
        if (storedBaby != null)
        {
            AudioManager.Instance.PlaySFX(SFX.PickUpBaby);
            GameObject baby = storedBaby;
            baby.SetActive(true); // Reactivate the baby when picked up
            storedBaby = null; // Clear the stored reference

            // Sound effect: Baby picked up
            Debug.Log("Baby picked up from incubator.");
            return baby;
        }
        return null;
    }
}
