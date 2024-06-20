using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryManager4 : MonoBehaviour
{
    public GameObject fadeToWhite;
    public Image dialogueBox;
    public Text dialogueText;
    public Transform boss;
    public Transform player;
    int dialogueIndex;
    bool waitingForE;
    public AudioClip unsheatheSFX;

    public Slider healthSlider;

    public AudioClip talkSFX;

    bool waitForBeginning;
    bool memDialogue;
    bool waitToKill;
    bool waitToKillOrSpare;

    public Text credits;
    public Canvas normalUI;
    int creditLineCount;

    static int levelThreeKills = 2; // should be from level 3

    // Start is called before the first frame update
    void Start()
    {
        creditLineCount = 0;
        normalUI.enabled = true;
        credits.enabled = false;
        waitForBeginning = true;
        waitingForE = false;
        dialogueBox.enabled = false;
        dialogueText.enabled = true;
        dialogueIndex = 1;
        fadeToWhite.SetActive(false);
        memDialogue = false;
        dialogueIndex = 0;
        waitToKill = false;
        waitToKillOrSpare = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForBeginning && Vector3.Distance(boss.position, player.position) <= 15)
        {
            waitForBeginning = false;
            PlayerController.pauseMovement = true;
            NextLine();
        }
        if (waitingForE && Input.GetKeyDown(KeyCode.E))
        {
            if (memDialogue)
            {
                waitingForE = false;
                memDialogue = false;
                Time.timeScale = 1.0f;
                dialogueBox.enabled = false;
                dialogueText.enabled = false;
            }
            else
            {
                NextLine();
            }
        }
        if (waitToKill && Input.GetButtonDown("Fire1"))
        {
            waitToKill = false;
            // boss dies
            // elevator will go to bad ending
            PlayerController.pauseMovement = false;
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            PlayerAttack.level4ElevatorEnding = 1;
            Debug.Log("level 4 ending is 1");
            // maybe make the music stop
        }
        if (waitToKillOrSpare)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                waitToKillOrSpare = false;
                // boss dies
                // elevator will go to bad ending
                PlayerController.pauseMovement = false;
                dialogueBox.enabled = false;
                dialogueText.enabled = false;
                PlayerAttack.level4ElevatorEnding = 1;
                // maybe make the music stop
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                waitToKillOrSpare = false;
                dialogueIndex++;
                PlayerAttack.level4ElevatorEnding = 2;
                // elevator will fade to white 
                // make the music stop?
                NextLine();
            }
        }

    }
    // when enemy dies, should trigger NextLine of storymanager
    public void NextLine()
    {
        Debug.Log("index " + dialogueIndex);
        // don't show empty dialogue while moving, then wait for E, prepare index 3
        if (dialogueIndex == 10) // changed
        {
            waitingForE = false;
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            dialogueIndex++;
            PlayerController.pauseMovement = false;
        }
        else if (dialogueIndex == 11)
        {
            waitingForE = true;
            dialogueText.text = dialogue[dialogueIndex];
            dialogueIndex++;
            dialogueBox.enabled = true; // consider putting this at end of 2
            dialogueText.enabled = true;
            PlayerController.pauseMovement = true;
        }
        else if (dialogueIndex == 12)
        {
            waitingForE = true;
            dialogueText.text = dialogue[dialogueIndex];
            dialogueIndex++;
            dialogueBox.enabled = true; // consider putting this at end of 2
            dialogueText.enabled = true;
            if (levelThreeKills < 3)
            {
                dialogueIndex++;
            }
        }
        else if (dialogueIndex == 13)
        {
            dialogueText.text = dialogue[dialogueIndex];
            waitToKill = true;
        }
        else if (dialogueIndex == 14)
        {
            dialogueText.text = dialogue[dialogueIndex];
            waitToKillOrSpare = true;
        }
        else if (dialogueIndex == 25)
        {
            waitingForE = false;
            dialogueBox.enabled = false; // consider putting this at end of 2
            dialogueText.enabled = false;
            PlayerController.pauseMovement = false;
        }
        else
        {
            waitingForE = true;
            dialogueText.text = dialogue[dialogueIndex];
            dialogueIndex++;
            dialogueBox.enabled = true; // consider putting this at end of 2
            dialogueText.enabled = true;
        }

        if (dialogueBox.enabled == true)
        {
            if (!waitToKill && !waitToKillOrSpare)
            {
                AudioSource.PlayClipAtPoint(talkSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
            }
        }
    }

    public void MemoryDialogue()
    {
        Time.timeScale = 0f;
        if (levelThreeKills > 2)
        {
            dialogueText.text = "[You pick up half of a photograph. It matches the other half you picked up earlier. Beside yourself, you're shaking hands with men in suits, grim-faced.]";
        }
        else 
        {
            dialogueText.text = "[You pick up half of a photograph. It matches the other half you picked up earlier. Beside yourself is a woman, and a child. You're all smiling.]";
        }
        dialogueBox.enabled = true;
        dialogueText.enabled = true;
        waitingForE = true;
        memDialogue = true;
    }

    public void GoodEnding()
    {
        PlayerController.pauseMovement = true;
        waitingForE = false;
        dialogueBox.enabled = false;
        dialogueText.enabled = false;
        normalUI.enabled = false;
        fadeToWhite.SetActive(true);
        fadeToWhite.GetComponent<Animator>().SetTrigger("FadeToWhite");
        Invoke("PlayCreditLine", 5);
    }

    void PlayCreditLine()
    {
        if (creditLineCount == 0)
        {
            credits.text = "Good Ending";
            credits.enabled = true;
            credits.GetComponent<Animator>().SetBool("playText", true);
            Invoke("NoCreditText", 5);
        }
        else if (creditLineCount == 1)
        {
            Debug.Log("got here");
            credits.text = "Created by Corina Torres, Vy Truong, Carina Vale, and Zhile Wang";
            credits.GetComponent<Animator>().SetBool("playText", true);
            Invoke("NoCreditText", 5);
        }
        else if (creditLineCount == 2)
        {
            Debug.Log("and here");
            credits.text = "Vestige";
            credits.GetComponent<Animator>().SetBool("playText", true);
            Invoke("BackToMainMenu", 6);
        }

    }


    void NoCreditText()
    {
        credits.GetComponent<Animator>().SetBool("playText", false);
        creditLineCount++;
        Invoke("PlayCreditLine", 1);
    }

    void WaitForE()
    {
        waitingForE = true;
    }

    void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    private readonly string[] dialogue =
    {
        "\"Well, well, you actually made it here. I was wondering how much longer it would take for you to show up.\"" ,
        "\"Funny how this whole limbo thing works. You die at the exact same time. Yet you still have to wait around to see the person who killed you.\"",
        "\"Do you recognize this place? Usually it's different for separate souls. But you and me, we're linked.\"",
        "\"This is the rooftop. The place where you killed me. The place where I killed you. Only I did it in self-defense. And I'd do it again.\"",
        "\"I can guess why you're here. You want to kill me again. Settle one final contract. Make sure you deal the final blow.\"",
        "\"Well, you can certainly try.\"",
        "\"I just take joy in the fact that you'll never leave this place. Not after what you've done. All the people you ended.\"",
        "\"Scum like you have no chance for redemption. Frankly, I thought you'd be more suited for Hell.\"",
        "\"But I won't question why you were even allowed here. Maybe it's a gift to me. A chance for my own revenge.\"",
        "\"And I'm going to enjoy it.\"",
        "",
        "\"You bastard. There's no end to the hurt you've caused. You have no remorse.\"",
        "\"Well go ahead. Finish what you've started!\"",
        "[Press Left Mouse Button to Kill.]",
        "[Press Left Mouse Button to Kill. Press Right Mouse Button to Spare.]",
        "\"...\"",
        "\"It's not enough to murder me twice? You have to mock me, too?\"",
        "\"What are you waiting for? Do it!\"",
        "\"You...you have to! What are you waiting for? Do it!\"",
        "\"You're...you're serious...\"",
        "\"Well then. Maybe there is a reason you were sent here instead of to Hell.\"",
        "\"I think...if you really feel this way, if you really do feel remorse, you should be able to move on. I don't think this is the place for either of us.\"",
        "\"Hmm... If we try the elevator again, it should take us where we're meant to be.\"",
        "\"You go ahead. I'll follow behind you. Can't be too careful, you know?\"",
        "\"I don't know if I can forgive for what you did when we were alive. But, thank you for this chance.\"",
        ""
     };
}




