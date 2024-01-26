using UnityEngine;

public class IncubatorController : MonoBehaviour
{
    public GameObject storedBaby = null; // Reference to the stored baby
    public int incubatorId; // Unique identifier for each incubator

    public bool TryPlaceBaby(GameObject baby)
    {
        BabyController babyController = baby.GetComponent<BabyController>();

        if (storedBaby == null && (babyController.OriginalIncubatorId == incubatorId || babyController.OriginalIncubatorId == -1))
        {
            storedBaby = baby;
            baby.transform.SetParent(transform);
            baby.SetActive(false); // Hide the baby when in the incubator

            // Set the baby's original incubator ID if it's not already set
            if (babyController.OriginalIncubatorId == -1)
            {
                babyController.OriginalIncubatorId = incubatorId;
            }

            // Sound effect: Baby placed
            Debug.Log("Baby placed successfully.");
            return true;
        }
        else
        {
            // Sound effect: Error or denied
            Debug.Log("Incubator is already occupied or does not match the baby's original incubator.");
            return false;
        }
    }

    public GameObject PickUpBaby()
    {
        if (storedBaby != null)
        {
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
