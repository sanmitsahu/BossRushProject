using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void RestartAction();
    public static event RestartAction OnRestart;

    void OnDeath()
    {
        if (PlayerController.health == 0)
        {
            UnityEngine.Debug.Log("ENACTED!");
            if (OnRestart != null)
            {
                OnRestart();
            }
        }
    }
}
