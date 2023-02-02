using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public static int health = 3;
    public float intervalTimer = 5.0f;
    public float knockBack = 5.0f;
    public float stunTimer = 5.0f;
    public static float knockBackTimer = 0.5f;
    private Vector3 originalPos;
    private Quaternion originalRot;

    public enum State
    {
        NORMAL,
        HIT,
        COMBO,
        STUN
    }
    State st = State.NORMAL;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (st != State.STUN)
        {
            intervalTimer -= Time.deltaTime;
            if (intervalTimer <= 0.0f)
            {
                BossSword.swung = true;
                intervalTimer = 5.0f;
            }
        }

        if (st == State.HIT)
        {
            knockBackTimer -= Time.deltaTime;
            transform.Translate(-transform.forward * Time.deltaTime * knockBack, Space.World);
            if (knockBackTimer <= 0.0f)
            {
                knockBackTimer = 0.5f;
                st = State.NORMAL;
                transform.position = originalPos;
                transform.rotation = originalRot;
            }
        }
        else if (st == State.COMBO)
        {
            transform.Translate(transform.forward * Time.deltaTime * knockBack, Space.World);
        }
        else if (st == State.STUN)
        {
            stunTimer -= Time.deltaTime;
            BossSword.swung = false;
            intervalTimer = 5.0f;
            if (stunTimer <= 0.0f)
            {
                stunTimer = 5.0f;
                st = State.NORMAL;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" && SwordSwing.swung && st == State.NORMAL)
        {
            transform.forward = -player.transform.forward;
            st = State.HIT;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Block")
        {
            health--;
            if (health == 0)
            {
                st = State.STUN;
            }
            else
            {
                UnityEngine.Debug.Log("Try Again!");
                health = 3;
                transform.position = originalPos;
                transform.rotation = originalRot;
                st = State.NORMAL;
            }
        }
        else if (other.gameObject.tag == "moveBlock" && (st == State.HIT || st == State.COMBO))
        {
            if (st == State.HIT)
            {
                knockBackTimer = 0.5f;
                st = State.COMBO;
            }
            health--;
            if (health == 0)
            {
                st = State.STUN;
            }
            Destroy(other.gameObject);
        }
    }
}