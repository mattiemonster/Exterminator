using UnityEngine;

public class AcidRatAI : MonoBehaviour
{
    private EnemyObject enemy;

    [Header("Asset References")]
    public GameObject spitBall;

    [Header("Values")]
    public float spitDistance = 8f;
    public bool boss = false;

    void Start()
    {
        if (!spitBall) Debug.LogError("Spit ball prefab null");
        enemy = GetComponent<EnemyObject>();
    }

    void FixedUpdate()
    {
        // AI
        var heading = enemy.player.transform.position - transform.position;
        // var distance = Vector3.Distance(enemy.player.transform.position, transform.position);
        if (!boss)
        {
            if (!(heading.sqrMagnitude < spitDistance * spitDistance))
            {
                enemy.agent.isStopped = false;
                enemy.agent.SetDestination(enemy.player.transform.position);
            }
            else
            {
                transform.LookAt(enemy.player.transform);
                // transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, transform.rotation.z));
                // Stop and spit
                enemy.agent.isStopped = true;
                if (enemy.canAttack) enemy.Attack(enemy.player.GetComponent<Player>());
            }
        } else
        {
            enemy.agent.SetDestination(enemy.player.transform.position);
            transform.LookAt(enemy.player.transform);
            // transform.rotation = Quaternion.Euler(transform.rotation * new Vector3(0, 0, 1));
            if (enemy.canAttack) enemy.Attack(enemy.player.GetComponent<Player>());
        }
    }
}
