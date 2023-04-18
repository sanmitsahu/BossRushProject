using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BlockPush : MonoBehaviour
{
    public float knockBack = 10.0f;
    public float knockBackTimer = 0.2f;
    public bool knocked = false;
    public bool fused = false;
    public bool boosted = false;
    public bool grabbed = false;
    private Vector3 forward;
    public Vector3 originalPos;
    private GameObject player;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;
        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        EventManager.OnRestart += OnDeath;
    }

    void OnDisable()
    {
        EventManager.OnRestart -= OnDeath;
    }

    public void OnDeath()
    {
        knockBackTimer = 0.2f;
        knocked = false;
        transform.position = originalPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (knocked)
        {
            knockBackTimer -= Time.deltaTime;
            if (knockBackTimer <= 0.0f)
            {
                rb.velocity = Vector3.zero;
                knockBackTimer = 0.2f;
                knocked = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
        }

        else if (other.gameObject.tag == "Boss")
        {
            rb.mass = 1f;
            BlockGrab.fj.connectedBody = null;
            knockBackTimer = 0.2f;
            knocked = false;
            rb.velocity = Vector3.zero;
            transform.position = originalPos;
            fused = false;
            boosted = false;
            rb.isKinematic = false;
            GetComponent<Collider>().isTrigger = false;
            GetComponent<MeshRenderer>().material.color = Color.white;
            transform.parent = null;
        }
    }

    void OnTriggerStay(Collider other)
    {
        //UnityEngine.Debug.Log(PlayerController.swung);
        if (other.gameObject.tag == "Punch" && PlayerController.swung && !knocked && !PlayerController.swordHit)
        {
            if (!boosted && !fused)
            {
                //UnityEngine.Debug.Log(PlayerController.swung);
                PlayerPrefs.SetFloat("pushed", PlayerPrefs.GetFloat("pushed", 0) + 1);
                PlayerController.swordHit = true;
                rb.AddForce(other.gameObject.transform.forward * knockBack, ForceMode.Impulse);
                knocked = true;
            }
            else if(boosted || fused)
            {
                rb.mass = 1f;
                transform.parent = null;
                fused = false;
                boosted = false;
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Collider>().isTrigger = false;
                GetComponent<MeshRenderer>().material.color = Color.white;
                transform.position = originalPos;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Boss")
        {
            rb.mass = 1f;
            BlockGrab.fj.connectedBody = null;
            knockBackTimer = 0.2f;
            knocked = false;
            rb.velocity = Vector3.zero;
            transform.position = originalPos;
            rb.isKinematic = false;
            GetComponent<Collider>().isTrigger = false;
            GetComponent<MeshRenderer>().material.color = Color.white;
            transform.parent = null;
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "ForwardBlock")
        {
            GameObject high = col.gameObject;
            if (this.gameObject.tag == "ForwardBlockShort" && ((knocked || grabbed) && !fused && !col.gameObject.GetComponent<BlockPush>().boosted))
            {
                UnityEngine.Debug.Log("hello to you too");
                GameObject low = this.gameObject;
                grabbed = false;

                high.GetComponent<BlockPush>().fused = true;
                low.GetComponent<BlockPush>().boosted = true;

                Vector3 pos = high.transform.position;
                transform.position = new Vector3(pos.x, 1.25f, pos.z);
                col.gameObject.transform.position = new Vector3(pos.x, 0.5f, pos.z);

                high.transform.parent = low.transform;
                high.GetComponent<Rigidbody>().isKinematic = true;
                high.GetComponent<Collider>().isTrigger = true;

                low.GetComponent<MeshRenderer>().material.color = Color.gray;
                high.GetComponent<MeshRenderer>().material.color = Color.gray;
            }
            knockBackTimer = 0.2f;
            knocked = false;
            rb.velocity = Vector3.zero;
            col.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        else if(col.gameObject.tag == "ForwardBlockShort")
        {
            GameObject low = col.gameObject;
            if (this.gameObject.tag == "ForwardBlock" && ((knocked || grabbed) && !boosted && !col.gameObject.GetComponent<BlockPush>().fused))
            {
                UnityEngine.Debug.Log("hello");
                GameObject high = this.gameObject;
                grabbed = false;

                high.GetComponent<BlockPush>().fused = true;
                low.GetComponent<BlockPush>().boosted = true;

                Vector3 pos = low.transform.position;
                low.transform.position = new Vector3(pos.x, 1.25f, pos.z);
                high.transform.position = new Vector3(pos.x, 0.5f, pos.z);

                high.transform.parent = low.transform;
                high.GetComponent<Rigidbody>().isKinematic = true;
                high.GetComponent<Collider>().isTrigger = true;

                low.GetComponent<MeshRenderer>().material.color = Color.gray;
                high.GetComponent<MeshRenderer>().material.color = Color.gray;
            }
            knockBackTimer = 0.2f;
            knocked = false;
            rb.velocity = Vector3.zero;
            col.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
