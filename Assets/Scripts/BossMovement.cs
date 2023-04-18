using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    private float updateRate = 2.0f;
    private float updateTimer;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        updateTimer = updateRate;
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer -= Time.deltaTime;
        if (updateTimer <= 0)
        {
            Debug.Log("moving");
            if (EnemyBehavior.st == EnemyBehavior.State.NORMAL && !EnemyBehavior.wallTouch)
            {
                
                gameObject.GetComponent<Rigidbody>().velocity = (gameObject.transform.position - player.transform.position).normalized * 0.2f;
            }

            updateTimer = updateRate;

        }
        

    }
}
