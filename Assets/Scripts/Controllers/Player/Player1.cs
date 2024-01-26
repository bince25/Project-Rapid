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
        // Add additional initialization for Player 1 if needed
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        // Add additional update logic for Player 1 if needed
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