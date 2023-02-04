using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Start is called before the first frame update

    public Color onColor;
    public SwitchoNo corSwitch;

    void OnTriggerEnter(Collider collider)

    {
        /*if (collider.gameObject.name == "Player")
        {
            GetComponent<MeshRenderer>().material.color = onColor;
            this.tag = "Touchable";
        }
        else*/ if (collider.gameObject.name == "Boss")
        {
            GetComponent<MeshRenderer>().material.color = Color.grey;
            corSwitch.flipOff();
            this.tag = "Untagged";
        }
    }
    public void TurnOn()
    {
        GetComponent<MeshRenderer>().material.color = onColor;
        this.tag = "Touchable";
    }
   
}
