using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Scene References")]
    public GameObject viewmodel;
    public Transform viewmodelPoint;

    [Header("Values")]
    public Weapon currentWeapon;

    void Start()
    {
        
    }

    public void ChangeWeapon(Weapon weapon)
    {
        //if (viewmodel) Destroy(viewmodel);
        //viewmodel = Instantiate(weapon.weaponPrefab, gameObject.transform);
        //viewmodel.transform.position = viewmodelPoint.transform.position;


    }
}
