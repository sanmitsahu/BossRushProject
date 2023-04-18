using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideTip : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject popUpScreen;
    public GameObject popupButtonParent;

    void Start()
    {
        popUpScreen.SetActive(true);
        Time.timeScale = 0;
        popupButtonParent.SetActive(false);
        if (popUpScreen == null)
        {
            Debug.LogError("popUpScreen is not assigned.");
        }
    }

    void Update()
    {

    }

    public void Hide_popup()
    {
        Debug.Log("Continue clicked");

        if (popUpScreen.activeSelf)
        {
            popUpScreen.SetActive(false);
            Time.timeScale = 1;
            popupButtonParent.SetActive(false);
        }
        else
        {
            Debug.LogWarning("popUpScreen is not active.");
        }
    }

    public void Show_popup()
    {
        popUpScreen.SetActive(true);
        Time.timeScale = 0;
        popupButtonParent.SetActive(false);
    }
}
