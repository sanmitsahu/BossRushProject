using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public GameObject character;
    public GameObject start;
    public float swingSpeed = 1000.0f;
    public float timer = 0.5f;
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
        else
        {
            transform.localPosition = originalPos;
            transform.localRotation = originalRot;
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
                timer = 0.5f;

            }

            StartCoroutine(waiter(originalPos, originalRot));
        }
        
    }
   IEnumerator waiter(Vector3 startPos, Quaternion startRot)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0,-90,0));
        yield return new WaitForSecondsRealtime(2);
        //transform.RotateAround(character.transform.position, Vector3.up, -500.0f * Time.deltaTime);
        //UnityEngine.Debug.Log("start wait");
        //UnityEngine.Debug.Log("done wait");
        blockon = false;
        transform.localPosition = startPos;
        transform.localRotation = startRot;

    }
}

