using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public float intervalTimer = 2.0f;
    public float knockBack = 10.0f;
    public static bool hit = false;
    public static float knockBackTimer = 0.25f;
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
            knockBackTimer -= Time.deltaTime;
            transform.Translate(-transform.forward * Time.deltaTime * knockBack, Space.World);
            BossSword.swung = false;
            intervalTimer = 1.0f;
            if (knockBackTimer <= 0.0f)
            {
                knockBackTimer = 0.25f;
                hit = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Sword" && SwordSwing.swung && !hit)
        {
            transform.forward = -player.transform.forward;
            hit = true;
        }
    }
}