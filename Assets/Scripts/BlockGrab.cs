using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrab : MonoBehaviour
{
    public GameObject player;
    private GameObject blockChild;
    private Transform originalParent;
    public static bool grab = false;
    public GameObject sword;
    private Vector3 grabPos;
    private Quaternion grabRot;
    private Vector3 swordPos;
    private Quaternion swordRot;
    public static bool isGrab = false;
    public static FixedJoint fj;

    // Start is called before the first frame update
    void Start()
    {
        swordPos = sword.transform.localPosition;
        swordRot = sword.transform.localRotation;
        grabPos = transform.localPosition;
        grabRot = transform.localRotation;
        originalParent = null;
        fj = this.GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !grab && !PlayerController.swung)
        {
            grab = true;
        }
        else if ((Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) && grab && !PlayerController.swung)
        {
            isGrab = false;
            grab = false;
            sword.transform.localPosition = swordPos;
            sword.transform.localRotation = swordRot;
            if (blockChild)
            {
                blockChild.GetComponent<BlockPush>().grabbed = false;
                blockChild.GetComponent<Rigidbody>().mass = 1f;
                blockChild.transform.parent = originalParent;
                blockChild = null;
                FixedJoint fj = this.GetComponent<FixedJoint>();
                fj.connectedBody = null;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<BlockPush>() && grab && !blockChild && !other.gameObject.GetComponent<BlockPush>().grabbed)
        {
            UnityEngine.Debug.Log("Hel");
            isGrab = true;
            PlayerPrefs.SetFloat("pulled",PlayerPrefs.GetFloat("pulled", 0)+1);

            sword.transform.localPosition = grabPos;
            sword.transform.localRotation = grabRot;

            blockChild = other.gameObject;
            originalParent = blockChild.transform.parent;
            blockChild.transform.parent = player.transform;
            FixedJoint fj = this.GetComponent<FixedJoint>();
            fj.connectedBody = other.GetComponent<Rigidbody>();

            other.GetComponent<Rigidbody>().mass = 0.00000001f;
            other.gameObject.GetComponent<BlockPush>().grabbed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<BlockPush>() && blockChild)
        {
            other.GetComponent<Rigidbody>().mass = 1f;
            blockChild.transform.parent = originalParent;
            blockChild = null;
            grab = false;
            sword.transform.localPosition = swordPos;
            sword.transform.localRotation = swordRot;
            other.gameObject.GetComponent<BlockPush>().grabbed = false;
            FixedJoint fj = this.GetComponent<FixedJoint>();
            fj.connectedBody = null;
        }
    }
}
