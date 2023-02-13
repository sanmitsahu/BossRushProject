using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBlock : MonoBehaviour
{
    private Vector3 switchPos;
    private Vector3 bossPos;
    public static bool chasing = false;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        switchPos = GameObject.FindWithTag("Switch").transform.position;
        bossPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SwitchOn.on && chasing)
        {
            Vector3 direct = (switchPos - bossPos).normalized;
            rb.velocity = direct * 1.0f;
        }       
    }
}
