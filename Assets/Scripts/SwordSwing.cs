using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public float timer = 0.25f;
    public static bool swung = false;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("SLASH!");
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            swung = false;
            timer = 0.25f;
            Destroy(gameObject);
        }
    }
}

