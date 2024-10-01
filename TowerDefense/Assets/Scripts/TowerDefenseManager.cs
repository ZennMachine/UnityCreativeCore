using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerDefenseManager : MonoBehaviour
{
    public int coins = 200;
    public int lives = 20;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI coinsText;
    public GameObject gameOverUI;
    public GameObject waveManager;
    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
        livesText.text = $"Health: {lives} / 20";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
        livesText.text = $"Health: {lives} / 20";
        if (lives <= 0)
            GameOver();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        coinsText.text = $"Coins: {coins}";
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
        coinsText.text = $"Coins: {coins}";
    }

    public void GameOver()
    {
        coins = 200;
        lives = 20;
        Transform spawnPoint = waveManager.transform.GetChild(0);
        foreach (Transform child in spawnPoint)
        {
            GameObject.Destroy(child.gameObject);
        }
        waveManager.GetComponent<WaveManager>().CancelInvoke();
        waveManager.GetComponent<WaveManager>().ResetWaveManager();
        gameOverUI.SetActive(true);
    }
}
