using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// This will instanciate enemies in the correspondent spawnpoints once the player enters de area
/// </summary>
public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] GameObject spawnPosFather;
    [SerializeField] GameObject enemy;
    [SerializeField] float spawnTime = 1f;
    Transform[] spawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        spawnPositions = spawnPosFather.GetComponentsInChildren<Transform>();
        InvokeRepeating("SpawnEnemy", 1, spawnTime);
    }

    // Remove all the enemies in the view
    private void OnDisable()
    {
        CancelInvoke();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
    }

    private void OnEnable()
    {
        InvokeRepeating("SpawnEnemy", 1, spawnTime);
    }

    void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPositions.Length);
        Instantiate(enemy, spawnPositions[spawnIndex].position, spawnPositions[spawnIndex].rotation);
    }
}
