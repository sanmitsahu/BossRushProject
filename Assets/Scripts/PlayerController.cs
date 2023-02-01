using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject swordPrefab;
    public GameObject start;
    Rigidbody rb;
    public float speed = 5.0f;
    private bool grounded = true;
    public float turnSpeed = 1080.0f;
    private float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "BossSword")
        {
            UnityEngine.Debug.Log("You Lose!");
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        Vector3 moveDirect = new Vector3(horizontalInput, 0, 0);
        moveDirect.Normalize();

        transform.Translate(moveDirect * speed * Time.deltaTime, Space.World);



        if (moveDirect != Vector3.zero)
        {
            transform.forward = moveDirect;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            grounded = false;
            rb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Z) && !SwordSwing.swung)
        {
            SwordSwing.swung = true;
            GameObject sword = Instantiate(swordPrefab, start.transform.localPosition, Quaternion.identity);
            sword.transform.parent = transform;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Plane")
        {
            grounded = true;
        }
    }
}
