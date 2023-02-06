using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableShock : MonoBehaviour
{
    public static bool drained = false;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnRestart += OnDeath;
    }

    void OnDisable()
    {
        EventManager.OnRestart -= OnDeath;
    }

    public void OnDeath()
    {
        drained = false;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Boss")
        {
            drained = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Boss")
        {
            UnityEngine.Debug.Log("SHOCK");
            drained = false;
        }
    }
}
