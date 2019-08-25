using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [Header("Scene References")]
    public MeshRenderer meshRenderer;
    public NewWeaponUI newWeaponUI;
    public CurrentWeaponUI currentWeaponUI;

    [Header("Values")]
    public float rotateSpeed = 150f;
    public Weapon weapon;

    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            newWeaponUI.OpenUI();
            currentWeaponUI.SetWeapon(weapon);
            col.gameObject.GetComponent<Player>().ChangeWeapon(weapon);           
            Destroy(meshRenderer);
            Destroy(gameObject, 0.3f);
        }
    }
}
