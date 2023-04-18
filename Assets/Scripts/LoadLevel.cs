using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{

    public void RunLevel(string levelName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelName);
    }
}
