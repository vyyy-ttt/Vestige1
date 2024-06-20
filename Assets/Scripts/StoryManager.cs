using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    public GameObject briefPause;
    public Image dialogueBox;
    public Text dialogueText;
    public GameObject receptionistObject;
    Transform receptionist;
    public Transform player;
    Vector3 recepLookPosition;
    int dialogueIndex;
    bool waitingForE;
    public AudioClip unsheatheSFX;

    public Slider healthSlider;
    public GameObject receptionistPrefab;
    public GameObject sword;
    public GameObject firstEnemyPrefab;
    //public GameObject blankMemoryPrefab;

    bool checkingForMovement;
    bool hasCrouched;
    bool hasRun;
    bool hasJumped;
    public static bool getFirstMemories;
    bool recepWaitForApproach;
    bool recepFacePlayer;
    bool waitForEnemy;
    bool miscDialogue;
    bool moveToPlayer;

    bool talkingToNPC;

    Animator recepAnim;
    Animator firstEnemyAnim;
    public AudioClip talkSFX;
    // Start is called before the first frame update
    void Start()
    {
        sword.SetActive(false);
        recepAnim = receptionistObject.GetComponent<Animator>();
        receptionist = receptionistObject.transform;
        receptionist.position = new Vector3(-0.62f, 1.865f, 6.86f);
        recepFacePlayer = false;
        waitingForE = false;
        dialogueBox.enabled = false;
        PlayerController.pauseMovement = true;
        dialogueIndex = 1;
        checkingForMovement = false;
        hasCrouched = false;
        hasRun = false;
        hasJumped = false;
        getFirstMemories = false;
        recepWaitForApproach = false;
        waitForEnemy = false;
        miscDialogue = false;
        moveToPlayer = false;
        talkingToNPC = false;
        briefPause.SetActive(false);
        Invoke("StartSequence", 3);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(dialogueIndex);
        if (waitingForE && Input.GetKeyDown(KeyCode.E))
        {
            if (!miscDialogue)
            {
                NextLine();
            }
            else if (talkingToNPC) // get rid of this bool?
            {
                talkingToNPC = false;
                Debug.Log("dismiss");
                dialogueBox.enabled = false;
                dialogueText.enabled = false;
                waitingForE = false;
                PlayerController.pauseMovement = false;
                // some other function for random dialogue?
            }
        }
        if (checkingForMovement)
        {
            Debug.Log("is checking");
            if (hasJumped && hasRun && hasCrouched)
            {
                Debug.Log("all good!");
                checkingForMovement = false;
                NextLine();
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("ran!");
                hasRun = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Debug.Log("crouched!");
                hasCrouched = true;
            }
            else if (Input.GetButton("Jump"))
            {
                Debug.Log("jumped!");
                hasJumped = true;
            }
        }
        if (recepWaitForApproach)
        {
            if (Vector3.Distance(receptionist.position, player.position) < 4)
            {
                Debug.Log("waited for approach");
                recepWaitForApproach = false;
                PlayerAttack.disableTeleport = true;
                PlayerController.pauseMovement = true;
                NextLine();
            }
        }
        if (waitForEnemy)
        {
            if (player.position.x > -1.4 && player.position.x < 2.5)
            {
                waitForEnemy = false;
                PlayerController.pauseMovement = true;
                Instantiate(firstEnemyPrefab, new Vector3(-3.61f, 1.975072f, 30.686f), Quaternion.Euler(0, 180, 0));
                // trigger ghost animation
                Invoke("FinishWaitForEnemy", 3);
            }
        }
        if (FirstEnemyScript.enemiesDead == true && FirstEnemyScript.seenPlayer == true)
        {
            FirstEnemyScript.seenPlayer = false; // so it only happens once, death can continue as normal
            Invoke("NextLine", 5);
        }
        
        // ghost animation, ghost will enter room, do this after some time and when player is in other part

        // if next part (and then first thing in next part function is to get rid of bool or do some conditional)
    }


    void LateUpdate()
    {
        if (recepFacePlayer)
        {
            //Debug.Log("looking");
            //receptionist.rotation = Quaternion.Slerp(receptionist.rotation, Quaternion.LookRotation(player.position), 10 * Time.deltaTime);
            receptionist.LookAt(player);
            //receptionist.position = recepLookPosition;
        }
        if (moveToPlayer)
        {
            Debug.Log("is moving");
            if (Vector3.Distance(receptionist.position, player.position) > 2.5)
            {
                receptionist.position = Vector3.MoveTowards(receptionist.position, player.position, Time.deltaTime * 3);
            }
            else
            {
                moveToPlayer = false;
                //ToggleLookAtPlayer();
            }
        }
    }

    void StartSequence()
    {
        // start at index 1, show message 1, index becomes 2
        dialogueText.text = dialogue[dialogueIndex++];
        dialogueBox.enabled = true;
        waitingForE = true;
        AudioSource.PlayClipAtPoint(talkSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
    }

    public void NextLine()
    {
        Debug.Log(dialogueIndex);
        // don't show empty dialogue while moving, then wait for E, prepare index 3
        if (dialogueIndex == 2) // changed
        {
            waitingForE = false;
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            recepAnim.SetTrigger("Greet");
            receptionist.LookAt(player);
            waitingForE = true;
            dialogueIndex++;
            NextLine();
        }
        else if (dialogueIndex == 6)
        {
            waitingForE = false;
            dialogueBox.enabled = false; // consider putting this at end of 2
            dialogueText.enabled = false;
            recepAnim.SetTrigger("LeaveRoom1");
            Invoke("FinishIndex6", 2.5f);
            // put position to proper spot
        }
        // don't show empty dialogue while player moves, prepare index 8 and message
        else if (dialogueIndex == 7)
        {
            waitingForE = false;
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            dialogueIndex++;
            dialogueText.text = dialogue[dialogueIndex];
            PlayerController.pauseMovement = false;
        }
        // show message 8, prepare index 9
        else if (dialogueIndex == 8)
        {
            dialogueIndex++;
            dialogueBox.enabled = true; // consider putting this at end of 2
            dialogueText.enabled = true;
        }
        // create message 9, prepare index 10
        else if (dialogueIndex == 9)
        {
            dialogueText.text = dialogue[dialogueIndex]; // changed
            dialogueIndex++;
            checkingForMovement = true;
            //recepFacePlayer = true;
            recepAnim.SetTrigger("StartMovement");
            Invoke("ToggleLookAtPlayer", 2);
        }
        // create message 10, prepare index 11
        else if (dialogueIndex == 10)
        {
            //recepFacePlayer = true;
            dialogueText.text = dialogue[dialogueIndex];
            dialogueIndex++;
            waitingForE = true;
            // player has jumped, player has pressed left shift, and player has pressed control


            // once all things checked for:

            //NextLine(); // enable teleport after recep has finished talking
        }
        // at 12, disable text, let the player teleport
        // don't show empty text, prepare index 13
        else if (dialogueIndex == 11)
        {
            ToggleLookAtPlayer();
            recepAnim.SetTrigger("EndMovement");
            PlayerController.pauseMovement = true;
            dialogueText.text = dialogue[dialogueIndex];
            dialogueIndex++;
            Invoke("FinishIndex11", 1.5f);
        }
        else if (dialogueIndex == 12)
        {
            waitingForE = false;
            PlayerController.pauseMovement = false;
            dialogueBox.enabled = false;
            dialogueText.enabled = false; // changed
            dialogueIndex++;
            //dialogueText.text = dialogue[dialogueIndex];
            //MoveReceptionistNextRoom();
        }
        else if (dialogueIndex == 13)
        {
            ToggleLookAtPlayer();
            PlayerController.pauseMovement = true;
            dialogueText.text = dialogue[dialogueIndex];
            dialogueIndex++;
            dialogueBox.enabled = true;
            dialogueText.enabled = true;
            waitingForE = true;
        }
        else if (dialogueIndex == 16)
        {
            PlayerController.pauseMovement = false;
            PlayerAttack.disableTeleport = true;
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            waitingForE = false;
            //dialogueIndex++;
            getFirstMemories = true;
        }
        else if (dialogueIndex == 22)
        {
            ToggleLookAtPlayer();
            waitingForE = false;
            recepAnim.SetTrigger("EndPickup");
            dialogueText.text = dialogue[dialogueIndex];
            dialogueIndex++;
            dialogueBox.enabled = true; // consider putting this at end of 2
            dialogueText.enabled = true;
            Debug.Log("at 22");
            Invoke("FinishIndex22", 1.5f);
        }
        else if (dialogueIndex == 23)
        {
            waitingForE = false;
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            dialogueIndex++;
            //dialogueText.text = dialogue[dialogueIndex];
            PlayerAttack.disableTeleport = false;
            //MoveReceptionistNextRoom();
            PlayerController.pauseMovement = false;
            recepWaitForApproach = true;
        }
        else if (dialogueIndex == 27)
        {
            ToggleLookAtPlayer();
            recepAnim.SetTrigger("LeaveCombat");
            Invoke("FinishIndex27", 1.5f);
            // animation
            waitingForE = false;
            PlayerController.pauseMovement = false;
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            dialogueIndex++;
            PlayerAttack.disableTeleport = true;
            //MoveReceptionistNextRoom();
            Invoke("WaitForEnemy", 6);
        }
        else if (dialogueIndex == 30)
        {
            sword.SetActive(true);
            dialogueText.text = dialogue[dialogueIndex];
            dialogueIndex++;
        }
        else if (dialogueIndex == 31)
        {
            waitingForE = false;
            PlayerController.pauseMovement = false;
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            dialogueIndex++;
            FirstEnemyScript.seenPlayer = true;
        }
        else if (dialogueIndex == 32)
        {
            briefPause.SetActive(true);
            AudioSource.PlayClipAtPoint(unsheatheSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
            waitingForE = true;
            dialogueBox.enabled = true;
            dialogueText.enabled = true;
            dialogueText.text = dialogue[dialogueIndex];
            dialogueIndex++;
        }
        else if (dialogueIndex == 34)
        {
            briefPause.SetActive(false);
            waitingForE = false;
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            dialogueIndex++;
            PlayerController.pauseMovement = false;
            FirstEnemyScript.seenPlayer = true;
        }
        else if (dialogueIndex == 35)
        {
            receptionistObject = Instantiate(receptionistPrefab, new Vector3(-3.2f, 1.865f, 24.53f), Quaternion.Euler(0, 0, 0)) as GameObject;
            receptionist = receptionistObject.transform;
            //receptionist.position = new Vector3(-3.2f, 1.865f, 24.53f); // set different position
            //receptionistObject.SetActive(true);
            ToggleLookAtPlayer();
            moveToPlayer = true;
            waitingForE = true;
            dialogueText.text = dialogue[dialogueIndex];
            dialogueIndex++;
            dialogueBox.enabled = true; // consider putting this at end of 2
            dialogueText.enabled = true;
        }
        // pause player when recep returns
        // approach and have him look at you to trigger the next dialogue
        else if (dialogueIndex == 42)
        {
            FindObjectOfType<LevelManager>().UpdateMemoryCountText();
            waitingForE = true;
            dialogueText.text = dialogue[dialogueIndex];
            dialogueIndex++;
            dialogueBox.enabled = true; // consider putting this at end of 2
            dialogueText.enabled = true;
        }
        else if (dialogueIndex == 44)
        {
            waitingForE = false;
            dialogueBox.enabled = false;
            dialogueText.enabled = false;
            PlayerController.pauseMovement = false;
            PlayerAttack.disableTeleport = false;
            miscDialogue = true;
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
            AudioSource.PlayClipAtPoint(talkSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
        }
        //else (dialogueIndex == )
        //{
        //}
    }
    /*
    public void MoveReceptionistNextRoom()
    {
        receptionist.position = new Vector3(receptionist.position.x, receptionist.position.y, receptionist.position.z + 7.9f);
        recepLookPosition = receptionist.position;
    }*/

    public void FirstMemoriesDialogue()
    {
        if (LevelManager.totalMemories == 1)
        {
            dialogueText.text = dialogue[dialogueIndex];
            dialogueBox.enabled = true;
            dialogueText.enabled = true;
            dialogueIndex++;
        }
        else if (LevelManager.totalMemories == 2)
        {
            dialogueText.text = dialogue[dialogueIndex];
            dialogueIndex++;
        }
        else if (LevelManager.totalMemories == 3)
        {
            dialogueText.text = dialogue[dialogueIndex];
            dialogueIndex++;
            getFirstMemories = false;
            PlayerController.pauseMovement = true;
            Invoke("NextLine", 5);
        }
    }

    public void LastMemoryDialogue()
    {
        PlayerController.pauseMovement = true;
        dialogueText.text = miscNPC[1];
        dialogueBox.enabled = true;
        dialogueText.enabled = true;
        talkingToNPC = true;
        Invoke("SetWaitForE", 1);
    }

    void WaitForEnemy()
    {
        waitForEnemy = true;
    }

    void ToggleLookAtPlayer()
    {
        recepFacePlayer = !recepFacePlayer;
    }

    void FinishIndex6()
    {
        receptionistObject.SetActive(false);
        receptionist.position = new Vector3(-0.62f, 1.865f, 12.224f);
        receptionist.rotation = Quaternion.Euler(0, 0, 0);
        receptionistObject.SetActive(true);
        recepLookPosition = receptionist.position;
        //recepFacePlayer = true;
        waitingForE = true;
        dialogueText.text = dialogue[dialogueIndex];
        dialogueIndex++;
        dialogueBox.enabled = true; // consider putting this at end of 2
        dialogueText.enabled = true;
    }

    void FinishIndex11()
    {
        receptionistObject.SetActive(false);
        receptionist.position = new Vector3(-0.62f, 1.865f, 22.3f); // set different position
        receptionist.rotation = Quaternion.Euler(0, 0, 0);
        receptionistObject.SetActive(true);
        recepLookPosition = receptionist.position;
        PlayerAttack.disableTeleport = false;
    }

    void FinishIndex22()
    {
        receptionistObject.SetActive(false);
        receptionist.position = new Vector3(-0.62f, 1.865f, 30); // set different position!!!!
        receptionist.rotation = Quaternion.Euler(0, 0, 0);
        receptionistObject.SetActive(true);
        ToggleLookAtPlayer();
        recepLookPosition = receptionist.position;
        dialogueBox.enabled = false; // consider putting this at end of 2
        dialogueText.enabled = false;
        PlayerController.pauseMovement = false;
        PlayerAttack.disableTeleport = false;
        recepWaitForApproach = true;
    }

    void FinishIndex27()
    {
        receptionistObject.SetActive(false);
    }

    void FinishWaitForEnemy()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("FirstEnemy");
        //firstEnemyAnim = enemy.GetComponent<Animator>();
        //firstEnemyAnim.SetTrigger("Idle");
        enemy.transform.LookAt(player);
        NextLine();
    }
    
    public void GetNPCDialogue()
    {
        AudioSource.PlayClipAtPoint(talkSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
        Debug.Log("dialogue");
        PlayerController.pauseMovement = true;
        dialogueText.text = miscNPC[Random.Range(2, 8)];
        dialogueBox.enabled = true;
        dialogueText.enabled = true;
        talkingToNPC = true;
        Invoke("SetWaitForE", 1);
    }

    public void ElevatorDialogue()
    {
        AudioSource.PlayClipAtPoint(talkSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
        PlayerController.pauseMovement = true;
        dialogueText.text = miscNPC[0];
        dialogueBox.enabled = true;
        dialogueText.enabled = true;
        talkingToNPC = true;
        Invoke("SetWaitForE", 1);
    }

    void SetWaitForE()
    {
        waitingForE = true;
    }
    /*
    public void PlayerDied1()
    {
        talkingToNPC = true;    // has same function for dismissing message
        waitingForE = false;
        PlayerHealth.currentHealth = 100;
        healthSlider.value = PlayerHealth.currentHealth;
        LevelManager.totalMemories = 0;
        FindObjectOfType<LevelManager>().UpdateMemoryText();
        PlayerSwordBehavior.hasSword = false;
        Debug.Log("rotate");
        player.parent.transform.Rotate(90, 0, 0, Space.Self);
        player.parent.transform.position = new Vector3(-0.636f, 2.38f, 2.49f);
        PlayerController.pauseMovement = true;
        PlayerAttack.disableTeleport = false;
        FindObjectOfType<FirstEnemyScript>().ResetEnemy();
        // need to handle the first enemy
        // instantiate some memories
        dialogueText.text = dialogue[0];
        dialogueText.enabled = true;
        dialogueBox.enabled = true;
        Invoke("SetWaitForE", 1);
    }
    */

    private readonly string[] dialogue = { "[You ran away]",
        "\"Oh, you look new!\" [Press E to continue]",
        "",
        "\"Welcome! It's good to have you here.\"",
        "\"You probably have some questions, they all do. I’d offer you my name, but it’s still forgotten to me. Souls around here call me The Receptionist. In time, you might come up with a name you want to be called by.\"",
        "\"Here, let me show you the ropes. This can all be a shock, but it’ll make your transition easier.\"",
        "[Follow The Receptionist with WASD. Dismiss this message with E.]",
        "",
        "[The crosshair turned yellow. This door can be interacted with. Press E when close enough.]",
        "\"Firstly, let me tell you a bit about your new form. You can move around with WASD. Use Space Bar to jump. You can hold down Left Shift to run, and use Control to toggle crouching. Give it a try!\"",
        "\"You’re a natural! Way more agile than the average soul, I'd say. You were probably an athlete when you were alive.\"",
        "\"Come on, I’ll teach you some more things.\"",
        "",
        "", //13
        "\"This is a good place to learn about memories. Souls always find their first few in here.\"", //14
        "\"This whole place runs on memories. Right now, you don’t have any, but you can find them floating around, waiting to be collected by you. Simply walk into them, and you might learn some things about yourself.\"",
        "[You pick up a bike lock, and you suddenly remember what bikes are. You think you know how to ride one.]",
        "[You pick up a movie ticket. The name sounds like it’d be romantic. You think you remember feeling happy.]",
        "[You pick up some gravel. From another time, you remember it rushing to meet you. You remember falling. It was a long fall.]",
        "\"Usually that second or third one reminds you how you died. Myself, I was bitten by a snake. It was probably a long story, but I don’t remember most of it.\"",
        "\"The first few memories are usually the easiest to remember, and also the easiest to remember context for. It’s fresher right now. Some things will take longer to come to you.\"",
        "\"Besides learning, memories will help you get around. Some floor levels can’t be accessed without enough.\"",
        "\"Here, follow me.\"",
        "",
        "\"In the next room, there will be a lobby for you to talk with souls, if you want to. Just walk up to them and press E. They won’t be a bother to you otherwise.\"",
        "\"If you want to get around further, you can use the elevator. You seem ready to me, so I’ll give you-\"",
        "\"Oh shoot, I forgot. Wait here, I’ll be right back.\"",
        //////
        "",
        "\"Wait. Do I know you?\"",
        "\"You remind me of something. I feel… wait a second.\"", //29
        "\"It’s you.\"", // 30
        "",
        "[You pick up a weapon. It feels familiar in your grasp.]",
        "[Press R to sheathe/unsheathe. Press Left Mouse Button to attack.]",
        ////////
        "",
        "\"Holy cow! I’ve never seen that happen before! That was crazy!\"",
        "\"It’s a good thing your memories gave you a knife. You seem really good at using that…\"",
        "\"Well anyway, you didn’t kill him, if that’s what you’re wondering. He just ran away, and dropped some constitution while he was at it! You might as well take it since he’s the one who attacked you.\"",
        "\"I wonder where the ones who die here go. It’s probably not the afterlife.\"", //39
        "\"Oh, I should probably be straightforward. This isn’t the afterlife. Well, it is after life. But it’s more like a limbo.\"", //40
        "\"There is an afterlife, though. Many souls have even made it there from here. I’ve never tried. It usually takes some soul-searching. If you want to move on, though, like most do, you should use the elevators.\"",
        "\"Which is why you need a keycard. Here’s yours.\"",
        "[You take the keycard. There’s a name and a picture of what must be a human face. You feel sure that they don’t belong to you, but they are vaguely familiar.]",
        "\"That’s all I have to show you. You’re free to go. Good luck!\"" // 44



    };

    private readonly string[] miscNPC = { "\"If you’re having trouble using the elevator, it’s probably because you don’t have enough memories. The next floor can’t take form without them.\"",
    "[You pick up a photograph. It's been torn in two. The half you hold shows a human. You think it more likely that this face was yours.]",
    "\"A soul might get scared, being approached by someone with a memory like that.\"",
    "\"If you ever lose your things, I'd advise you to look where you last left it. I've never needed to retrace my steps.\"",
    "\"Hey, you alright? You look pretty intense.\"",
    "\"So, you met The Receptionist? They used to be human, too, you know. But that means there must've been another Receptionist before. Wonder what happened to them.\"",
    "\"Have you seen my buddy? He went into the room you just came out of. Said he had a bad feeling. I haven't seen him since.\"",
    "\"Don't listen to the superstitious folk around here. Six is a lucky number, I tell ya!\""
    };








 
}
