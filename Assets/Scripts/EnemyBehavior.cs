using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float intervalTimer = 2.0f;
    public float knockBack = 10.0f;
    public static bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hit)
        {
            intervalTimer -= Time.deltaTime;
            if (intervalTimer <= 0.0f)
            {
                BossSword.swung = true;
                intervalTimer = 2.0f;
            }
        }
        else
        {
            BossSword.swung = false;
            intervalTimer = 1.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Sword" && SwordSwing.swung && !hit)
        {
            rb.AddForce((player.transform.forward * knockBack) + (player.transform.up * knockBack));
            hit = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Plane")
        {
            hit = false;
        }
    }
}
