using UnityEngine;

public class BabyController : MonoBehaviour
{
    public int OriginalIncubatorId = -1;
    public bool isCrying = false;
    private GameObject notification;
    /// <summary>
    /// Called when the baby is placed in an incubator.
    /// </summary>

    void Start()
    {
        notification = this.transform.GetChild(0).gameObject;
    }
    public void OnPlacedInIncubator()
    {
        Debug.Log("Baby is now in the incubator.");
    }

    public void Cry()
    {
        Debug.Log("The baby is crying!");
        notification.SetActive(true);
        isCrying = true;
    }

}
