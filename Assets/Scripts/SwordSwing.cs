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
    public static bool block = false;
    public static bool blockon = false;
    private bool swinging = false;
    private bool blocking = false;
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
            //UnityEngine.Debug.Log(timer + " " + Time.deltaTime);
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

        if (block)
        {
            if (!blocking)
            {
                transform.localPosition = startPos;
                transform.localRotation = startRot;
                blocking = true;
            }

            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                blocking = false;
                block = false;
                timer = 0.15f;

            }

            StartCoroutine(waiter(originalPos, originalRot));
            
        }
    }
    
    IEnumerator waiter(Vector3 startPos, Quaternion startRot)
    {
        float smooth = 5.0f;
        float tiltAngle = 180.0f;
        float tiltAroundX = Input.GetAxis("Horizontal") * tiltAngle;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, 0);

        // Dampen towards the target rotation
        //transform.localRotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
        //transform.localRotation = Quaternion.Euler(0, 350, 0);
        transform.RotateAround(character.transform.position, Vector3.up, -500.0f * Time.deltaTime);


        //UnityEngine.Debug.Log("start wait");
        yield return new WaitForSecondsRealtime(5);
        //UnityEngine.Debug.Log("done wait");
        blockon = false;
        transform.localPosition = startPos;
        transform.localRotation = startRot;

    }
}

