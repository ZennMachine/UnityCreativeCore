using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private int damage;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float range;
    [SerializeField]
    private List<GameObject> targets;
    private GameObject currentTarget;
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
        InvokeRepeating("CheckTargets", fireRate, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckTargets()
    {
        if(currentTarget == null && targets[0] != null)
            currentTarget = targets[0];

        GameObject obj = Instantiate(projectile, transform.position, Quaternion.identity);
        obj.GetComponent<Projectile>().myTarget = currentTarget;
        Debug.Log("Pew");
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
