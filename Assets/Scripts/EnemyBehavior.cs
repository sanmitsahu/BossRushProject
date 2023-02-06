using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject fireBall;
    public static int health = 5;
    public float knockBack = 5.0f;
    public bool shocked = false;
    public bool fired = false;
    public bool startDelay = true;
    public static float knockBackTimer = 0.5f;
    public float shockTimer = 5.0f;
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
        shocked = false;
        st = State.NORMAL;
        knockBackTimer = 0.5f;
        startDelay = true;
        health = 5;
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

    IEnumerator Stun()
    {
        startDelay = true;
        fired = true;
        yield return new WaitForSeconds(5.0f);
        st = State.NORMAL;
        health = 5;
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

        if (shocked && DisableShock.drained)
        {
            shockTimer -= Time.deltaTime;
            if (shockTimer <= 0.0f)
            {
                shocked = false;
                shockTimer = 5.0f;
                knockBackTimer = 0.5f;
                health = 5;
                transform.position = originalPos;
                transform.rotation = originalRot;
            }
        }

        if (st == State.HIT)
        {
            knockBackTimer -= Time.deltaTime;
            transform.Translate(-transform.forward * Time.deltaTime * knockBack, Space.World);
            if (knockBackTimer <= 0.0f)
            {
                knockBackTimer = 0.5f;
                if (health <= 0)
                {
                    st = State.STUN;
                    StartCoroutine(Stun());
                }
                else
                {
                    health = 5;
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
            health--;
            shocked = false;
            shockTimer = 5.0f;
            transform.forward = -player.transform.forward;
            st = State.HIT;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PushBlock")
        {
            health--;
            if (health <= 0)
            {
                st = State.STUN;
                StartCoroutine(Stun());
            }
            else
            {
                shocked = false;
                health = 5;
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
            if (health <= 0)
            {
                st = State.STUN;
                StartCoroutine(Stun());
            }
        }
        else if (other.gameObject.tag == "StunBlock" && st != State.STUN && !DisableShock.drained && !shocked)
        {
            knockBackTimer = 0.5f;
            health--;
            if (health <= 0)
            {
                st = State.STUN;
                StartCoroutine(Stun());
            }
            else
            {
                st = State.NORMAL;
                shocked = true;
            }
        }
        else if (other.gameObject.tag == "Block")
        {
            shocked = false;
            shockTimer = 5.0f;
            health = 5;
            transform.position = originalPos;
            transform.rotation = originalRot;
            st = State.NORMAL;
        }
    }
}