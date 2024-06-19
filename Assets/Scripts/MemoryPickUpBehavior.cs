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
    void Start()
    {
        PlayerSwordBehavior.swordIsActive = false;
        levelManager = GetComponent<LevelManager>();

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
            else {
                FindObjectOfType<LevelManager>().UpdateMemoryCountText();
                /*if(LevelManager.nextLevel == "Level4")
                {
                     AudioSource.PlayClipAtPoint(pickupSFX, cameraTransform.position);
                     Destroy(gameObject);
                }*/
                if (gameObject.CompareTag("Sword"))
                {
                    other.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    PlayerSwordBehavior.swordIsActive = true;
                    PlayerSwordBehavior.hasSword = true;
                    PlayerController.pauseMovement = true;
                    FirstEnemyScript.seenPlayer = false;
                    FindObjectOfType<StoryManager>().NextLine();
                }
                //if (levelManager.nextLevel == "Level2")
                //{
                else
                {
                    if (StoryManager.getFirstMemories)
                    {
                        FindObjectOfType<StoryManager>().FirstMemoriesDialogue();
                    }
                    else
                    {
                        FindObjectOfType<StoryManager>().LastMemoryDialogue();
                    }
                }
                AudioSource.PlayClipAtPoint(pickupSFX, cameraTransform.position);
                Destroy(gameObject);
            }
        }

    }
}
