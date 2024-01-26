using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float moveSpeed = 10f;

    public GameObject heldBaby = null;
    protected GameObject currentIncubatorArea = null;

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

    protected virtual void Update()
    {

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("IncubatorArea"))
        {
            currentIncubatorArea = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == currentIncubatorArea)
        {
            currentIncubatorArea = null;
        }
    }

    /// <summary>
    /// Places the held baby in the current incubator area
    /// </summary>
    protected void PlaceBaby()
    {
        IncubatorController incubator = currentIncubatorArea.GetComponentInParent<IncubatorController>();
        if (incubator != null && incubator.TryPlaceBaby(heldBaby))
        {
            heldBaby = null;

            // Sound effect: Successful placement
            Debug.Log("Baby placed in the incubator.");
        }
        else
        {
            // Sound effect: Placement failed
            Debug.Log("Failed to place the baby in the incubator.");
        }
    }

    /// <summary>
    /// Picks up the baby from the current incubator area
    /// </summary>
    protected void PickUpBaby()
    {
        IncubatorController incubator = currentIncubatorArea.GetComponentInParent<IncubatorController>();
        if (incubator != null)
        {
            GameObject baby = incubator.PickUpBaby();
            if (baby != null)
            {
                heldBaby = baby;

                // Sound effect: Baby picked up
                Debug.Log("Baby picked up from the incubator.");
            }
            else
            {
                // Sound effect: No baby to pick up
                Debug.Log("No baby in this incubator to pick up.");
            }
        }
    }
}
