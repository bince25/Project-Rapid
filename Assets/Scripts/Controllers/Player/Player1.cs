using UnityEngine;

public class Player1 : PlayerController
{
    protected override void Awake()
    {
        base.Awake();
        // Add additional initialization for Player 1 if needed
    }

    protected override void Start()
    {
        base.Start();
        isPlayer2 = false;
        // Add additional initialization for Player 1 if needed
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        // Add additional update logic for Player 1 if needed
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
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
            else if (heldBaby != null && dad != null)
            {
                if (dad.transform.parent.GetComponent<DadController>().GiveBabyToDad(heldBaby))
                {
                    heldBaby = null;
                }
            }
            else if (heldBaby == null && dad != null)
            {
                dad.transform.parent.GetComponent<DadController>().KillOrTalk(this.gameObject);
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
        return "Horizontal";
    }

    protected override string GetVerticalInput()
    {
        return "Vertical";
    }
}