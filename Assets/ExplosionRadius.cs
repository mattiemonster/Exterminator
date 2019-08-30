using System.Collections.Generic;
using UnityEngine;

public class ExplosionRadius : MonoBehaviour
{
    Source source;
    Projectile projectile;
    bool triggered = false;
    // List<GameObject> hurtObjects = new List<GameObject>();

    public void Init(Projectile projectile)
    {
        GetComponent<SphereCollider>().radius = projectile.explosionRange;
        source = projectile.source;
        this.projectile = projectile;
    }

    void OnTriggerEnter(Collider col)
    {
        triggered = true;

        // Debug.Log("Collsion");
        // Debug.Log(col.gameObject.tag);
        // Debug.Log(source);

        // hurtObjects.Add(col.gameObject);
        // if (hurtObjects.Contains(col.gameObject)) return;

        if (col.gameObject.tag == "Enemy" && source == Source.Enemy) return;
        if (col.gameObject.tag == "Player" && source == Source.Player) return;
        if (col.gameObject.tag == "Player")
        {
            var dist = Vector3.Distance(col.gameObject.transform.position, transform.position);
            var damage = Mathf.Lerp(projectile.damage, 0, (dist / (projectile.explosionRange / 2)));
            Debug.Log(damage);
            col.gameObject.GetComponent<Player>().Hurt(damage);
        }
        if (col.gameObject.tag == "Enemy")
        {
            // Debug.Log("Attempting hit");
            var dist = Vector3.Distance(col.gameObject.transform.position, transform.position);
            var damage = Mathf.Lerp(projectile.damage, 0, (dist / (projectile.explosionRange / 2)));
            Debug.Log(damage);
            col.gameObject.GetComponent<EnemyObject>().Hurt(damage);
        }

    }

    void Update()
    {
        Debug.Log("Updating");
        if (triggered)
        {
            Debug.Log("destroying");
            Destroy(gameObject);
        }
    }
}
