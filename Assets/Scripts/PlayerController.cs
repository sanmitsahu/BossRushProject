using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject swordPrefab;
    public GameObject start;
    public float speed = 5.0f;
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

        transform.Translate(moveDirect * speed * Time.deltaTime, Space.World);



        if (moveDirect != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirect), 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !SwordSwing.swung)
        {
            SwordSwing.swung = true;
            GameObject sword = Instantiate(swordPrefab, start.transform.position, start.transform.rotation);
            sword.transform.parent = transform;
        }
    }
}
