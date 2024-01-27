using UnityEngine;

public class ShieldController : MonoBehaviour
{
    // Define the rotation angles for each direction
    private readonly float angleUp = 0f;      // Angle for the shield facing up
    private readonly float angleDown = 180f;  // Angle for the shield facing down
    private readonly float angleLeft = 90f;  // Angle for the shield facing left
    private readonly float angleRight = 270f;  // Angle for the shield facing right

    void Update()
    {
        // Check for key presses (arrow keys and WASD) and rotate the shield to the corresponding direction
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            SetShieldRotation(angleUp);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            SetShieldRotation(angleDown);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            SetShieldRotation(angleLeft);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
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
