using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BlockPush : MonoBehaviour
{
    public float knockBack = 10.0f;
    public float knockBackTimer = 0.2f;
    public bool knocked = false;
    public bool grabbed = false;
    private Vector3 forward;
    public Vector3 originalPos;
    private GameObject player;
    private Rigidbody rb;
    private bool respawned = false;
    private Material mat;
    private BoxCollider bcollider;
    //private SphereCollider scollider;
    private bool insideBlock = false;
    private float phaseTimer = -1;
    public GameObject fusedBlock;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;
        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        EventManager.OnRestart += OnDeath;
        mat = gameObject.GetComponent<Renderer>().material;
        bcollider = gameObject.GetComponent<BoxCollider>();
        //scollider = gameObject.GetComponent<SphereCollider>();
        //scollider.enabled = false;
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
        if (phaseTimer> 0)
        {
            phaseTimer -= Time.deltaTime;
            if (phaseTimer <= 0.0f)
            {
                Color newColor = mat.color;
                newColor.a = 1.0f;
                mat.color = newColor;
                rb.isKinematic = false;
                bcollider.isTrigger = false;
                //bcollider.enabled = true;
                //scollider.enabled = false;
                respawned = false;

                Debug.Log("exiting");
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
            rb.isKinematic = false;
            GetComponent<Collider>().isTrigger = false;
            GetComponent<MeshRenderer>().material.color = Color.white;
            Color newColor = mat.color;
            newColor.a = 0.5f;
            GetComponent<MeshRenderer>().material.color = newColor;
            transform.parent = null;
        }
    }
    public void Respawn()
    {
        Debug.Log("Respawning");
        rb.mass = 1f;
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().isTrigger = false;
        GetComponent<MeshRenderer>().material.color = Color.white;
        transform.position = originalPos;

        Color newColor = mat.color;
        newColor.a = 0.5f;
        mat.color = newColor;
        Debug.Log("Color: " + mat.color);
        rb.isKinematic = true;
        bcollider.isTrigger = true;
        phaseTimer = 0.8f;
    }

    void OnTriggerStay(Collider other)
    {
        //UnityEngine.Debug.Log(PlayerController.swung);
        if (other.gameObject.tag == "Punch" && PlayerController.swung && !knocked && !PlayerController.swordHit)
        {
            PlayerPrefs.SetFloat("pushed", PlayerPrefs.GetFloat("pushed", 0) + 1);
            PlayerController.swordHit = true;
            rb.AddForce(other.gameObject.transform.forward * knockBack, ForceMode.Impulse);
            knocked = true;
        }
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Boss")
        {
            phaseTimer = 0.8f;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Boss")
        {
            Debug.Log("HittingForward");
            //Debug.Log("Boosted:" + boosted + "Fused:" + fused);

            rb.mass = 1f;
            BlockGrab.fj.connectedBody = null;
            knockBackTimer = 0.2f;
            knocked = false;
            //transform.parent.GetComponent<BlockPush>().Respawn();
            transform.parent = null;
            rb.velocity = Vector3.zero;
            transform.position = originalPos;
            Color newColor = mat.color;
            newColor.a = 0.5f;
            mat.color = newColor;
            Debug.Log("Color: " + mat.color);
            rb.isKinematic = true;
            bcollider.isTrigger = true;
            phaseTimer = 0.8f;
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "ForwardBlock")
        {
            GameObject high = col.gameObject;
            if (this.gameObject.tag == "ForwardBlockShort" && (knocked || grabbed))
            {
                GameObject low = this.gameObject;
                grabbed = false;
                low.GetComponent<Rigidbody>().mass = 1f;
                high.GetComponent<Rigidbody>().mass = 1f;
                low.GetComponent<Rigidbody>().velocity = Vector3.zero;
                high.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Vector3 newPos = new Vector3(high.transform.position.x, 0.5f, high.transform.position.z);
                fusedBlock.GetComponent<FuseBlock>().highChild = high;
                fusedBlock.GetComponent<FuseBlock>().lowChild = low;
                low.transform.position = new Vector3(5f, 5f, 5f);
                high.transform.position = new Vector3(5f, 8f, 5f);
                fusedBlock.transform.position = newPos;
            }
            knockBackTimer = 0.2f;
            knocked = false;
        }

        else if(col.gameObject.tag == "ForwardBlockShort")
        {
            GameObject low = col.gameObject;
            if (this.gameObject.tag == "ForwardBlock" && (knocked || grabbed))
            {
                GameObject high = this.gameObject;
                grabbed = false;
                low.GetComponent<Rigidbody>().mass = 1f;
                high.GetComponent<Rigidbody>().mass = 1f;
                low.GetComponent<Rigidbody>().velocity = Vector3.zero;
                high.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Vector3 newPos = new Vector3(low.transform.position.x, 0.5f, low.transform.position.z);
                fusedBlock.GetComponent<FuseBlock>().highChild = high;
                fusedBlock.GetComponent<FuseBlock>().lowChild = low;
                low.transform.position = new Vector3(5f, 5f, 5f);
                high.transform.position = new Vector3(5f, 8f, 5f);
                fusedBlock.transform.position = newPos;
            }
            knockBackTimer = 0.2f;
            knocked = false;
        }
    }
}

