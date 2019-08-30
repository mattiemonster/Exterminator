using UnityEngine;

public enum Source
{
    Enemy,
    Player
}

[RequireComponent(typeof(Rigidbody))]
public class ProjectileObject : MonoBehaviour
{
    [Header("Asset References")]
    public Projectile projectileType;
    public GameObject explosionRadius;

    [Header("Values")]
    public Vector3 dir;
    public Source source = Source.Enemy;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (source == Source.Enemy)
        {
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * projectileType.speed;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && source == Source.Enemy) return;
        if (col.gameObject.tag == "Enemy" && source == Source.Player)
        {
            if (!projectileType.explosive)
                col.gameObject.GetComponent<EnemyObject>().Hurt(projectileType.damage);
        }
        if (col.gameObject.tag == "Player" && source == Source.Player) return;
        if (col.gameObject.tag == "Player" && source == Source.Enemy)
        {
            if (!projectileType.explosive)
                col.gameObject.GetComponent<Player>().Hurt(projectileType.damage);
        }

        if (projectileType.explosive)
        {
            Instantiate(explosionRadius, transform.position, Quaternion.identity).GetComponent<ExplosionRadius>().Init(projectileType);
        }
        Instantiate(projectileType.collideEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
