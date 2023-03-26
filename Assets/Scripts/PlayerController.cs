using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float swordTimer = 0.2f;
    private float damageTimer = 0.2f;
    private bool hit = false;
    public static bool swordHit = false;
    public GameObject sword;
    public GameObject start;
    public static bool swung = false;
    [SerializeField]
    private int originalHealth;
    public static int maximumHealth;
    public static int health;
    public float speed = 5.0f;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 swordPos;
    private Quaternion swordRot;
    private Vector3 startPos;
    private Quaternion startRot;
    private Quaternion originalRot;
    Scene scene;
    public GameOverManager gameOverManager;
    private void Awake()
    {
        maximumHealth = originalHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        scene = SceneManager.GetActiveScene();
        startPos = start.transform.localPosition;
        startRot = start.transform.localRotation;
        swordPos = sword.transform.localPosition;
        swordRot = sword.transform.localRotation;
        
        health = originalHealth;
        EventManager.OnRestart += OnDeath;
    }

    void OnDisable()
    {
        EventManager.OnRestart -= OnDeath;
    }

    public void OnDeath()
    {
        swung = false;
        hit = false;
        //SceneManager.LoadScene(scene.buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (swung)
        {
            swordTimer -= Time.deltaTime;
            if (swordTimer <= 0.0f)
            {
                swordTimer = 0.2f;
                swung = false;
                swordHit = false;
                sword.transform.localPosition = swordPos;
                sword.transform.localRotation = swordRot;
            }
        }

        if (BlockGrab.grab)
        {
            speed = 1.5f;
        }
        else
        {
            speed = 5.0f;
        }

        if (hit)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0.0f)
            {
                damageTimer = 0.2f;
                hit = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !swung && !BlockGrab.grab)
        {
            swung = true;
            sword.transform.localPosition = startPos;
            sword.transform.localRotation = startRot;
        }
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirect = new Vector3(horizontalInput, 0, verticalInput);
        rb.AddForce(moveDirect * speed * Time.deltaTime);

        if (moveDirect != Vector3.zero && !BlockGrab.grab)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirect), 1.0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile" && !hit && !EnemyBehavior.completed)
        {
            health--;
            hit = true;
            if (health <= 0)
            {
                Time.timeScale = 0;
                gameOverManager.SetGameOver();
                Time.timeScale = 0;
            }
        }
    }
}
