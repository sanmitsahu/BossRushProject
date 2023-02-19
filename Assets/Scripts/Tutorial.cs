using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tutorialText;
    private string triggerTag= null;
    
    void Start()
    {
        
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        TextMeshProUGUI textPane = tutorialText.GetComponentInChildren<TextMeshProUGUI>();
        if (other.gameObject.tag== "PushBlock")
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
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == triggerTag)
        {
            TextMeshProUGUI textPane = tutorialText.GetComponentInChildren<TextMeshProUGUI>();
            Debug.Log(textPane);
            textPane.text = "";
            triggerTag = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
