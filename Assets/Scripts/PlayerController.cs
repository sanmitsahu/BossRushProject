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
    public static bool switchesValid = false;
    


    // Start is called before the first frame update
    void Start()
    {
       
    }
    void OnTriggerEnter(Collider collision)
    {
        

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "BossSword" && SwordSwing.blockon)
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            StartCoroutine(waitercolor());
        }
        else if (collision.gameObject.name == "BossSword" && SwordSwing.blockon)
        {
            StartCoroutine(waitercolor());
        }
        

    }
    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.tag == "prop" && SwordSwing.swung)
        {

            Vector3 pos = gameObject.transform.position;
            Vector3 col = collision.gameObject.transform.position;
            Vector3 mov = col-pos;
            mov.Normalize();
            collision.gameObject.GetComponent<Rigidbody>().AddForce(mov * 100);






        }

    }
    IEnumerator waitercolor()
    {
        swordPrefab.GetComponent<Renderer> ().material.color = Color.yellow;
        switchesValid = true;
        
        yield return new WaitForSecondsRealtime(5);
        switchesValid = false;
        swordPrefab.GetComponent<Renderer> ().material.color = Color.grey;
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
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && swordPrefab.GetComponent <SwordSwing>().timer == 0.5f)
        {
            SwordSwing.block = true;
            SwordSwing.blockon = true;
        }
    }
}
