
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverScreen.SetActive(false);
    }
}
