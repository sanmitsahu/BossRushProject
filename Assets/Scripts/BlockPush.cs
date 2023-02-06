using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPush : MonoBehaviour
{
    public float knockBack = 5.0f;
    public float knockBackTimer = 0.2f;
    public bool knocked = false;
    private Vector3 forward;
    private Vector3 originalPos;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;
        player = GameObject.FindWithTag("Player");
        EventManager.OnRestart += OnDeath;
    }

    void OnDisable()
    {
        EventManager.OnRestart -= OnDeath;
    }

    public void OnDeath()
    {
        knockBackTimer = 0.2f;
        knocked = false;
        transform.position = originalPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (knocked)
        {
            knockBackTimer -= Time.deltaTime;
            transform.Translate(forward * Time.deltaTime * knockBack, Space.World);
            if (knockBackTimer <= 0.0f)
            {
                knockBackTimer = 0.2f;
                knocked = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" && !knocked)
        {
            forward = player.transform.forward;
            knocked = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Boss")
        {
            knockBackTimer = 0.2f;
            knocked = false;
            transform.position = originalPos;
        }
    }
}
