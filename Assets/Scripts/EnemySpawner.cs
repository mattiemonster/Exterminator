using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Scene References")]
    public Transform spawnPoint;

    [Header("Asset References")]
    public GameObject enemyPrefab;

    private float size = 0.15f;
    
    void Start()
    {
        if (LevelMaster.noRatSpawning && enemyPrefab.name == "Rat")
            Destroy(gameObject);
        if (LevelMaster.noAcidRatSpawning && enemyPrefab.name == "AcidRat")
            Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        switch (enemyPrefab.name)
        {
            case "Rat":
                Gizmos.color = Color.red;
                break;
            case "AcidRat":
                Gizmos.color = Color.green;
                break;
            case "BigRat":
                Gizmos.color = Color.red;
                size = 0.5f;
                break;
            case "BigAcidRat":
                Gizmos.color = Color.green;
                size = 0.5f;
                break;
            default:
                Gizmos.color = Color.black;
                break;
        }
        Gizmos.DrawSphere(spawnPoint.transform.position, size);
    }
}
