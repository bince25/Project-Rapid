using UnityEngine;

public class ShieldController : MonoBehaviour
{
    // Define the rotation angles for each direction
    private readonly float angleUp = 0f;      // Angle for the shield facing up
    private readonly float angleDown = 180f;  // Angle for the shield facing down
    private readonly float angleLeft = 90f;  // Angle for the shield facing left
    private readonly float angleRight = 270f;  // Angle for the shield facing right
    public bool isPlayer2;

    void Update()
    {
        switch (isPlayer2)
        {
            case true:
                Player2Controls();
                break;
            case false:
                Player1Controls();
                break;
        }
    }

    // Function to control the shield for Player 1
    void Player2Controls()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetShieldRotation(angleUp);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetShieldRotation(angleDown);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetShieldRotation(angleLeft);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetShieldRotation(angleRight);
        }
    }

    // Function to control the shield for Player 2
    void Player1Controls()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetShieldRotation(angleUp);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SetShieldRotation(angleDown);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SetShieldRotation(angleLeft);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SetShieldRotation(angleRight);
        }
    }

    // Function to set the shield's rotation
    void SetShieldRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
