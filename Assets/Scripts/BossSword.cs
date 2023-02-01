using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BossSword : MonoBehaviour
{
    private GameObject start;
    public float timer = 0.25f;
    public static bool swung = false;
    // Start is called before the first frame update
    void Start()
    {
        start = GameObject.Find("PlayerCamera/Player/start");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = start.transform.localPosition;
        transform.localRotation = start.transform.localRotation;
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            swung = false;
            timer = 0.25f;
        }

        if (EnemyBehavior.hit)
        {
            swung = false;
            timer = 0.25f;
            Destroy(gameObject);
        }
    }
}

