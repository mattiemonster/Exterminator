using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Scene References")]
    public Transform spawnPoint;

    [Header("Asset References")]
    public GameObject enemyPrefab;
    
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
}
