using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DadController : MonoBehaviour
{
    [SerializeField] private int dadIndex;
    public GameObject notificationHappy;
    public GameObject notificationSad;
    private Animator happyAnimator;
    private Animator sadAnimator;
    private bool isTalkable = false;

    void Start()
    {
        // Assuming both notifications have an Animator component
        happyAnimator = notificationHappy.GetComponent<Animator>();
        sadAnimator = notificationSad.GetComponent<Animator>();
        StartCoroutine(TalkTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator TalkTimer()
    {
        float time = Random.Range(5f, 10f);
        yield return new WaitForSeconds(time);
        isTalkable = true;
    }
    public void KillOrTalk(GameObject player)
    {
        if (isTalkable)
        {
            ChoiceManager.Instance.Activate(this.gameObject, player);
        }
    }

    public void setDadIndex(int dadIndex)
    {
        this.dadIndex = dadIndex;
    }
    public int getDadIndex()
    {
        return dadIndex;
    }
    public bool GiveBabyToDad(GameObject heldBaby)
    {
        if (heldBaby == null)
        {
            Debug.Log("No Baby Held");
            return false;
        }
        if (heldBaby.GetComponent<BabyController>().readyToBeGivenToDad)
        {
            if (dadIndex == heldBaby.GetComponent<BabyController>().getIndex())
            {
                Debug.Log("Baby Given to Dad");
                notificationHappy.SetActive(true);
                StartCoroutine(DeactivateAfterAnimation(notificationHappy, happyAnimator.GetCurrentAnimatorStateInfo(0).length));
                heldBaby.GetComponent<BabyController>().incubator.GetComponent<IncubatorController>().SetIncubatorTaken(false);
                PregnantManager.Instance.dadSpawnPointsDictionary[dadIndex] = false;
                Destroy(heldBaby);
                Destroy(this.gameObject);
                return true;
            }
            else
            {
                Debug.Log("Baby Given to Wrong Dad");
                notificationSad.SetActive(true);
                StartCoroutine(DeactivateAfterAnimation(notificationSad, sadAnimator.GetCurrentAnimatorStateInfo(0).length));
                return false;
            }
        }
        else
        {
            Debug.Log("Baby Not Ready to be Given to Dad");
            notificationSad.SetActive(true);
            StartCoroutine(DeactivateAfterAnimation(notificationSad, sadAnimator.GetCurrentAnimatorStateInfo(0).length));
            return false;
        }
    }
    private IEnumerator DeactivateAfterAnimation(GameObject notification, float delay)
    {
        yield return new WaitForSeconds(delay);
        notification.SetActive(false);
    }
}
