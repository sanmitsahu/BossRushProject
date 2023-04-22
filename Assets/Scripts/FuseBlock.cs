using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBlock : MonoBehaviour
{
    public GameObject lowChild = null;
    public GameObject highChild = null;
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
        if ((other.gameObject.tag == "Punch" && PlayerController.swung && !PlayerController.swordHit) || BlockGrab.grab)
        {
            transform.position = new Vector3(10.0f, 0.0f, 0.0f);

            lowChild.GetComponent<Rigidbody>().mass = 1.0f;
            lowChild.GetComponent<Rigidbody>().velocity = Vector3.zero;
            lowChild.transform.position = lowChild.GetComponent<BlockPush>().originalPos;

            highChild.GetComponent<Rigidbody>().mass = 1.0f;
            highChild.GetComponent<Rigidbody>().velocity = Vector3.zero;
            highChild.transform.position = highChild.GetComponent<BlockPush>().originalPos;

            //transform.position = new Vector3(1000.0f, 1000.0f, 1000.0f);
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Boss")
        {
            transform.position = new Vector3(10.0f, 0.0f, 0.0f);

            lowChild.GetComponent<Rigidbody>().mass = 1.0f;
            lowChild.GetComponent<Rigidbody>().velocity = Vector3.zero;
            lowChild.transform.position = lowChild.GetComponent<BlockPush>().originalPos;
            lowChild = null;

            highChild.GetComponent<Rigidbody>().mass = 1.0f;
            highChild.GetComponent<Rigidbody>().velocity = Vector3.zero;
            highChild.transform.position = highChild.GetComponent<BlockPush>().originalPos;
            highChild = null;
        }
    }
}
