using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSwordBehavior : MonoBehaviour
{
    public AudioClip slashSFX;
    public AudioClip unsheatheSFX;
    public Transform cameraTransform;
    public static bool swordIsActive; // whether sword is out
    public static bool hasSword = false;
    bool canSwing;
    // Start is called before the first frame update
    void Start()
    {
        // start with no sword
        bool swordIsActive = false;
        if (SceneManager.GetActiveScene().name == "Level2a")
        {
            swordIsActive = true;
            hasSword = true;
        }
        canSwing = true;
        gameObject.transform.GetChild(2).gameObject.SetActive(swordIsActive);   
    }

    // Update is called once per frame
    void Update()
    {
        // toggle sword when click R
        if (Input.GetKeyDown(KeyCode.R) && hasSword)
        {
            swordIsActive = !swordIsActive;
            gameObject.transform.GetChild(2).gameObject.SetActive(swordIsActive);
            if (swordIsActive)
            {
                AudioSource.PlayClipAtPoint(unsheatheSFX, cameraTransform.position);
            }
        }
        FixedUpdate();
        // play animation is sword out and click mouse button, play audio
        if (swordIsActive && Input.GetButtonDown("Fire1") && canSwing)
        {
            gameObject.transform.GetChild(2).GetComponent<Animator>().SetTrigger("SwordSwung");
            gameObject.transform.GetChild(2).GetComponent<Animator>().SetTrigger("SwordReturned");
            AudioSource.PlayClipAtPoint(slashSFX, cameraTransform.position);
        }
    }

    private void FixedUpdate()
    {
        ReticleEffectNPCs();
    }

    void ReticleEffectNPCs()
    {
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, cameraTransform.forward, out hit, 3))
        {
            if (hit.collider.CompareTag("NPC") || hit.collider.CompareTag("Receptionist"))
            {
                canSwing = false;
                if (hit.collider.CompareTag("NPC") && Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("dialogue 1");
                    hit.collider.transform.LookAt(gameObject.transform.GetChild(0).gameObject.transform);
                    if (FindObjectOfType<LevelManager>().nextLevel == "Level2")
                    {
                        FindObjectOfType<StoryManager>().GetNPCDialogue();
                    }
                    else if (FindObjectOfType<LevelManager>().nextLevel == "Level2a")
                    {
                        Debug.Log("in level manager");
                        FindObjectOfType<StoryManager2>().GetNPCDialogue();
                    }
                }
            }
            else
            {
                canSwing = true;
            }
        }
        else
        {
            canSwing = true;
        }
    }
}
