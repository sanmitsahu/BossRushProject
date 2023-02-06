using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Port : MonoBehaviour
{
    private bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
        EventManager.OnRestart += OnDeath;
    }

    void OnDisable()
    {
        EventManager.OnRestart -= OnDeath;
    }

    public void OnDeath()
    {
        open = false;
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boss" && open)
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("You win!");
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyBehavior.health <= 0)
        {
            open = true;
            gameObject.GetComponent<Renderer>().material.color = Color.cyan;
        }
        else
        {
            open = false;
            gameObject.GetComponent<Renderer>().material.color = Color.gray;
        }
    }
}
