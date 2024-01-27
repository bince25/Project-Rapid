using UnityEngine;

public class ShieldBlock : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack")) // Ensure your attacks have the "Attack" tag
        {
            Destroy(other.gameObject); // Destroy the attack on collision
            // Play sound effect or visual effect for successful block
        }
    }
}
