using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWP = 0;
    int i = 0;
    float dist1;
    float dist2;
    float dist3;    
    float cur_dist = 99999999;
    public float speed = 2.0f;
    private Transform target;
    private Rigidbody rb;

    private void Update()
    {
        target = FindTarget();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Touchable"))
        {
            collision.collider.gameObject.tag = "Untagged"; 
            //Destroy(collision.collider.gameObject);
        }
        target = FindTarget();
    }

    public Transform FindTarget()
    {
        GameObject[] candidates = GameObject.FindGameObjectsWithTag("Touchable");
        float minDistance = Mathf.Infinity;
        Transform closest;

        if (candidates.Length == 0)
        {
            float distance = Vector3.Distance(this.transform.position, GameObject.Find("Player").transform.position);
            if (distance > 1)
                return GameObject.Find("Player").transform;
            return null;
        }

        closest = candidates[0].transform;
        minDistance = Vector3.Distance(this.transform.position, candidates[0].transform.position);
        for (int i = 1; i < candidates.Length; ++i)
        {
            float distance = Vector3.Distance(this.transform.position, candidates[i].transform.position);

            if (distance < minDistance)
            {
                closest = candidates[i].transform;
                minDistance = distance;
            }
        }
        return closest;
    }
}


// Update is called once per frame
/*void Update()
{*/
    /*if(Vector3.Distance(this.transform.position, waypoints[currentWP].transform.position) < 0.5) 
    {
        currentWP++;
    }
    if(currentWP >= waypoints.Length)
    {
        currentWP = 0;
    }
    this.transform.LookAt(waypoints[currentWP].transform);
    this.transform.Translate(0, 0, speed * Time.deltaTime);*/
    /*dist1 = Vector3.Distance(this.transform.position, waypoints[0].transform.position);
    dist2 = Vector3.Distance(this.transform.position, waypoints[1].transform.position);
    dist3 = Vector3.Distance(this.transform.position, waypoints[2].transform.position);

    if(dist1 < dist2 && dist1 < dist3)
    {
        currentWP = 0;
    }else if(dist2 < dist1 && dist2 < dist3)
    {
        currentWP = 1;
    }else
    { 
        currentWP = 2; 
    }

    Debug.Log(currentWP);

    if(currentWP >= waypoints.Length)
    {
        currentWP = 0;
    }
    this.transform.LookAt(waypoints[currentWP].transform);
    this.transform.Translate(0, 0, speed * Time.deltaTime);*/


/*}
}*/