using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFlight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, -1.0f) * 100);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && !SwordSwing.blockon)
        {
            Destroy(collision.gameObject);
        }
        if(!collision.gameObject.CompareTag("Boss")){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
