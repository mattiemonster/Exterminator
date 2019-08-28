using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HealthPickup : MonoBehaviour
{
    [Header("Scene References")]
    public Player player;

    [Header("Asset References")]
    public AudioClip healSound;

    [Header("Values")]
    public float healAmount = 15f;

    private AudioSource audioSrc;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (player.FullHealth) return;

            // Give player health
            player.Heal(healAmount);

            // Play sound
            audioSrc.clip = healSound;
            audioSrc.Play();

            // Destroy object
            Destroy(gameObject, healSound.length + 0.05f);
        }
    }
}
