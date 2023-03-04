using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{
    public float timeToAppear = 2f;
    public float timeToDisappear;
    [SerializeField] GameObject gameStartScreen;

    void Start()
    {
        timeToDisappear = Time.time + timeToAppear;
        //gameStartScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeToDisappear)
        {
            //UnityEngine.Debug.Log("Level Start");
            //gameStartScreen.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
