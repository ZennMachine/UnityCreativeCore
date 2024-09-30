using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private int damage;
    public int towerCost;
    public int towerLevel;
    [SerializeField]
    private LineRenderer lr;
    [SerializeField]
    private float range;
    [SerializeField]
    private List<GameObject> targets;
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.6f);
        Gizmos.DrawSphere(transform.position, range);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Instead of fancy coroutines or whatever, just check on each firerate cycle
        // if the tower has a valid target and onl then will it try to shoot.
        // This way on each firing cycle it can check if the target is still in range
        CheckTargets();
        InvokeRepeating("CheckTargets", fireRate, fireRate);
        lr.gameObject.SetActive(false);
        lr.SetPosition(0, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckTargets()
    {
        if (!targets.Any())
            return;
        GameObject currentTarget = targets[0];


        if(currentTarget != null)
        {
            lr.gameObject.SetActive(true);
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, currentTarget.transform.position);
            //Debug.Log("Pew");
            if (currentTarget.GetComponent<Enemy>().TakeDamage(damage))
            {
                targets.Remove(currentTarget);
            }
            Invoke("TurnOffLineRenderer", 0.1f);
        }
    }

    private void TurnOffLineRenderer()
    {
        lr.gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            targets.Remove(other.gameObject);
        }
    }
}
