using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchoNo : MonoBehaviour
{
    // Start is called before the first frame update
    static public int on_switch_count=0;
    public bool on = false;
    public ChangeColor zone;
    

    /*IEnumerator flipSwitch()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        on = true;
        on_switch_count++;
        yield return new WaitForSecondsRealtime(25);
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
        on = false;
        on_switch_count--;


    }*/
    public void flipOn()
    {
        gameObject.GetComponent<Renderer>().material.color = zone.onColor ;
        on = true;
        on_switch_count++;
        zone.TurnOn();

    }

    public void flipOff()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
        on = false;
        on_switch_count--;
    }

    void OnCollisionEnter(Collision collision)
    {

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player" && !on && PlayerController.switchesValid)
        {
            flipOn();
            //If the GameObject's name matches the one you suggest, output this message in the console

        }
        else if (collision.gameObject.name == "Player" && !on && PlayerController.switchesValid)
        {
            flipOn();
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BossSword" && !on && BossSword.swung)
        {
            flipOn();
            //If the GameObject's name matches the one you suggest, output this message in the console

        }
        else if (other.gameObject.name == "BossSword" && !on && BossSword.swung)
        {
            flipOn();
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
