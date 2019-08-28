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
    public GameObject reloadingUI;
    public GameObject noAmmoUI;
    public TextMeshProUGUI currentAmmoUI, reserveAmmoUI;

    [Header("Asset References")]
    public AudioClip hurtSound;
    public AudioClip weaponReadySound;

    [Header("Values")]
    public Weapon currentWeapon;
    public float health = 100f;

    [HideInInspector]
    public bool canShoot = true;
    [HideInInspector]
    public bool FullHealth
    {
        get { return health == 100 ? true : false; }
    }
    [HideInInspector]
    public bool FullReserveAmmo
    {
        get { return reserveAmmo == currentWeapon.maxReserveAmmo ? true : false; }
    }
    private AudioSource audioSrc;
    private int currentAmmo, reserveAmmo;
    private bool outOfAmmo;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        reloadingUI.SetActive(false);
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

        if (currentAmmo != currentWeapon?.maxCurrentAmmo && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    public void Shoot()
    {
        if (!canShoot) return;

        // Shoot cooldown
        canShoot = false;
        StartCoroutine(ShootCooldown());

        // Update ammo
        currentAmmo--;
        UpdateAmmoUI();
        
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

        if (currentAmmo == 0)
        {
            canShoot = false;
            StartCoroutine(Reload());
            outOfAmmo = true;
        }
    }

    IEnumerator Reload()
    {
        // Show reloading UI
        if (reserveAmmo == 0)
        {
            if (currentAmmo == 0)
            {
                noAmmoUI.SetActive(true);
                yield break;
            }
            else
                yield break;
        }
        canShoot = false;
        reloadingUI.SetActive(true);
        // Wait
        yield return new WaitForSeconds(currentWeapon.reloadTime);
        // Update ammo
        int neededAmmo = currentWeapon.maxCurrentAmmo - currentAmmo;
        reserveAmmo -= neededAmmo;
        if (reserveAmmo >= neededAmmo)
        {
            currentAmmo = currentWeapon.maxCurrentAmmo;
        } else
        {
            currentAmmo += reserveAmmo;
            reserveAmmo = 0;
        }
        UpdateAmmoUI();
        reloadingUI.SetActive(false);
        outOfAmmo = false;
        canShoot = true;
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
        UpdateHealthBar();

        // Play hurt sound
        audioSrc.clip = hurtSound;
        audioSrc.Play();

        // Check if player has died
        // TODO Sound and animation
        if (health <= 0)
            Kill();
    }

    public void Heal(float healAmount = 15f)
    {
        // Add health
        health += healAmount;
        // Check if new health is over max
        if (health >= 100)
            health = 100;
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthBar.value = health;
        healthBarFill.color = Color.Lerp(Color.red, Color.green, health / 100);
        healthText.text = health.ToString();
    }

    public void Kill()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(currentWeapon.fireSpeed);
        if (outOfAmmo) yield break;
        canShoot = true;

        //audioSrc.clip = weaponReadySound;
        //audioSrc.Play();

        if (currentWeapon.showWeaponReady)
            weaponReadyUI.GetComponent<Animator>().Play("WeaponReady");
    }

    public void PickupAmmo()
    {
        if (reserveAmmo == currentWeapon.maxReserveAmmo) return;
        else
        {
            reserveAmmo += currentWeapon.maxCurrentAmmo;
            if (reserveAmmo > currentWeapon.maxReserveAmmo)
                reserveAmmo = currentWeapon.maxReserveAmmo;
            UpdateAmmoUI();
        }
    }
}
