using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public GameObject character;
    public GameObject start;
    public float swingSpeed = 1000.0f;
    public float timer = 0.15f;
    public static bool swung = false;
    private bool swinging = false;
    private Quaternion originalRot;
    private Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalRot = transform.localRotation;
        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = start.transform.localPosition;
        Quaternion startRot = start.transform.localRotation;
        if (swung)
        {
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
                timer = 0.15f;
                transform.localPosition = originalPos;
                transform.localRotation = originalRot;
            }
            transform.RotateAround(character.transform.position, Vector3.up, -swingSpeed * Time.deltaTime);
        }
    }
}

