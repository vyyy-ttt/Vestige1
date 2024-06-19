using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryManagerBadEnding : MonoBehaviour
{
    public Image dialogueBox;
    public Text dialogueText;
    public GameObject fadeOut;
    public AudioClip talkSFX;
    public Text credits;
    public Canvas normalUI;

    int creditLineCount;
    bool waitingForE; 

    // Start is called before the first frame update
    void Start()
    {
        creditLineCount = 0;
        waitingForE = false;
        dialogueBox.enabled = false;
        dialogueText.enabled = false;
        normalUI.enabled = true;
        credits.enabled = false;
        PlayerController.pauseMovement = true;
        Invoke("DialogueSequence", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingForE && Input.GetKeyDown(KeyCode.E))
        {
            CreditSequence();
        }
    }

    void DialogueSequence()
    {
        dialogueText.enabled = true;
        dialogueBox.enabled = true;
        AudioSource.PlayClipAtPoint(talkSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
        Invoke("WaitForE", 1); // make sure player doesn't exit out if spamming
    }

    void CreditSequence()
    {
        waitingForE = false;
        dialogueBox.enabled = false;
        dialogueText.enabled = false;
        normalUI.enabled = false;
        fadeOut.GetComponent<Animator>().SetTrigger("FadeToBlack");
        Invoke("PlayCreditLine", 5);
    }

    void PlayCreditLine()
    {
        if (creditLineCount == 0)
        {
            credits.text = "Bad Ending";
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

}