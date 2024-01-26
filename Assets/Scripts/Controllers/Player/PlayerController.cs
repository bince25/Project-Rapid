using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float moveSpeed = 10f;
     protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

    }

    protected virtual void Start()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }
    /// <summary>
    /// Moves the player based on the input
    /// </summary>
    protected virtual void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis(GetHorizontalInput()), Input.GetAxis(GetVerticalInput()));
        Vector2 moveVelocity = moveInput.normalized * moveSpeed;
        rb.velocity = moveVelocity;
    }
    /// <summary>
    /// Gets the horizontal input axis
    /// </summary>
    protected virtual string GetHorizontalInput()
    {
        return "Horizontal";
    }

    /// <summary>
    /// Gets the vertical input axis
    /// </summary>
    protected virtual string GetVerticalInput()
    {
        return "Vertical";
    }
}
