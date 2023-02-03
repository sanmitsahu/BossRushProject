using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject fireBall;
    public static int health = 3;
    public float knockBack = 5.0f;
    public bool fired = false;
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
    public static State st = State.NORMAL;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    IEnumerator Fireball()
    {
        GameObject fire = Instantiate(fireBall, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        fired = false;
        Destroy(fire);
    }

    IEnumerator Stun()
    {
        fired = true;
        yield return new WaitForSeconds(20.0f);
        st = State.NORMAL;
        health = 3;
        transform.position = originalPos;
        transform.rotation = originalRot;
    }

    // Update is called once per frame
    void Update()
    {
        if (st != State.STUN && !fired && health != 0)
        {
            fired = true;
            StartCoroutine(Fireball());
        }

        if (st == State.HIT)
        {
            knockBackTimer -= Time.deltaTime;
            transform.Translate(-transform.forward * Time.deltaTime * knockBack, Space.World);
            if (knockBackTimer <= 0.0f)
            {
                knockBackTimer = 0.5f;
                if (health == 0)
                {
                    st = State.STUN;
                }
                else
                {
                    st = State.NORMAL;
                    transform.position = originalPos;
                    transform.rotation = originalRot;
                }
            }
        }
        else if (st == State.COMBO)
        {
            transform.Translate(transform.forward * Time.deltaTime * knockBack/2.0f, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" && PlayerController.swung && (st == State.NORMAL || st == State.STUN))
        {
            transform.forward = -player.transform.forward;
            st = State.HIT;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Block" || other.gameObject.tag == "PushBlock")
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
        else if (other.gameObject.tag == "ForwardBlock" && st != State.STUN)
        {
            if (st == State.HIT || st == State.NORMAL)
            {
                knockBackTimer = 0.5f;
                st = State.COMBO;
            }
            health--;
            transform.forward = other.gameObject.transform.forward;
            if (health == 0)
            {
                st = State.STUN;
                StartCoroutine(Stun());
            }
        }
    }
}