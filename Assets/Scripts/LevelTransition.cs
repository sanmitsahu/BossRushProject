using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winScreen;

    public void nextLevel()
    {
        Time.timeScale = 1f;
        winScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
