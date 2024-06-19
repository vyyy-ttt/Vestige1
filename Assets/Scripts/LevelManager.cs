using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool isGameOver = false;
    //public Text gameText; // currently with Player Attack
    //public AudioClip gameOverSFX; 
    //public AudioClip gameWonSFX;
    public string nextLevel;
    public static int totalMemories = 0;
    public static int totalKills = 0;
    public Text memoryCount;
    public Text memoryInfo;
    public Text killCount;

    private LevelThreeMemory levelThreeMemory;
    private bool isLevelThree;

    public static bool hasTriedElevator;

    void Start()
    {
        // game is not over upon starting
        isGameOver = false;
        totalMemories = 0;
        hasTriedElevator = false;
        isLevelThree = SceneManager.GetActiveScene().name == "Level3";

        //gameText.gameObject.SetActive(false);
        //totalMemories = 0;
        //memoryCount.text = "memories: " + totalMemories; 
        }
    }

    void Update()
    {
        // if not working comment out it was commented out from last submission
        if (nextLevel == "Level4" && Input.GetKeyDown(KeyCode.E))
        {
            memoryInfo.gameObject.SetActive(false);
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

        Invoke("LoadCurrentLevel", 2);
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
        if (isLevelThree && levelThreeMemory != null)
        {
            levelThreeMemory.UpdateMemoryCountText(memoryCount, memoryInfo);
        }
        // for levels 1 and 2
        else if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2")
        {
            totalMemories++;
            memoryInfo.gameObject.SetActive(true);
            if (hasTriedElevator)
            {
                memoryCount.text = "memories: " + totalMemories + "   remaining: " + (6 - totalMemories);
            }
            else
            {
                memoryCount.text = "memories: " + totalMemories;
            }
        }
    }
    public void UpdateMemoryText()
    {
        if (isLevelThree && levelThreeMemory != null)
        {
            levelThreeMemory.UpdateMemoryText(memoryCount);
        }
        // for levels 1 and 2
        else if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2")
        {
            if (hasTriedElevator)
            {
                memoryCount.text = "memories: " + totalMemories + "   remaining: " + (6 - totalMemories);
            }
            else
            {
                memoryCount.text = "memories: " + totalMemories;
            }
        }
    }
