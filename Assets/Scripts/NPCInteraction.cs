using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public GameObject player;

    public Image dialogueBox;
    public Text dialogueText;
    //public Text npcsCountText;

    public string[] lines = new string[1];

    //float interactionDistance = 3.0f;
    //int dialogueIndex = 0;
    //int npcsCount;
    bool inDialogue;
    bool playerInRange;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //dialogueIndex = 0;
        //npcsCount = 0;
        inDialogue = false;
        playerInRange = false;
    }

    void Update()
    {   
        /*
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= interactionDistance && Input.GetKeyDown(KeyCode.F) && npcsCount != 3)
        {
            ToggleDialogue();
        }

        if (inDialogue && Input.GetKeyDown(KeyCode.F))
        {
            inDialogue = false;
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            dialogueIndex++;
        }
        npcsCountText.text = "NPCs Talked To: " + npcsCount + "/3";
        */

        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !inDialogue)
        {
            ToggleDialogue();
        }

        if (inDialogue && Input.GetKeyDown(KeyCode.F))
        {
            ExitDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player collided into NPC");
        
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
        
        /*
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Player hit F key");
            ToggleDialogue();
            if (Input.GetKeyDown(KeyCode.F))
            {
                ExitDialogue();
            }
        }
        */
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void ToggleDialogue()
    {
        inDialogue = true;
        dialogueBox.enabled = true;
        dialogueText.enabled = true;

        PlayerController.pauseMovement = true;

        dialogueText.text = lines[0];
        //npcsCount++;
    }

    void ExitDialogue()
    {
        inDialogue = false;
        dialogueBox.enabled = false;
        dialogueText.enabled = false;

        PlayerController.pauseMovement = false;
    }

    private readonly string[] npcDialogue =
{
        "I wonder what this room looks like to you. You know rooms here are different for everyone. I see a big train station. I wonder if I traveled a lot?",
        "Time is weird here. Some souls show up soon after dying, some show up much later. No idea why. My sister and I died at the same time, but she got here later than me. Now she's already in the afterlife.",
        "I don't know why some souls come here first instead of straight to the afterlife. Probably has something to do with what we did when we were alive. But I hardly remember."
    };
}
