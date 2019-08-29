using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyObject : MonoBehaviour
{
    [Header("Values")]
    public Enemy enemyType;
    public MeshRenderer meshRenderer;
    public bool canAttack = true;
    [Space]
    [SerializeField]
    private float health;

    [Header("Scene References")]
    public GameObject idleModel;
    public GameObject attackModel;
    public GameObject player;
    public GameObject eyeLight;

    [Header("Asset References")]
    public GameObject bloodParticle;

    private Rigidbody rb;
    private AudioSource audioSrc;
    private Color originalColour;
    [HideInInspector]
    public NavMeshAgent agent;

    void Start()
    {
        if (!meshRenderer)
            meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(player.transform.position);
        agent.speed = enemyType.movementSpeed;
        originalColour = meshRenderer.material.color;
        health = enemyType.maxHealth;
        idleModel.SetActive(true);
        attackModel.SetActive(false);
    }

    IEnumerator HurtEffect(MeshRenderer toFlash, Color flashColor, float flashTime, float flashSpeed)
    {
        var flashingFor = 0f;
        var newColor = flashColor;
        while (flashingFor < flashTime)
        {
            if (!toFlash) yield break;
            toFlash.material.color = newColor;
            flashingFor += Time.deltaTime;
            yield return new WaitForSeconds(flashSpeed);
            flashingFor += flashSpeed;
            if (newColor == flashColor)
            {
                newColor = originalColour;
            }
            else
            {
                newColor = flashColor;
            }
        }
        if (!toFlash) yield break;
        toFlash.material.color = originalColour;
    }

    public void Hurt(float damage)
    {
        health -= damage;
        StartCoroutine(HurtEffect(meshRenderer, Color.red, 0.5f, 0.08f));
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
        audioSrc.clip = enemyType.hurtSound;
        audioSrc.Play();
        if (health <= 0)
        {
            StopAllCoroutines();
            Destroy(meshRenderer);
            Destroy(GetComponent<BoxCollider>());
            Destroy(eyeLight);
            audioSrc.clip = enemyType.deathSound;
            audioSrc.Play();
            Destroy(gameObject, enemyType.deathSound.length);
            Destroy(this);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //if (col.gameObject.tag == "Player")
        //{
        //    Debug.Log("Player collision!");
        //    // gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * 2);
        //    EnterAttackModel();
        //}
    }

    IEnumerator AttackCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        canAttack = true;
    }

    public void Attack(Player player)
    {
        if (canAttack)
        {
            EnterAttackModel();
            StartCoroutine(ReturnToIdleModel());
            StartCoroutine(AttackCooldown(enemyType.attackDelay));
        }

        if (enemyType.attackType == EnemyAttackType.Melee)
        {
            // Melee attack

            // Knockback
            Vector3 moveDirection = player.transform.position - transform.position;
            moveDirection.y = 0;
            // rb.AddForce(moveDirection.normalized * -enemyType.attackKnockback);
            if (rb.velocity.y >= 1)
                rb.velocity = new Vector3(rb.velocity.x, 1, rb.velocity.z);

            if (!canAttack) return; // If still cooling down, do not attack

            player.Hurt(enemyType.damage);
        } else
        {
            if (!canAttack) return; // If still cooling down, do not attack
            // Projectile attack

            // var heading = player.transform.position - transform.position;
            Instantiate(enemyType.projectile, transform.position, Quaternion.identity);
        }

        canAttack = false;
    }

    void EnterAttackModel()
    {
        attackModel.SetActive(true);
        idleModel.SetActive(false);
    }

    IEnumerator ReturnToIdleModel(float time = 0.4f)
    {
        yield return new WaitForSeconds(time);
        idleModel.SetActive(true);
        attackModel.SetActive(false);
    }

    void FixedUpdate()
    {
        if (enemyType.supressBasicAI) return;
        agent.SetDestination(player.transform.position);
    }
}
