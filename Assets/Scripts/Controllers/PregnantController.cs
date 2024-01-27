using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PregnantController : MonoBehaviour
{
    private GameObject notification;
    public bool isActivated = false;
    private bool playerIsNear = false;
    private bool babyIsBorn = false;
    private bool gaveBirth = false;
    private bool isPlayer2;

    [SerializeField] private GameObject babyPrefab;
    [SerializeField] private GameObject baby;


    void Update()
    {
        if (playerIsNear && !gaveBirth && isActivated)
        {
            switch (isPlayer2)
            {
                case true:
                    if (Input.GetKeyDown(KeyCode.RightShift))
                    {
                        // Open Mini-Game
                        PregnantManager.Instance.ActivateMiniGame(isPlayer2);
                    }
                    break;
                case false:
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        // Open Mini-Game
                        PregnantManager.Instance.ActivateMiniGame(isPlayer2);
                    }
                    break;
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsNear = true;
            if (other.gameObject.GetComponent<PlayerController>().isPlayer2)
            {
                isPlayer2 = true;
            }
            else
            {
                isPlayer2 = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

    public void GiveBirth()
    {
        if (babyIsBorn)
        {
            return;
        }
        gaveBirth = true;
        baby = Instantiate(babyPrefab, this.transform.position, Quaternion.identity);
        babyIsBorn = true;
    }

    public GameObject TakeBaby()
    {
        if (!babyIsBorn)
        {
            return null;
        }
        babyIsBorn = false;
        this.Deactivate();
        return baby;
    }

    public void Activate()
    {
        if (isActivated)
        {
            return;
        }
        float time = Random.Range(3f, 5f);
        StartCoroutine(ActivateAfterSeconds(time));
    }

    IEnumerator ActivateAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        notification = this.transform.GetChild(0).gameObject;
        notification.SetActive(true);
        isActivated = true;
    }

    public void Deactivate()
    {
        if (!isActivated)
        {
            return;
        }
        Destroy(this.gameObject);
        isActivated = false;
    }
}

