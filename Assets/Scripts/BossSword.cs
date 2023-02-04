using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BossSword : MonoBehaviour
{
    public GameObject character;
    public GameObject start;
    public float swingSpeed = 1000.0f;
    public float timer = 0.5f;
    public static bool swung = false;
    private bool swinging = false;
    private Quaternion originalRot;
    private Vector3 originalPos;
    PlayerHealth playerhealth;

    
    // Start is called before the first frame update


    void Start()
    {
        originalRot = transform.localRotation;
        originalPos = transform.localPosition;
        playerhealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = start.transform.localPosition;
        if (swung)
        { 
            Quaternion startRot = start.transform.localRotation;
            if (!swinging)
            {
                transform.localPosition = startPos;
                transform.localRotation = startRot;
                swinging = true;
            }
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                swinging = false;
                swung = false;
                timer = 0.5f;
                transform.localPosition = originalPos;
                transform.localRotation = originalRot;
            }
            transform.RotateAround(character.transform.position, Vector3.up, -swingSpeed * Time.deltaTime);
        }

        if (EnemyBehavior.hit)
        {
            swinging = false;
            swung = false;
            timer = 0.5f;
            transform.localPosition = originalPos;
            transform.localRotation = originalRot;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "Player")
        {
            
            playerhealth.TakeDamage(20);
        }
    }

}

