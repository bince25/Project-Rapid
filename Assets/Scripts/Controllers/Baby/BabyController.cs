using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BabyController : MonoBehaviour
{
    private Coroutine incrementCoroutine;

    public int OriginalIncubatorId = -1;
    [SerializeField] private int babyIndex;
    public bool isCrying = false;
    public GameObject incubator;
    private GameObject notification;
    private float cryTimer = 0f;
    public bool isIncubated = false;
    public bool readyToBeGivenToDad = false;
    public Slider slider;
    public int maturityLevel = 0;

    void Start()
    {
        notification = this.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (isCrying)
        {
            cryTimer += Time.deltaTime;
            if (cryTimer >= 10f)
            {
                StopCrying();
                cryTimer = 0f;
                GameManager.Instance.DecreaseSatisfactionLevel(5f);
            }
        }
        if (maturityLevel >= 100)
        {
            maturityLevel = 100;
            StopCoroutine(IncrementNumber());
            readyToBeGivenToDad = true;
        }

    }

    public void OnPlacedInIncubator()
    {
        if (incrementCoroutine != null)
        {
            StopCoroutine(incrementCoroutine);
        }
        incrementCoroutine = StartCoroutine(IncrementNumber());
    }
    public void OnTakenFromIncubator()
    {
        if (incrementCoroutine != null)
        {
            StopCoroutine(incrementCoroutine);
            incrementCoroutine = null;
        }
    }
    private IEnumerator IncrementNumber()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); // Wait for 1 second
            maturityLevel += 5; // Increment the number
            slider.value = maturityLevel;
        }

    }

    public void setIndex(int babyIndex)
    {
        this.babyIndex = babyIndex;
    }
    public int getIndex()
    {
        return babyIndex;
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
