using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public float timeToAppear = 2f;
    public float timeToDisappear;
    [SerializeField] GameObject gameStartScreen;

    void Start()
    {
        timeToDisappear = Time.time + timeToAppear;
        gameStartScreen.SetActive(true);
        //Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= timeToDisappear)
        {
            UnityEngine.Debug.Log("here");
            gameStartScreen.SetActive(false);
            //Time.timeScale = 1;
        }
    }
}
