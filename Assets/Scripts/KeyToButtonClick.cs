using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeyToButtonClick : MonoBehaviour
{
    public Button targetButton;
    public KeyCode triggerKey;

    void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            // Simulate button click
            ExecuteEvents.Execute(targetButton.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }
}
