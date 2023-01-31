using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject swordPrefab;
    public float speed = 3.0f;
    public float turnSpeed = 1080.0f;
    private float horizontalInput;
    private float verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirect = new Vector3(horizontalInput, 0, verticalInput);
        moveDirect.Normalize();

        transform.Translate(moveDirect * speed * Time.deltaTime, Space.World);



        if (moveDirect != Vector3.zero)
        {
            //Quaternion toRot = Quaternion.LookRotation(moveDirect, Vector3.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, turnSpeed * Time.deltaTime);
            transform.forward = moveDirect;
        }

        if (Input.GetKeyDown(KeyCode.Space) && swordPrefab.GetComponent <SwordSwing>().timer == 0.5f)
        {
            SwordSwing.swung = true;
        }
    }
}
