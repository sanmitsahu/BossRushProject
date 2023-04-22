using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        //UnityEngine.Debug.Log(PlayerController.swung);
        if ((other.gameObject.tag == "Punch" && PlayerController.swung && !PlayerController.swordHit))
        {
            transform.position = new Vector3(10.0f, 0.0f, 0.0f);
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<BlockPush>())
                {
                    child.gameObject.GetComponent<Rigidbody>().mass = 1.0f;
                    child.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    child.gameObject.transform.position = child.gameObject.GetComponent<BlockPush>().originalPos;
                    child.gameObject.transform.parent = null;
                }
            }
            //transform.position = new Vector3(1000.0f, 1000.0f, 1000.0f);
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Boss")
        {
            transform.position = new Vector3(10.0f, 0.0f, 0.0f);
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<BlockPush>())
                {
                    child.gameObject.GetComponent<Rigidbody>().mass = 1.0f;
                    child.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    child.gameObject.transform.position = child.gameObject.GetComponent<BlockPush>().originalPos;
                    child.gameObject.transform.parent = null;
                }
            }
        }
    }
}
