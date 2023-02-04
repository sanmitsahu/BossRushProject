using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementScript : MonoBehaviour
{
    PlayerHealth playerhealth;
    bool gameHasEnded = false;
    // Start is called before the first frame update
    void Start()
    {
        playerhealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerhealth.curHealth <= 0)
        {
            Debug.Log("Game Over");
            EndGame();
        }
            
    }

    void EndGame()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded= true;
            Invoke("RestartCurrentLevel", 2f);
        }
    }

    void RestartCurrentLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
