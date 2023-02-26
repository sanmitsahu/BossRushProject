using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrab : MonoBehaviour
{
    public GameObject grabStart;
    public static bool grab = false;
    public GameObject sword;
    private Vector3 grabPos;
    private Quaternion grabRot;
    private Vector3 swordPos;
    private Quaternion swordRot;
    private bool isGrab = false;

    // Start is called before the first frame update
    void Start()
    {
        swordPos = sword.transform.localPosition;
        swordRot = sword.transform.localRotation;
        grabPos = grabStart.transform.localPosition;
        grabRot = grabStart.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !grab && !PlayerController.swung)
        {
            grab = true;
            sword.transform.localPosition = grabPos;
            sword.transform.localRotation = grabRot;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && grab && !PlayerController.swung)
        {
            grab = false;
            sword.transform.localPosition = swordPos;
            sword.transform.localRotation = swordRot;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "PushBlock" || other.gameObject.tag == "ForwardBlock") && grab && !isGrab)
        {
            isGrab = true;
            other.gameObject.GetComponent<BlockPush>().held = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "PushBlock" || other.gameObject.tag == "ForwardBlock") && (isGrab && other.gameObject.GetComponent<BlockPush>().held))
        {
            other.gameObject.transform.position = new Vector3(grabStart.transform.position.x, other.gameObject.transform.position.y, grabStart.transform.position.z);
            //other.gameObject.GetComponent<Rigidbody>().velocity = sword.GetComponent<Rigidbody>().velocity;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "PushBlock" || other.gameObject.tag == "ForwardBlock"))
        {
            isGrab = false;
            other.gameObject.GetComponent<BlockPush>().held = false;
        }
    }
}
