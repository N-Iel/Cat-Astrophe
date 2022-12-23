using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.XR;
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
    bool isBattleStarted;


    // Start is called before the first frame update
    void Start()
    {
        spawnPositions = spawnPosFather.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBattleStarted) return;

        InvokeRepeating("SpawnEnemy", 1, spawnTime);
    }

    // Remove all the enemies in the view
    private void OnDisable()
    {
        
    }

    void SpawnEnemy()
    {
        float spawnIndex = Random.Range(0, spawnPositions.Length);
        
    }
}
