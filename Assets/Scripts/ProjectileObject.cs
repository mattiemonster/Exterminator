using System.Collections;
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

    [Header("Scene References")]
    public GameObject mesh;
    public GameObject objectLight;

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
        StartCoroutine(ExplodeAfterTime(projectileType.maxFlyTime));
    }

    IEnumerator ExplodeAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
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

        StopAllCoroutines();

        if (projectileType.explosive)
        {
            Instantiate(explosionRadius, transform.position, Quaternion.identity).GetComponent<ExplosionRadius>().Init(projectileType);
        }
        Instantiate(projectileType.collideEffect, transform.position, Quaternion.identity);
        if (!projectileType.explosive)
        {
            Destroy(gameObject);
            return;
        } else
        {
            foreach (var comp in gameObject.GetComponents<Component>())
            {
                if (!(comp is Transform || comp is AudioSource))
                {
                    Destroy(comp);
                }
            }
            Destroy(mesh);
            Destroy(objectLight);
            Destroy(gameObject, projectileType.explosionSound.length + 0.05f);
        }
        AudioSource audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = projectileType.explosionSound;
        audioSrc.Play();

    }

}
