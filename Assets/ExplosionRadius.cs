using UnityEngine;

public class ExplosionRadius : MonoBehaviour
{
    Source source;
    Projectile projectile;
    bool triggered = false;

    public void Init(Projectile projectile)
    {
        GetComponent<SphereCollider>().radius = projectile.explosionRange;
        source = projectile.source;
        this.projectile = projectile;
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collsion");
        Debug.Log(source);
        Debug.Log(col.gameObject.tag);

        if (col.gameObject.tag == "Enemy" && source == Source.Enemy) return;
        if (col.gameObject.tag == "Player" && source == Source.Player) return;
        if (col.gameObject.tag == "Player")
        {
            var dist = Vector3.Distance(col.gameObject.transform.position, transform.position);
            col.gameObject.GetComponent<Player>().Hurt(Mathf.Lerp(0, projectile.damage, (dist / (projectile.explosionRange / 2))) / 1.5f);
        }
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Attempting hit");
            var dist = Vector3.Distance(col.gameObject.transform.position, transform.position);
            col.gameObject.GetComponent<EnemyObject>().Hurt(Mathf.Lerp(0, projectile.damage, (dist / (projectile.explosionRange / 2))) / 1.5f);
        }

        triggered = true;
    }

    void Update()
    {
        if (triggered) Destroy(gameObject);
    }
}
