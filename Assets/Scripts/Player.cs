using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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

    [Header("Asset References")]
    public AudioClip hurtSound;

    [Header("Values")]
    public Weapon currentWeapon;
    public float health = 100f;

    private AudioSource audioSrc;

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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Enemy")
            hit.gameObject.GetComponent<EnemyObject>().Attack(this);
    }

    public void Hurt(float damage)
    {
        health -= damage;
        healthBar.value = health;
        healthBarFill.color = Color.Lerp(Color.red, Color.green, health / 100);
        healthText.text = health.ToString();
        audioSrc.clip = hurtSound;
        audioSrc.Play();

        if (health <= 0)
            Kill();
    }

    public void Kill()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
