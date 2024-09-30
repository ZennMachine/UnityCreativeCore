using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float health;
    public float maxHealth;
    public int damage;
    public int scoreGiven;
    // This is the players base
    private Transform endPoint;
    private NavMeshAgent agent;
    private TowerDefenseManager tdm;
    [SerializeField]
    private SpriteRenderer healthBar;
    [SerializeField]
    private GameObject healthBarHolder;
    private float healthScale;
    private float healthBaseScale = 5.0f;

    private void Awake()
    {
        tdm = GameObject.FindAnyObjectByType<TowerDefenseManager>();
        agent = GetComponent<NavMeshAgent>();
        healthBar.transform.localScale = new Vector3(healthBaseScale, 1, 1);
        healthScale = transform.localScale.x / health;
        maxHealth = health;
        if (endPoint == null)
            endPoint = GameObject.Find("EndPoint").transform;
        agent.SetDestination(endPoint.position);
        agent.speed = moveSpeed;
        agent.acceleration = moveSpeed;
    }

    private void Update()
    {
        healthBarHolder.transform.LookAt(Camera.main.transform.position);
    }

    public bool TakeDamage(int damage)
    {
        health -= damage;
        float newHealthScale = health * healthScale;
        healthBar.transform.localScale = new Vector3(newHealthScale, 1, 1);
        if (health <= 0)
        {
            Death();
            return true;
        }
        return false;
    }

    public void Death()
    {
        tdm.AddCoins(scoreGiven);
        Destroy(gameObject);
    }
}
