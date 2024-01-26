using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PregnantController : MonoBehaviour
{
    private GameObject notification;
    public bool isActivated = false;
    private bool playerIsNear = false;

    void Update()
    {
        if (playerIsNear && isActivated && Input.GetKeyDown(KeyCode.E))
        {
            // Open Mini-Game
            Deactivate();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsNear = false;
        }
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

