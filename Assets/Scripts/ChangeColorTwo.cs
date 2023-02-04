using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorTwo : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
            this.tag = "Touchable";
        }
        else if (collider.gameObject.name == "Boss")
        {
            GetComponent<MeshRenderer>().material.color = Color.grey;
            this.tag = "Untagged";
        }
    }
    
}
