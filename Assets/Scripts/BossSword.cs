using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BossSword : MonoBehaviour
{
    private Vector3 playerPos;
    private Vector3 bossPos;
    private Rigidbody rb;
    public float firingSpeed = 1.0f;
    public float accel = 0.01f;
    // Start is called before the first frame update

    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform.position;
        bossPos = GameObject.FindWithTag("Boss").transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
        EventManager.OnRestart += OnDeath;
    }

    void OnDisable()
    {
        EventManager.OnRestart -= OnDeath;
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direct = (playerPos - bossPos).normalized;
        rb.velocity = direct * firingSpeed;
        firingSpeed += accel;
    }

}

