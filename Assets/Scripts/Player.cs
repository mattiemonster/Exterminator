using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Scene References")]
    public GameObject viewmodel;
    public Transform viewmodelPoint;
    public Crosshair crosshair;

    [Header("Values")]
    public Weapon currentWeapon;

    public void ChangeWeapon(Weapon weapon)
    {
        //if (viewmodel) Destroy(viewmodel);
        //viewmodel = Instantiate(weapon.weaponPrefab, gameObject.transform);
        //viewmodel.transform.position = viewmodelPoint.transform.position;

        currentWeapon = weapon;
    }

    void Update()
    {
        if (currentWeapon && Input.GetButtonDown("PrimaryAttack")) 
            Shoot();
    }

    public void Shoot()
    {
        crosshair.PlayDamageAnim(); // Temp just play damage anim
    }
}
