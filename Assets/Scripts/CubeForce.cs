using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeForce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "Boss")
        {
            Vector3 f = new Vector3(1, 0, 0);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.rotation * f * 100);
        }
        
    }
}
