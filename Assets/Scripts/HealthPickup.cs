using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Scene References")]
    public Player player;

    [Header("Values")]
    public float healAmount = 15f;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (player.FullHealth) return;

            // Give player health
            player.Heal(healAmount);

            // Destroy object
            Destroy(gameObject);
        }
    }
}
