using UnityEngine;

public class AttackController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the attack moves
    private Vector3 moveDirection; // Direction for the attack to move
    private bool hit = false;

    // Function to be called by the spawner to set the attack's movement direction
    public void SetMoveDirection(Vector3 direction)
    {
        moveDirection = direction.normalized; // Ensure the direction is normalized
    }

    void Update()
    {
        // Move the attack in the set direction at the set speed
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hit)
        {
            return;
        }
        GetComponentInParent<AttackSpawner>().AttackDestroyed();
        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject);

            // Optional: Add visual or audio feedback for the collision
        }
        else if (other.CompareTag("MinigamePlayer"))
        {
            GetComponentInParent<AttackSpawner>().Hit();
            Destroy(gameObject);
        }
    }
}
