using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject textBox;
    public string spotText;
    private TextMeshProUGUI textPane;


    void Start()
    {
        textPane = textBox.GetComponentInChildren<TextMeshProUGUI>();



    }
    private void OnTriggerEnter(Collider other)
    {
        
        /*if (other.gameObject.tag== "PushBlock")
        {

            
            
            textPane.text =  "Press [Space] to push things";
            triggerTag = other.gameObject.tag;
            
        }
        else if(other.gameObject.tag == "ForwardBlock")
        {
            textPane.text = "These blocks can push the enemy in the direction of the arrow";
            triggerTag = other.gameObject.tag;
        }
        else if (other.gameObject.tag == "Boss")
        {
            textPane.text = "Hit the boss into white blocks to hurt him";
            triggerTag = other.gameObject.tag;
        }*/
        if (other.gameObject.tag == "Player")
        {
            
            textPane.text = spotText;
        }

    }
    private void OnTriggerExit(Collider other)
    {

        //Debug.Log(textPane.text);
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player" && textPane.text==spotText)
        {

            
            
            textPane.text = "";
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
