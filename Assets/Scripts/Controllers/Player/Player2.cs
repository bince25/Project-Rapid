using UnityEngine;

public class Player2 : PlayerController
{
    protected override void Awake()
    {
        base.Awake();
        // Add additional initialization for Player 1 if needed
    }

    protected override void Start()
    {
        base.Start();
        // Add additional initialization for Player 1 if needed
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        // Add additional update logic for Player 1 if needed
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (heldBaby == null && this.currentIncubatorArea != null)
            {
                PickUpBaby();
            }
            else if (heldBaby != null && currentIncubatorArea != null)
            {
                PlaceBaby();
            }
        }
    }

    protected override void Move()
    {
        base.Move();
        // Add additional movement logic for Player 1 if needed
    }
    protected override string GetHorizontalInput()
    {
        return "Horizontal2";
    }

    protected override string GetVerticalInput()
    {
        return "Vertical2";
    }
}