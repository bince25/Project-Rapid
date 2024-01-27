using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PregnantIndicatorController : MonoBehaviour
{
    public Transform right;
    public Transform left;
    public Transform center;
    public GameObject vfx;
    public float speed = 1.0f;

    private float t = 0.0f; // A parameter that goes from 0 to 1
    private bool movingToLeft = true; // Direction of movement
    private bool isMoving = true; // Flag to control movement

    void Update()
    {
        switch (PregnantManager.Instance.isPlayer2)
        {
            case true:
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    isMoving = !isMoving;
                    Hit();
                }
                break;
            case false:
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    isMoving = !isMoving;
                    Hit();
                }
                break;
        }
        if (isMoving)
        {
            Move();
        }
    }

    void Move()
    {
        if (movingToLeft)
        {
            t += speed * Time.deltaTime;
            if (t > 1.0f)
            {
                t = 1.0f;
                movingToLeft = false; // Switch direction
            }
        }
        else
        {
            t -= speed * Time.deltaTime;
            if (t < 0.0f)
            {
                t = 0.0f;
                movingToLeft = true; // Switch direction
            }
        }

        transform.position = Vector3.Lerp(right.position, left.position, t);
    }

    void Hit()
    {
        AudioManager.Instance.PlaySFX(SFX.CutCord);
        GameObject vfxInstance = Instantiate(vfx, transform.position, Quaternion.identity);
        StartCoroutine(WaitAndCalculateScore(vfxInstance));
    }
    IEnumerator WaitAndCalculateScore(GameObject vfxInstance)
    {
        // Check if the VFX has a Particle System and get its duration
        if (vfxInstance.TryGetComponent(out ParticleSystem particleSystem))
        {
            yield return new WaitForSeconds(particleSystem.main.duration);
        }
        else // If not a Particle System, use a default wait time or animation clip duration
        {
            // Replace this with the actual duration of your VFX if it's not a particle system
            yield return new WaitForSeconds(1.0f);
        }

        // Now call CalculateScore
        CalculateScore();
    }
    void CalculateScore()
    {
        float distance = Vector3.Distance(transform.position, center.position);
        int score = Mathf.Max(0, 100 - Mathf.RoundToInt(distance * 10));
        Debug.Log("Score: " + score);
        PregnantManager.Instance.GiveBirthToBabyAndDestroyPregnant();
        isMoving = true;
    }
}
