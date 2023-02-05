using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject fireBall;
    public static int health = 4;
    public float knockBack = 5.0f;
    public bool fired = false;
    public bool startDelay = true;
    public bool shocked = false;
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
        EventManager.OnRestart += OnDeath;
    }

    void OnDisable()
    {
        EventManager.OnRestart -= OnDeath;
    }

    public void OnDeath()
    {
        st = State.NORMAL;
        knockBackTimer = 0.5f;
        //fired = false;
        startDelay = true;
        health = 4;
        transform.position = originalPos;
        transform.rotation = originalRot;
    }

    IEnumerator Fireball()
    {
        if (startDelay)
        {
            yield return new WaitForSeconds(2.5f);
            startDelay = false;
        }
        GameObject fire = Instantiate(fireBall, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        fired = false;
        Destroy(fire);
    }

    IEnumerator Shock()
    {
        yield return new WaitForSeconds(5.0f);
        if (st == State.NORMAL && shocked)
        {
            shocked = false;
            knockBackTimer = 0.5f;
            health = 4;
            transform.position = originalPos;
            transform.rotation = originalRot;
        }
    }

    IEnumerator Stun()
    {
        startDelay = true;
        fired = true;
        yield return new WaitForSeconds(20.0f);
        st = State.NORMAL;
        health = 4;
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
        if (other.gameObject.tag == "Sword" && PlayerController.swung)
        {
            shocked = false;
            transform.forward = -player.transform.forward;
            st = State.HIT;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PushBlock")
        {
            shocked = false;
            health--;
            if (health == 0)
            {
                st = State.STUN;
            }
            else
            {
                health = 4;
                transform.position = originalPos;
                transform.rotation = originalRot;
                st = State.NORMAL;
            }
        }
        else if (other.gameObject.tag == "ForwardBlock" && st != State.STUN)
        {
            shocked = false;
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
        else if (other.gameObject.tag == "StunBlock" && st != State.STUN)
        {
            health--;
            if (health == 0)
            {
                st = State.STUN;
            }
            else
            {
                st = State.NORMAL;
                shocked = true;
                StartCoroutine(Shock());
            }
        }
        else if (other.gameObject.tag == "Block")
        {
            shocked = false;
            UnityEngine.Debug.Log("Try Again!");
            health = 4;
            transform.position = originalPos;
            transform.rotation = originalRot;
            st = State.NORMAL;
        }
    }
}