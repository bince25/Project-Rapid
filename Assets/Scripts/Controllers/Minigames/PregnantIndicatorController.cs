using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PregnantIndicatorController : MonoBehaviour
{
    public Transform right;
    public Transform left;
    public Transform center;
    public float speed = 1.0f;

    private float t = 0.0f; // A parameter that goes from 0 to 1
    private bool movingToLeft = true; // Direction of movement
    private bool isMoving = true; // Flag to control movement

    void Update()
    {
        // Toggle movement when space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = !isMoving;
            CalculateScore();
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
    void CalculateScore()
    {
        float distance = Vector3.Distance(transform.position, center.position);
        int score = Mathf.Max(0, 100 - Mathf.RoundToInt(distance * 10));
        Debug.Log("Score: " + score);
    }
}
