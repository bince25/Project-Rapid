using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float moveSpeed = 10f;

    public GameObject heldBaby = null;
    protected GameObject currentIncubatorArea = null;
    protected GameObject pregnantWoman = null;
    protected GameObject babySit = null;
    protected GameObject dad = null;
    protected bool canMove = true;
    public bool isPlayer2;
    public Animator animator;

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
        animator = GetComponent<Animator>();
    }


    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Update()
    {

    }

    public virtual void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    /// <summary>
    /// Moves the player based on the input
    /// </summary>
    protected virtual void Move()
    {
        if (canMove)
        {
            Vector2 moveInput = new Vector2(Input.GetAxis(GetHorizontalInput()), Input.GetAxis(GetVerticalInput()));
            Vector2 moveVelocity = moveInput.normalized * moveSpeed;
            rb.velocity = moveVelocity;
            animator.SetBool("IsMoving", moveInput.magnitude > 0.1f);
            animator.SetFloat("Horizontal", moveInput.x);
            animator.SetFloat("Vertical", moveInput.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
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
        else if (other.CompareTag("Pregnant"))
        {
            pregnantWoman = other.gameObject;
        }
        else if (other.CompareTag("BabySit"))
        {
            babySit = other.gameObject;
        }
        else if (other.CompareTag("Dad"))
        {
            dad = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == currentIncubatorArea)
        {
            currentIncubatorArea = null;
        }
        else if (other.gameObject == pregnantWoman)
        {
            pregnantWoman = null;
        }
        else if (other.gameObject == babySit)
        {
            babySit = null;
        }
        else if (other.gameObject == dad)
        {
            dad = null;
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
        if (currentIncubatorArea != null)
        {
            IncubatorController incubator = currentIncubatorArea.GetComponentInParent<IncubatorController>();
            GameObject baby = incubator.PickUpBaby();
            if (baby != null)
            {
                AudioManager.Instance.PlaySFX(SFX.PickUpBaby);
                heldBaby = baby;
                if (heldBaby != null)
                {
                    heldBaby.transform.SetParent(transform);
                }
                // Sound effect: Baby picked up
                Debug.Log("Baby picked up from the incubator.");
            }
            else
            {
                // Sound effect: No baby to pick up
                Debug.Log("No baby in this incubator to pick up.");
            }
        }
        else if (pregnantWoman != null)
        {
            PregnantController pregnant = pregnantWoman.GetComponent<PregnantController>();
            heldBaby = pregnant.TakeBaby();

            if (heldBaby != null)
            {
                heldBaby.transform.SetParent(transform);
            }
        }
        else
        {
            // Sound effect: No baby to pick up
            Debug.Log("No baby in this incubator to pick up.");
        }
    }
}
