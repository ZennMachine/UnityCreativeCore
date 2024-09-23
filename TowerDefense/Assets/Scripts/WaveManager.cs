using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int numOfWaves;
    private int currentWave = 0;
    public float waveTimer;
    public AudioSource WaveStartSound;
    public Transform spawnPoint;
    public Dictionary<Enemy, int> enemySpawnDict;
    public List<GameObject> enemySpawnList;
    public void StartWaveSpawner()
    {
        InvokeRepeating("SpawnWave", 0.2f, waveTimer);
    }

    private void SpawnWave()
    {
        Instantiate(enemySpawnList[currentWave], spawnPoint);
        currentWave++;
    }
    /*
    private void SpawnEnemy(Enemy enemy)
    {
        Instantiate(enemy, spawnPoint, true);
    }*/
    // Start is called before the first frame update
    void Start()
    {
        StartWaveSpawner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
