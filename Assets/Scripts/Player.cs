using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Scene References")]
    public GameObject viewmodel;
    public Transform viewmodelPoint;
    public Crosshair crosshair;
    public GameObject fpsCam;

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
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, currentWeapon.range))
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                crosshair.PlayDamageAnim();
                hit.transform.gameObject.GetComponent<EnemyObject>().Hurt(currentWeapon.damage);
            }
        }
    }
}
