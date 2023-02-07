using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public GameObject fireBall;
    public Light light;
    public static int health = 6;
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
    }
    public static State st = State.NORMAL;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
        EventManager.OnRestart += OnDeath;
        light.intensity = 0.0f;
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
        health = 6;
        transform.position = originalPos;
        transform.rotation = originalRot;
    }

    IEnumerator Fireball()
    {
        if (startDelay)
        {
            yield return new WaitForSeconds(1.0f);
            startDelay = false;
        }
        light.intensity = 20.0f;
        yield return new WaitForSeconds(0.5f);
        light.intensity = 0.0f;
        GameObject fire = Instantiate(fireBall, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        fired = false;
        Destroy(fire);
    }

    // Update is called once per frame
    void Update()
    {
        if (!fired && health > 0)
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
                health = 6;
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
                transform.position = originalPos;
                transform.rotation = originalRot;
                health = 6;
                st = State.NORMAL;
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
            if (health <= 0)
            {
                UnityEngine.Debug.Log("You beat the boss!");
                Destroy(gameObject);
            }
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
                UnityEngine.Debug.Log("You beat the boss!");
                Destroy(gameObject);
            }
            else
            {
                transform.position = originalPos;
                transform.rotation = originalRot;
                shocked = false;
                health = 6;
                st = State.NORMAL;
            }
        }
        else if (other.gameObject.tag == "ForwardBlock")
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
                UnityEngine.Debug.Log("You beat the boss!");
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.tag == "StunBlock" && !DisableShock.drained && !shocked)
        {
            knockBackTimer = 0.5f;
            health--;
            if (health <= 0)
            {
                UnityEngine.Debug.Log("You beat the boss!");
                Destroy(gameObject);
            }
            else
            {
                st = State.NORMAL;
                shocked = true;
            }
        }
        else if (other.gameObject.tag == "Block")
        {
            transform.position = originalPos;
            transform.rotation = originalRot;
            st = State.NORMAL;
            startDelay = true;
            shocked = false;
            shockTimer = 5.0f;
            health = 6;
        }
    }
}