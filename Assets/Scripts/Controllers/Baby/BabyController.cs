using UnityEngine;

public class BabyController : MonoBehaviour
{
    public int OriginalIncubatorId = -1;
    [SerializeField] private int babyIndex;
    public bool isCrying = false;
    public GameObject incubator;
    private GameObject notification;
    /// <summary>
    /// Called when the baby is placed in an incubator.
    /// </summary>

    void Start()
    {
        notification = this.transform.GetChild(0).gameObject;
    }

    public void setIndex(int babyIndex)
    {
        this.babyIndex = babyIndex;
    }
    public int getIndex()
    {
        return babyIndex;
    }
    public void OnPlacedInIncubator()
    {
        Debug.Log("Baby is now in the incubator.");
    }

    public void Cry()
    {
        AudioManager.Instance.PlaySFX(SFX.BasicNotification);
        Debug.Log("The baby is crying!");
        notification.SetActive(true);
        isCrying = true;
    }

    public void StopCrying()
    {
        Debug.Log("The baby stopped crying!");
        notification.SetActive(false);
        isCrying = false;
    }

}
