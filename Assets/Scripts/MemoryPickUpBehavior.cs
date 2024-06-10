using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryPickUpBehavior : MonoBehaviour
{
    public AudioClip pickupSFX;
    public Transform cameraTransform;
    private bool bonusActive;
    void Start()
    {
        PlayerSwordBehavior.swordIsActive = false;
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
            FindObjectOfType<LevelManager>().UpdateMemoryCountText();
            if (gameObject.CompareTag("Sword"))
            {
                other.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                PlayerSwordBehavior.swordIsActive = true;
                PlayerSwordBehavior.hasSword = true;
                PlayerController.pauseMovement = true;
                FirstEnemyScript.seenPlayer = false;
                FindObjectOfType<StoryManager>().NextLine();
            }
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
