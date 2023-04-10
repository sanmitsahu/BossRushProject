using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{

    public void RunLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
