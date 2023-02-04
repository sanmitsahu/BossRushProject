using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Port : MonoBehaviour
{
    private int switch_count = 3;
    private bool open =false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
    }

    private void OnCollisionStay(Collision collision)
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
        if (SwitchoNo.on_switch_count >= switch_count)
        {
            
            open = true;
        }
        else
        {
            open = false;
        }
        if (open) { gameObject.GetComponent<Renderer>().material.color = Color.cyan; }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.gray;
        }
    }
}
