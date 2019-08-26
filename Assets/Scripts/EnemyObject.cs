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

    private Rigidbody rb;
    private Color originalColour;
    private NavMeshAgent agent;

    void Start()
    {
        if (!meshRenderer)
            meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(player.transform.position);
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
        toFlash.material.color = originalColour;
    }

    public void Hurt(float damage)
    {
        health -= damage;
        StartCoroutine(HurtEffect(meshRenderer, Color.red, 0.5f, 0.08f));
        if (health == 0)
            Destroy(gameObject);
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
        // Knockback
        Vector3 moveDirection = player.transform.position - transform.position;
        moveDirection.y = 0;
        rb.AddForce(moveDirection.normalized * -enemyType.attackKnockback);
        if (rb.velocity.y >= 1)
            rb.velocity = new Vector3(rb.velocity.x, 1, rb.velocity.z);

        if (!canAttack) return; // If still cooling down, do not damage player.

        EnterAttackModel();
        StartCoroutine(ReturnToIdleModel());
        player.Hurt(enemyType.damage);

        canAttack = false;
        StartCoroutine(AttackCooldown(enemyType.attackDelay));
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
        agent.SetDestination(player.transform.position);
    }
}
