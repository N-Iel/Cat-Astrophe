using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This script will manage all the systems related with the gameplay of the phases
/// TODO:
/// 0� Detectar cuando el jugador entra en area de juego
/// 1� Control del tiempo activo (tiempo en el que enemigos est�n siendo spawneando)
/// 2� Control del spawn de enemigos
/// 3� Control de distintos stats (todo lo posible, hacer esto super modular)
/// </summary>

public class PhaseManager : MonoBehaviour
{

    // Phase Management
    bool isPhaseActive = false;

    // Timer
    [SerializeField] TextMeshProUGUI timerTxt;
    float timer;

    // Enemies
    [Header("Enemies")]
    [Tooltip("The time between enemy spawns")]
    public float spawnRate              = 5.0f;
    public float spawnRateIncreaseTime  = 5.0f;
    public float spawnRateLimit         = 1.0f;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    float spawnRateTimer;
    float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPhaseActive && Time.timeScale > 0.0f) return;

        OnGUITimer();

        if(spawnTimer >= spawnRate)
            OnEnemySpawn();

        if (spawnRate >= spawnRateLimit && spawnRateTimer >= spawnRateIncreaseTime)
        {
            spawnRateTimer = 0;
            spawnRate *= 0.8f;
        }

        spawnTimer += Time.deltaTime;
        spawnRateTimer += Time.deltaTime;

    }

    void OnEnemySpawn()
    {
        // Temporal, hasta hacer un buen sistema de spawn aleatorio en base a la pos del jugador
        /* 
         * El sistema actual har� aparecer en sitios marcados previamente enemigos
         * se usar� un sistema para detectar cuales est�n demasiado cerca del jugador para que no se haga injusto
         */

        // El pool no ha funcionado as� que por ahora lo dejo como destroy y buena suerte si luego hay mala optimizaci�n

        // 0� Check if there is available enemies in the pool DONE
        // 1� get a random pos spawn DONE
        // 2� check if that pos is far enough from the player
        // 3� spawn an enemy in the pos DONE
        spawnTimer = 0;
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[spawnIndex]);
    }

    void OnGUITimer()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerTxt.text = niceTime;
    }

    // Detects when the player exists the save zone and triggers the battle
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Battle Phase started");
            isPhaseActive = true;
        }
    }
}
