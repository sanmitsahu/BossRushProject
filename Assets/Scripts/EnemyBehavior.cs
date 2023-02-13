using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    private GameObject player;
    public GameObject fireBall;
    public Light light;
    public int health = 6;
    public float knockBack = 5.0f;
    public bool wallTouch = false;
    public bool fired = false;
    public bool startDelay = true;
    public float knockBackTimer = 0.5f;
    public float shockTimer = 5.0f;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private Rigidbody rb;

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
        player = GameObject.FindWithTag("Player");
        originalPos = transform.position;
        originalRot = transform.rotation;
        EventManager.OnRestart += OnDeath;
        light.intensity = 0.0f;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void OnDisable()
    {
        EventManager.OnRestart -= OnDeath;
    }

    public void OnDeath()
    {
        Restart();
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

        if (wallTouch)
        {
            shockTimer -= Time.deltaTime;
            if (shockTimer <= 0.0f)
            {
                Restart();
            }
        }

        if (st == State.HIT)
        {
            knockBackTimer -= Time.deltaTime;
            //transform.Translate(-transform.forward * Time.deltaTime * knockBack, Space.World);
            if (knockBackTimer <= 0.0f)
            {
                Restart();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" && PlayerController.swung)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(other.gameObject.transform.forward * knockBack, ForceMode.Impulse);
            st = State.HIT;
        }
    }

    private void Restart()
    {
        rb.velocity = Vector3.zero;
        transform.position = originalPos;
        transform.rotation = originalRot;
        st = State.NORMAL;
        startDelay = true;
        wallTouch = false;
        knockBackTimer = 0.5f;
        health = 6;
        shockTimer = 5.0f;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PushBlock")
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                Restart();
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
            rb.velocity = Vector3.zero;
            rb.AddForce(other.gameObject.transform.forward * knockBack/2.0f, ForceMode.Impulse);
            if (health <= 0)
            {
                UnityEngine.Debug.Log("You beat the boss!");
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.tag == "StunBlock")
        {
            if (!wallTouch)
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
                    wallTouch = true;
                }
            }
            else
            {
                Restart();
            }
        }
        else if (other.gameObject.tag == "Block")
        {
            Restart();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "StunBlock")
        {
            wallTouch = false;
        }
    }
}