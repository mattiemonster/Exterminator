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
        if (col.gameObject.tag == "Enemy") return;
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().Hurt(projectileType.damage);
        }
        Instantiate(projectileType.collideEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
}
