using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotate : MonoBehaviour
{
    //public GameObject player;
    private bool rotated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !rotated)
        {
            rotated = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) && rotated)
        {
            rotated = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<BlockPush>() && rotated)
        {
            rotated = false;
            other.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        }
    }
}
