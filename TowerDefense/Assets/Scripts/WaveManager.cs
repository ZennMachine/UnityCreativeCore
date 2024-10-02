using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public TowerDefenseManager tdm;
    public int numOfWaves;
    private int currentWave = 0;
    public float waveTimer;
    public AudioSource WaveStartSound;
    public Transform spawnPoint;
    public Dictionary<Enemy, int> enemySpawnDict;
    public List<GameObject> enemySpawnList;

    private void Awake()
    {
        tdm = GameObject.Find("GameManager").GetComponent<TowerDefenseManager>();
    }
    public void StartWaveSpawner()
    {
        InvokeRepeating("SpawnWave", 0.2f, waveTimer);
    }

    private void SpawnWave()
    {
        if (currentWave == numOfWaves)
        {
            Debug.Log("End of game");
            InvokeRepeating("CheckEnemies", 5.0f, 2.0f);
        }
        else
        {
            Instantiate(enemySpawnList[currentWave], spawnPoint);
            currentWave++;
        }
    }

    public void ResetWaveManager()
    {
        currentWave = 0;                                                                                                                                                                                                                                                                                                                        
    }

    public void CheckEnemies()
    {

        int numOfEnemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log(numOfEnemiesAlive);
        if(numOfEnemiesAlive == 0) 
        {
            tdm.GameWin();
        }
    }
}
