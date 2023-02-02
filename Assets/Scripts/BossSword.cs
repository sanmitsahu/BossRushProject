using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BossSword : MonoBehaviour
{
    public float firingSpeed = 10.0f;
    public float timer = 5.0f;
    public static bool swung = false;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * firingSpeed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            timer = 5.0f;
            swung = false;
            Destroy(gameObject);
        }
    }
}

