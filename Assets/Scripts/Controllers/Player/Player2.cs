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
        isPlayer2 = true;
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
            if (heldBaby == null && (this.currentIncubatorArea != null || this.pregnantWoman != null))
            {
                PickUpBaby();
            }
            else if (heldBaby != null && currentIncubatorArea != null)
            {
                PlaceBaby();
            }
            else if (heldBaby != null && babySit != null)
            {
                ShieldGameManager.Instance.StartGame(heldBaby);
            }
        }
    }

    public override void SetCanMove(bool canMove)
    {
        base.SetCanMove(canMove);
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