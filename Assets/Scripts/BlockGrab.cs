using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrab : MonoBehaviour
{
    public GameObject player;
    private GameObject blockChild;
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
        grabPos = transform.localPosition;
        grabRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !grab && !PlayerController.swung)
        {
            grab = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && grab && !PlayerController.swung)
        {
            grab = false;
            sword.transform.localPosition = swordPos;
            sword.transform.localRotation = swordRot;
            if (blockChild)
            {
                blockChild.transform.parent = null;
                blockChild = null;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "PushBlock" || other.gameObject.tag == "ForwardBlock" || other.gameObject.tag == "StunBlock") && grab && !blockChild)
        {
            sword.transform.localPosition = grabPos;
            sword.transform.localRotation = grabRot;
            //blockChild.transform.localPosition = new Vector3(grabStart.transform.localPosition.x, blockChild.transform.localPosition.y, grabStart.transform.localPosition.z);
            blockChild = other.gameObject;
            blockChild.transform.parent = player.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "PushBlock" || other.gameObject.tag == "ForwardBlock" || other.gameObject.tag == "StunBlock") && blockChild)
        {
            blockChild.transform.parent = null;
            blockChild = null;
            //grab = false;
            sword.transform.localPosition = swordPos;
            sword.transform.localRotation = swordRot;
        }
    }
}
