using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager2 : MonoBehaviour
{
    public Image dialogueBox;
    public Text dialogueText;
    public Text NPCsCountText;
    public Text memoriesCountText;
    public Transform cameraTransform;

    //public GameObject sword;

    public GameObject NPC1;
    public GameObject NPC2;
    public GameObject NPC3;

    int npcInteractionCount;
    int memoriesCount;
    
    int memoriesIndex;

    bool inDialogue;
    bool talkedToNPC1;
    bool talkedToNPC2;
    bool talkedToNPC3;

    void Start()
    {
        //sword.SetActive(false);
        dialogueBox.enabled = false;
        dialogueText.enabled = false;
        PlayerController.pauseMovement = false;
        npcInteractionCount = 0;
        memoriesCount = 0;
        memoriesIndex = 0;
        inDialogue = false;
        talkedToNPC1 = false;
        talkedToNPC2 = false;
        talkedToNPC3 = false;
        Invoke("StartScene", 2);
    }

    void Update()
    {
        if (!inDialogue && Input.GetKeyDown(KeyCode.E))
        {
            GetNPCDialogue();
        }

        if (inDialogue && Input.GetKeyDown(KeyCode.E))
        {
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            PlayerController.pauseMovement = false;
            inDialogue = false;
        }

        NPCsCountText.text = "NPCs Talked To: " + npcInteractionCount + "/3";
        memoriesCountText.text = "Memories: " + memoriesCount + "/3";
    }

    // Player's inner monologue at the very start of the level
    void StartScene()
    {
        PlayerController.pauseMovement = true;
        dialogueBox.enabled = true;
        dialogueText.enabled = true;
        inDialogue = true;
        dialogueText.text = playerMonologue[0];
    }

    public void GetNPCDialogue()
    {
        Debug.Log("got inside the function!");

        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, cameraTransform.forward, out hit, 3) && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("got to hit!");
            if (hit.collider.gameObject == NPC1)
            {
                Debug.Log("got to 1!");
                PlayerController.pauseMovement = true;
                dialogueBox.enabled = true;
                inDialogue = true;
                dialogueText.text = npcDialogue[0];
                if (!talkedToNPC1)
                {
                    talkedToNPC1 = true;
                    npcInteractionCount++;
                }
            }
            else if (hit.collider.gameObject == NPC2)
            {
                PlayerController.pauseMovement = true;
                dialogueBox.enabled = true;
                inDialogue = true;
                dialogueText.text = npcDialogue[1];
                if (!talkedToNPC2)
                {
                    talkedToNPC2 = true;
                    npcInteractionCount++;
                }
            }
            else if (hit.collider.gameObject == NPC3)
            {
                PlayerController.pauseMovement = true;
                dialogueBox.enabled = true;
                inDialogue = true;
                dialogueText.text = npcDialogue[2];
                if (!talkedToNPC3)
                {
                    talkedToNPC3 = true;
                    npcInteractionCount++;
                }
            }
        }
    }

    private readonly string[] playerMonologue =
    {
        "[A ballroom? There's so many other ghosts here...]"
    };

    private readonly string[] memories =
    {
        "[You pick up a bullet. You can still feel the coolness of the metal in this form.]",
        "[You pick up an envelope filled with money. Wait, are those blood stains?]",
        "[You pick up a contract. The target's face is blurry, but the features seem to match up with the face on your keycard.]"
    };

    private readonly string[] npcDialogue =
    {
        "I wonder what this room looks like to you. You know rooms here are different for everyone. I see a big train station. I wonder if I traveled a lot?",
        "Time is weird here. Some souls show up soon after dying, some show up much later. No idea why. My sister and I died at the same time, but she got here later than me. Now she's already in the afterlife.",
        "I don't know why some souls come here first instead of straight to the afterlife. Probably has something to do with what we did when we were alive. But I hardly remember."
    };
}
