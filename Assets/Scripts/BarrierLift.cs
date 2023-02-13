using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierLift : MonoBehaviour
{
    private Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (SwitchOn.on)
        {
            transform.position = originalPos + new Vector3(40, 0, 0);
        }
        else
        {
            transform.position = originalPos;
        }
    }
}
