using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmmoPickup : MonoBehaviour
{
    [Header("Scene References")]
    public Player player;

    [Header("Asset References")]
    public AudioClip pickupSound;

    private AudioSource audioSrc;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (player.FullReserveAmmo) return;

            // Give player ammo
            player.PickupAmmo();

            // Play sound
            audioSrc.clip = pickupSound;
            audioSrc.Play();

            // Destroy object
            Destroy(GetComponent<BoxCollider>());
            Destroy(GetComponent<MeshRenderer>());
            Destroy(gameObject, pickupSound.length + 0.05f);
        }
    }
}
