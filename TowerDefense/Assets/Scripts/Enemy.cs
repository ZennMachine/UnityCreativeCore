using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float health;
    public int scoreGiven;
    // This is the players base
    private Transform endPoint;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (endPoint == null)
            endPoint = GameObject.Find("EndPoint").transform;
        agent.SetDestination(endPoint.position);
        agent.speed = moveSpeed;
        agent.acceleration = moveSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
            Death();
    }

    public void Death()
    {

    }
}
