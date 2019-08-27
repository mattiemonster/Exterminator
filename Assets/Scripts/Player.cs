using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Scene References")]
    public GameObject viewmodel;
    public Transform viewmodelPoint;
    public Crosshair crosshair;
    public GameObject fpsCam;
    public Slider healthBar;
    public Image healthBarFill;
    public TextMeshProUGUI healthText;
    public GameObject weaponReadyUI;
    public TextMeshProUGUI currentAmmoUI, reserveAmmoUI;

    [Header("Asset References")]
    public AudioClip hurtSound;
    public AudioClip weaponReadySound;

    [Header("Values")]
    public Weapon currentWeapon;
    public float health = 100f;

    [HideInInspector]
    public bool canShoot = true;
    private AudioSource audioSrc;
    private int currentAmmo, reserveAmmo;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void ChangeWeapon(Weapon weapon)
    {
        //if (viewmodel) Destroy(viewmodel);
        //viewmodel = Instantiate(weapon.weaponPrefab, gameObject.transform);
        //viewmodel.transform.position = viewmodelPoint.transform.position;

        currentWeapon = weapon;
        currentAmmo = weapon.maxCurrentAmmo;
        reserveAmmo = weapon.maxReserveAmmo;
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        currentAmmoUI.text = currentAmmo.ToString();
        reserveAmmoUI.text = reserveAmmo.ToString();
    }

    void Update()
    {
        if (currentWeapon && canShoot && currentWeapon.fireMode == FireMode.SingleFire &&
            Input.GetButtonDown("PrimaryAttack"))
        {
            Shoot();
        }

        if (currentWeapon && canShoot && currentWeapon.fireMode == FireMode.Auto &&
           Input.GetButton("PrimaryAttack"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {

        // Shoot cooldown
        canShoot = false;
        StartCoroutine(ShootCooldown());

        // Update ammo
        currentAmmo--;
        UpdateAmmoUI();
        if (currentAmmo == 0)
        {
            // TODO: RELOAD, PREVENT FIRING
        }
        
        // Play shoot sound
        audioSrc.clip = currentWeapon.shootSound;
        audioSrc.Play();
        
        // Check for enemy hit
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, transform.forward, out hit, currentWeapon.range))
        {
            Debug.DrawRay(fpsCam.transform.position, hit.point, Color.green, 5f);
            if (hit.transform.gameObject.tag == "Enemy")
            {
                crosshair.PlayDamageAnim();
                hit.transform.gameObject.GetComponent<EnemyObject>().Hurt(currentWeapon.damage);
            }
        }
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Enemy")
            hit.gameObject.GetComponent<EnemyObject>().Attack(this);
    }

    public void Hurt(float damage)
    {
        // Hurt player and update health UI
        health -= damage;
        healthBar.value = health;
        healthBarFill.color = Color.Lerp(Color.red, Color.green, health / 100);
        healthText.text = health.ToString();

        // Play hurt sound
        audioSrc.clip = hurtSound;
        audioSrc.Play();

        // Check if player has died
        // TODO Sound and animation
        if (health <= 0)
            Kill();
    }

    public void Kill()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(currentWeapon.fireSpeed);
        canShoot = true;

        //audioSrc.clip = weaponReadySound;
        //audioSrc.Play();

        if (currentWeapon.showWeaponReady)
            weaponReadyUI.GetComponent<Animator>().Play("WeaponReady");
    }
}
