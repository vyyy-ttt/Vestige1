using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MemoryPickUpBehavior : MonoBehaviour
{
    public AudioClip pickupSFX;
    public Transform cameraTransform;
    private bool bonusActive;
    private LevelManager levelManager;
    private bool isLevelThree;

    void Start()
    {
        PlayerSwordBehavior.swordIsActive = false;
        levelManager = GetComponent<LevelManager>();
        isLevelThree = SceneManager.GetActiveScene().name == "Level3";
    }

    // Update is called once per frame
    void Update()
    {

    }

    // when player collides, update memory count text and self destroy
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "Level2" || SceneManager.GetActiveScene().name == "Level2a")
            {
                FindObjectOfType<StoryManager2>().GetMemoriesDialogue();
                FindObjectOfType<LevelManager>().UpdateLevel2Memories();
                AudioSource.PlayClipAtPoint(pickupSFX, cameraTransform.position);
                Destroy(gameObject);
            }

            //FindObjectOfType<LevelManager>().UpdateMemoryCountText();
            // comment out if issue \/\/
            else if(isLevelThree)
            {
                AudioSource.PlayClipAtPoint(pickupSFX, cameraTransform.position);
                Destroy(gameObject);
            }

            else if (gameObject.CompareTag("Sword"))
            {
                FindObjectOfType<LevelManager>().UpdateMemoryCountText();
                other.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                PlayerSwordBehavior.swordIsActive = true;
                PlayerSwordBehavior.hasSword = true;
                PlayerController.pauseMovement = true;
                FirstEnemyScript.seenPlayer = false;
                FindObjectOfType<StoryManager>().NextLine();
                Destroy(gameObject);
            }

            else 
            {
                FindObjectOfType<LevelManager>().UpdateMemoryCountText();
                if (StoryManager.getFirstMemories)
                {
                    FindObjectOfType<StoryManager>().FirstMemoriesDialogue();
                }
                else
                {
                    FindObjectOfType<StoryManager>().LastMemoryDialogue();
                }
                AudioSource.PlayClipAtPoint(pickupSFX, cameraTransform.position);
                Destroy(gameObject);
            }
        }
    }
}