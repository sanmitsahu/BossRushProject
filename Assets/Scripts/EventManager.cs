using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void RestartAction();
    public static event RestartAction OnRestart;

    void Update()
    {
        if (PlayerController.health <= 0)
        {
            if (OnRestart != null)
            {
                OnRestart();
                PlayerController.health = PlayerController.maximumHealth;
            }
        }
    }
}
