using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager2 : MonoBehaviour
{
    public Image dialogueBox;
    public Text dialogueText;

    int dialogueIndex;
    bool waitingForE;

    public GameObject sword;

    bool inMiscDialogue;
    bool talkingToNPC;

    void Start()
    {
        sword.SetActive(false);
        waitingForE = false;
        dialogueBox.enabled = false;
        PlayerController.pauseMovement = true;
        dialogueIndex = 0;
        inMiscDialogue = false;
        talkingToNPC = false;
        Invoke("StartSequence", 1);
    }

    void Update()
    {
        if (waitingForE && Input.GetKeyDown(KeyCode.E))
        {
            if (!inMiscDialogue)
            {
                NextLine();
            }
            else if (talkingToNPC)
            {
                Debug.Log("dismiss");
                dialogueBox.enabled = false;
                dialogueText.enabled = false;
                waitingForE = false;
                PlayerController.pauseMovement = false;
                talkingToNPC = false;
            }
        }
        //NPCsCountText.text = "NPCs Talked To: " + npcInteractionCount + "/3";
        //memoriesCountText.text = "Memories: " + memoriesCount + "/3";
    }

    void StartSequence()
    {
        // start at index 0, show message 0; shows player's inner monologue at the beginning of the level
        dialogueText.text = miscDialogue[dialogueIndex];
        dialogueBox.enabled = true;
        waitingForE = true;
    }

    public void NextLine()
    {
        if (dialogueIndex == 0)
        {
            waitingForE = false;
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            waitingForE = true;
            PlayerController.pauseMovement = false;
        }
        else
        {
            waitingForE = true;
            dialogueText.text = miscDialogue[dialogueIndex];
            dialogueBox.enabled = true;
            dialogueText.enabled = true;
        }
    }

    void SetWaitForE()
    {
        waitingForE = true;
    }

    public void GetNPCDialogue()
    {
        Debug.Log("in getnpcdialogue");
        PlayerController.pauseMovement = true;
        Debug.Log("pausemovement should be true");
        dialogueText.text = npcDialogue[Random.Range(0, 6)];
        Debug.Log("dtext should be set to a dline");
        dialogueBox.enabled = true;
        Debug.Log("dbox should be enabled");
        dialogueText.enabled = true;
        Debug.Log("dtext should be enabled");
        talkingToNPC = true;
        Debug.Log("talkingtonpc should be true");
        Invoke("SetWaitForE", 1);
        Debug.Log("invoking setwaitfore");
    }

    // Player's inner monologue at the beginning of the level.
    private readonly string[] miscDialogue =
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
        "I don't know why some souls come here first instead of straight to the afterlife. Probably has something to do with what we did when we were alive. But I hardly remember.",
        "I wish I could still understand my memories. An acorn here, a ribbon there. At this point, I have no idea what they mean anymore.",
        "You trying to get to the afterlife? That’s a hard road. I had to give up. The memories were too painful.",
        "Why do they make us go through all these trials, aren't we all going to end up in the afterlife anyway?"
    };
}
