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
    public GameObject dialogueBox;
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
        if (isLevelThree)
        {
            memoryInfo.gameObject.SetActive(false);
        }

        //gameText.gameObject.SetActive(false);
        //totalMemories = 0;
        //memoryCount.text = "memories: " + totalMemories; 

        if (SceneManager.GetActiveScene().name == "Level2a")
        {
            totalMemories = 2;
        }

        if (isLevelThree)
        {
            levelThreeMemory = GetComponent<LevelThreeMemory>();
            levelThreeMemory.InitializeMemory();
        }
    }

    void Update()
    {
        // if not working comment out it was commented out from last submission
        // if (nextLevel == "Level4" && Input.GetKeyDown(KeyCode.E))
        // {
        //     memoryInfo.gameObject.SetActive(false);
        // }
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
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            totalMemories++;
            memoryInfo.gameObject.SetActive(true);
            dialogueBox.gameObject.SetActive(true);
            if (totalMemories == 1)
            {
                memoryInfo.text = "You pick up a pair of gold bands – wedding rings – with ‘C&S 6/7/17 engraved on the inside.";
            }
            else if (totalMemories == 2)
            {
                memoryInfo.text = "You pick up a bouquet of flowers, slightly wilted. There’s a card attached but you can’t quite make out it says. They still smell nice but you're not sure how that makes you feel.";
            }
            else if (totalMemories == 3)
            {
                memoryInfo.text = "You pick up an old drawing notebook. You flip through the pages, the first few contain rudimentary sketches of still lives and anatomy studies. The rest is filled with crayon drawings of families, dogs, parks, school, friends, and the last page a teddy bear in a hospital room";
            }
            else if (totalMemories == 4)
            {
                memoryInfo.text = "You pick up a pile of bills – unpaid hospital bills, late electricity payments, past due rent, etc. The words seem to blur together as you flip through the pages and it becomes harder to tell what money is owed for what by the time you reach the end of the pile";
            }
            else if (totalMemories == 5)
            {
                memoryInfo.text = "You pick up card with the photo of a young girl smiling up at you. ‘In Loving Memory Emma Grace DeMarco 2017-2024’ A short Psalm expert is written on the back";
            }
            else if (totalMemories == 6)
            {
                memoryInfo.text = "You pick up a plain business card. It looks worn and bent, as if someone kept it in their pocket for a while forgotten about. No writing printed on it, just a phone number scribbled out. You wonder if you've ever called it...";
            }

            Debug.Log(totalMemories);
            if (hasTriedElevator)
            {
                memoryCount.text = "memories: " + totalMemories + "   remaining: " + (6 - totalMemories);
            }
            else
            {
                memoryCount.text = "memories: " + totalMemories;
            }
            Debug.Log(memoryCount.text);
        }
        //    levelThreeMemory.UpdateMemoryCountText(memoryCount, memoryInfo);
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
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (hasTriedElevator)
            {   
                memoryCount.text = "memories: " + totalMemories + "   remaining: " + (6 - totalMemories);
            }
            else
            {
                memoryCount.text = "memories: " + totalMemories;
            }
            //levelThreeMemory.UpdateMemoryText(memoryCount);
        }
        // for levels 1 and 2
        else if (SceneManager.GetActiveScene().name == "Level1")
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

    public void UpdateKillCountText()
    {
        totalKills++;
        killCount.text = "Kill Count: " + totalKills + "/4";
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            killCount.text = "Kill Count: " + totalKills + "/3";
        }
    }

    public void UpdateLevel2Memories()
    {
        totalMemories++;
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            memoryCount.text = "Memories: " + totalMemories + "/2";
        }
        else
        {
            memoryCount.text = "Memories: " + totalMemories + "/3";
        }
    }
}
