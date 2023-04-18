
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;

    public void SetGameOver()
    {
        gameOverScreen.SetActive(true);
    }
  
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //PlayerController.swung = false;
        SwitchOn.on = false;
        gameOverScreen.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
