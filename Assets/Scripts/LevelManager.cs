using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool isGameOver = false;
    //public Text gameText;
    //public AudioClip gameOverSFX;
    //public AudioClip gameWonSFX;
    public string nextLevel;
    public static int totalMemories = 0;
    public Text memoryCount;

    void Start()
    {
        // game is not over upon starting
        isGameOver = false;
        // set UI and timers
        //gameText.gameObject.SetActive(false);
        totalMemories = 0;
        memoryCount.text = "memories: " + totalMemories; 
    }

    void Update()
    {
        // if the game is not over, count down by seconds until game ends and level is lost
        // update timer text
        if (!isGameOver)
        {/*
            if (countDown > 0)
            {
                countDown -= Time.deltaTime; //subtract time since the last frame
            }
            else
            {
                countDown = 0.0f;
                LevelLost();
            }

           */
        }
    }

    // called when health reaches 0
    public void LevelLost()
    {
        // when level lost, display game over text
        // play audio and reload level
        isGameOver = true;
        //gameText.text = "GAME OVER!";
        //gameText.gameObject.SetActive(true);
        //AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

        Invoke("LoadCurrentLevel", 1);
    }

    public void LevelBeat()
    {
        // when level beaten, display appropriate message
        // play audio and load next level
        isGameOver = true;
        /*if (SceneManager.GetActiveScene().name == "Level1")
        {
            gameText.text = "LEVEL 1 COMPLETE!";
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            gameText.text = "LEVEL 2 COMPLETE!";
        }
        else
        {
            gameText.text = "YOU WIN!";
        }*/
        //gameText.gameObject.SetActive(true);

        //AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);
        if (!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadNextLevel", 1);
        }

    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateMemoryCountText()
    {
        totalMemories++;

        memoryCount.text = "memories: " + totalMemories;
    }
}
