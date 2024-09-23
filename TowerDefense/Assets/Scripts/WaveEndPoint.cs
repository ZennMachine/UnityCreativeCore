using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEndPoint : MonoBehaviour
{
    private TowerDefenseManager tdManager;
    // Start is called before the first frame update
    void Start()
    {
        tdManager = GameObject.Find("GameManager").GetComponent<TowerDefenseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if(other.gameObject.tag == "Enemy")
        {
            tdManager.TakeDamage(other.GetComponent<Enemy>().damage);
            Destroy(other.gameObject);
        }
    }
}
