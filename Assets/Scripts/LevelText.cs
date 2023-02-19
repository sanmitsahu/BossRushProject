using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelText : MonoBehaviour
{
    public float timeToAppear = 2f;
    public float timeToDisappear;

    void Start()
    {
        timeToDisappear = Time.time + timeToAppear;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeToDisappear)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


}
