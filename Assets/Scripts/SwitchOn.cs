using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOn : MonoBehaviour
{
    public static bool on;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(0.06f, 0.17f, 0.59f);
        on = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            on = true;
            ChaseBlock.chasing = true;
            gameObject.GetComponent<Renderer>().material.color = Color.cyan;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Boss")
        {
            on = false;
            gameObject.GetComponent<Renderer>().material.color = new Color(0.06f, 0.17f, 0.59f);
        }
    }
}
