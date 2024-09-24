using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject myTarget;
    [SerializeField]
    private float myMoveSpeed;

    private float lifeTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Invoke(Destroy(""gameObject"", lifeTime));
    }

    // Update is called once per frame
    void Update()
    {
        if(myTarget != null)
        {
            transform.position += myTarget.transform.position * Time.deltaTime * myMoveSpeed;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == myTarget)
        {
            myTarget.GetComponent<Enemy>().Death();
            Destroy(gameObject);
        }
    }
}
