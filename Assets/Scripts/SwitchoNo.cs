using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchoNo : MonoBehaviour
{
    // Start is called before the first frame update
    static public int on_switch_count=0;
    private bool on = false;

    IEnumerator flipSwitch()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        on = true;
        on_switch_count++;
        yield return new WaitForSecondsRealtime(5);
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
        on = false;
        on_switch_count--;


    }

    void OnCollisionEnter(Collision collision)
    {

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player" && !on && PlayerController.switchesValid)
        {
            StartCoroutine(flipSwitch());
            //If the GameObject's name matches the one you suggest, output this message in the console

        }
        else if (collision.gameObject.name == "Player" && !on && PlayerController.switchesValid)
        {
            StartCoroutine(flipSwitch());
        }
        else if (collision.gameObject.tag == "BossSword" && BossSword.swung)
        {
            StartCoroutine(flipSwitch());
            //If the GameObject's name matches the one you suggest, output this message in the console

        }
        else if (collision.gameObject.name == "BossSword" && BossSword.swung)
        {
            StartCoroutine(flipSwitch());
        }


    }

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
