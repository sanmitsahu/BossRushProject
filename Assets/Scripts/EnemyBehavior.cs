using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    public float intervalTimer = 4.0f;
    public float knockBack = 10.0f;
    public static bool hit = false;
    public static float knockBackTimer = 0.25f;
    private float projASpeed = 0.0001f;
    private float projRSpeed = 0.00005f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void ShootRadial()
    {
        Vector3 pos = gameObject.transform.position;
        Vector3 unitVector = new Vector3(1, 0, 0);
        for (int d = 0; d<360; d += 45)
        {
            
            Vector3 f = Quaternion.AngleAxis(d, Vector3.up) * unitVector;
            UnityEngine.Debug.Log(f);
            var instance = Instantiate(projectile, pos + f, new Quaternion());
            instance.GetComponent<Rigidbody>().AddForce(f*projRSpeed);
        }
    }
    void ShootAimed()
    {
        Vector3 pos = gameObject.transform.position;
        Vector3 ppos = player.transform.position;
        Vector3 mov = ppos - pos;
        mov.Normalize();
        var instance = Instantiate(projectile, pos + mov, new Quaternion());
        instance.GetComponent<Rigidbody>().AddForce(mov * projASpeed);
    }
    void Update()
    {
        if (!hit)
        {
            intervalTimer -= Time.deltaTime;
            if (intervalTimer <= 0.0f)
            {
                ShootRadial();
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            knockBackTimer = 0.0f;
        }
    }
}