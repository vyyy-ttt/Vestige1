using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryManager2 : MonoBehaviour
{
    public Image dialogueBox;
    public Text dialogueText;

    int dialogueIndex;
    bool waitingForE;

    public GameObject sword;

    bool inMiscDialogue;
    //bool talkingToNPC;

    public AudioClip talkSFX;

    public GameObject memory1;
    public GameObject memory2;
    public GameObject stageMemory;

    void Start()
    {
        waitingForE = false;
        dialogueBox.enabled = false;
        PlayerController.pauseMovement = false;
        dialogueIndex = 0;
        inMiscDialogue = false;
        //talkingToNPC = false;
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            sword.SetActive(false);
            memory1.SetActive(true);
            memory2.SetActive(true);
            PlayerController.pauseMovement = true;
            Invoke("StartSequence", 1);
        }
        else
        {
            memory1.SetActive(false);
            memory2.SetActive(false);
            stageMemory.SetActive(false);
        }
    }

    void Update()
    {
        if (waitingForE && Input.GetKeyDown(KeyCode.E))
        {
            if (inMiscDialogue)
            {
                NextLine();
            }
            else //if (talkingToNPC)
            {
                Debug.Log("dismiss");
                dialogueBox.enabled = false;
                dialogueText.enabled = false;
                waitingForE = false;
                PlayerController.pauseMovement = false;
                //talkingToNPC = false;

                if (LevelManager.totalMemories == 2)
                {
                    TransitionLevel();
                }
            }
        }
        //NPCsCountText.text = "NPCs Talked To: " + npcInteractionCount + "/3";
        //memoriesCountText.text = "Memories: " + memoriesCount + "/3";

        if (LevelManager.totalKills == 5)
        {
            stageMemory.SetActive(true);
        }
    }

    void StartSequence()
    {
        // start at index 0, show message 0; shows player's inner monologue at the beginning of the level
        dialogueText.text = miscDialogue[dialogueIndex];
        dialogueBox.enabled = true;
        waitingForE = true;
        AudioSource.PlayClipAtPoint(talkSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
    }

    public void NextLine()
    {
        if (dialogueIndex == 0)
        {
            //inMiscDialogue = true;
            dialogueIndex++;
            waitingForE = false;
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            waitingForE = true;
            PlayerController.pauseMovement = false;
            NextLine(); // causes stackoverflow, but actually shows the npc dbox
        }
        else
        {
            waitingForE = true;
            //dialogueText.text = miscDialogue[dialogueIndex];
            dialogueBox.enabled = true;
            dialogueText.enabled = true;
        }

        if (dialogueBox.enabled == true)
        {
            AudioSource.PlayClipAtPoint(talkSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
        }
    }

    void SetWaitForE()
    {
        waitingForE = true;
    }

    public void GetMemoriesDialogue()
    {
        if (LevelManager.totalMemories == 0)
        {
            dialogueText.text = memories[0];
            dialogueBox.enabled = true;
            dialogueText.enabled = true;
            SetWaitForE();
        }
        else if (LevelManager.totalMemories == 1)
        {
            dialogueText.text = memories[1];
            dialogueBox.enabled = true;
            dialogueText.enabled = true;
            SetWaitForE();
        }
        else if (LevelManager.totalMemories == 2)
        {
            dialogueText.text = memories[2];
            dialogueBox.enabled = true;
            dialogueText.enabled = true;
            SetWaitForE();
        }
    }

    public void TransitionLevel()
    {
        FindObjectOfType<LevelManager>().LevelBeat();
    }

    public void GetNPCDialogue()
    {
        AudioSource.PlayClipAtPoint(talkSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
        PlayerController.pauseMovement = true;
        dialogueText.text = npcDialogue[Random.Range(0, 9)];
        dialogueBox.enabled = true;
        dialogueText.enabled = true;
        Invoke("SetWaitForE", 1);
    }

    // Player's inner monologue at the beginning of the level.
    private readonly string[] miscDialogue =
    {
        "[A ballroom? There's so many other ghosts here, I wonder if any of them can help me.]"
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
        "...",
        "Time is weird here. Some souls show up soon after dying, some show up much later. No idea why. My sister and I died at the same time, but she got here later than me. Now she's already in the afterlife.",
        "...",
        "I don't know why some souls come here first instead of straight to the afterlife. Probably has something to do with what we did when we were alive. But I hardly remember.",
        "...",
        "I wish I could still understand my memories. An acorn here, a ribbon there. At this point, I have no idea what they mean anymore.",
        "...",
        "You trying to get to the afterlife? That’s a hard road. I had to give up. The memories were too painful.",
        "...",
        "Why do they make us go through all these trials, aren't we all going to end up in the afterlife anyway?"
    };
}
