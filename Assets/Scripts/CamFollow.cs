using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 5.0f, Player.transform.position.z - 3.0f);
    }
}
