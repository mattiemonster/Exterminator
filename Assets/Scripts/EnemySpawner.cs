using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Scene References")]
    public Transform spawnPoint;

    [Header("Asset References")]
    public GameObject enemyPrefab;
    
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            Destroy(gameObject);
        }
    }
}
