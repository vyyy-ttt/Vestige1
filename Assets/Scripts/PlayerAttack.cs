using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    public AudioClip doorSFX;
    public Image reticleImage;
    int weaponDamage;
    Color originalReticleColor;
    public Text gameText;
    public Transform cameraTransform;
    public static bool disableTeleport;
    private MouseLook mouseLook;

    private GhostEnemyAI ghostAI;

    // story triggers
    public Transform receptionist;
    static bool doorMessage;
    static bool doorMessage2;   // maybe not static
    static bool doorMessage3;

    // Start is called before the first frame update
    void Start()
    {
        disableTeleport = false;
        doorMessage = false;
        doorMessage2 = false;
        doorMessage3 = false;
        gameText.gameObject.SetActive(false);
        originalReticleColor = Color.white;
        weaponDamage = 20;
        mouseLook = GetComponent<MouseLook>();
        ghostAI = GetComponent<GhostEnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // animation and soundclip

            // attack, play animation, deal damage if lands on enemy
            // play audio clip
            // a clip should play if enemy is hit
        }
    }

    private void FixedUpdate()
    {
        ReticleEffect();
    }

    void ReticleEffect()
    {
        RaycastHit hit;
        // at a distance of 10:
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10)) // will return true if hits a collider
        { 
            // if an enemy 10 units away, turn cursor red
            if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("FirstEnemy"))
            {
                reticleImage.color = Color.Lerp(reticleImage.color, Color.red, Time.deltaTime * 5);
                //reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(0.7f, 0.7f, 1), Time.deltaTime * 2);
            }
            // if an interactable (currently doors) 10 units away, turn cursor yellow
            else if (hit.collider.CompareTag("InDoor") || hit.collider.CompareTag("OutDoor"))
            {
                reticleImage.color = Color.Lerp(reticleImage.color, Color.yellow, Time.deltaTime * 5);
                /*if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
                {
                    if (!doorMessage)
                    {
                        Debug.Log("ready for message");
                        doorMessage = true;
                        FindObjectOfType<StoryManager>().NextLine();
                    }
                    if (hit.collider.CompareTag("OutDoor") && Input.GetKeyDown(KeyCode.E) && !disableTeleport)
                    {
                        DoorTeleport();
                    }
                    if (hit.collider.CompareTag("InDoor") && Input.GetKeyDown(KeyCode.E))
                    {
                        ElevatorNextLevel();
                    }
                }*/
            }
            // otherwise turn white
            else
            {
                reticleImage.color = Color.Lerp(reticleImage.color, Color.white, Time.deltaTime * 5);
                //reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, Vector3.one, Time.deltaTime * 2);
            }
        }
        // if nothing at distance, turn white
        else
        {
            reticleImage.color = Color.Lerp(reticleImage.color, Color.white, Time.deltaTime * 5);
        }

        //At distance of 2:
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
        {
            // immediate kill if crouching when attacking
            if(MouseLook.isCrouching && hit.collider.CompareTag("Enemy") && PlayerSwordBehavior.swordIsActive && Input.GetButtonDown("Fire1") && ghostAI.currentState == GhostEnemyAI.FSMStates.Patrol) // and in patrol mode
            {
                Debug.Log("enemy died hopefully");
                FindObjectOfType<EnemyBehavior>().EnemyTakesDamage(100);
            }
            // if enemy within 2 units and player has sword out and hits mouse, enemy takes damage
            if (hit.collider.CompareTag("Enemy") && PlayerSwordBehavior.swordIsActive && Input.GetButtonDown("Fire1"))
            {
                Debug.Log("enemy took damage");
                FindObjectOfType<EnemyBehavior>().EnemyTakesDamage(weaponDamage);
            }
            else if (hit.collider.CompareTag("FirstEnemy") && PlayerSwordBehavior.swordIsActive && Input.GetButtonDown("Fire1"))
            {
                if (!PlayerController.pauseMovement)
                {
                    Debug.Log("enemy took damage");
                    FindObjectOfType<FirstEnemyScript>().EnemyTakesDamage(weaponDamage);
                }
            }
            else if (hit.collider.CompareTag("InDoor") && Input.GetKeyDown(KeyCode.E))
            {
                ElevatorNextLevel();
            }            
            else if (hit.collider.CompareTag("OutDoor"))
            {
                if (!doorMessage)
                {
                    doorMessage = true;
                    FindObjectOfType<StoryManager>().NextLine();
                }
                if (Input.GetKeyDown(KeyCode.E) && !disableTeleport)
                {
                    DoorTeleport();
                }
            }
        }

    }

    // if player hits E on closeby InDoor, trigger next level
    private void ElevatorNextLevel()
    {
        
        if (SceneManager.GetActiveScene().name == "Level4")
        {

            gameText.gameObject.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Level1")
        {
            Debug.Log("I have memories");
            if(LevelManager.totalMemories == 6)
            {
                LevelManager.isGameOver = true;
                FindObjectOfType<LevelManager>().LevelBeat();
            }
            else
            {
                FindObjectOfType<StoryManager>().ElevatorDialogue();
                LevelManager.hasTriedElevator = true;
                FindObjectOfType<LevelManager>().UpdateMemoryText();
            }
    
        }
        else if (SceneManager.GetActiveScene().name == "Level2" || SceneManager.GetActiveScene().name == "Level2a")
        {
            if (LevelManager.totalMemories == 3)
            {
                LevelManager.isGameOver = true;
                FindObjectOfType<LevelManager>().LevelBeat();
            }
            else
            {
                LevelManager.isGameOver = false;
            }
        }
        else
        {
            LevelManager.isGameOver = true;
            FindObjectOfType<LevelManager>().LevelBeat();
        }
    }

    // if player hits E on closeby OutDoor, enter next room
    private void DoorTeleport()
    {
        AudioSource.PlayClipAtPoint(doorSFX, cameraTransform.position);
        gameObject.transform.position = transform.position + (transform.forward * 2.5f);//.normalized;
        if (!doorMessage2)
        {
            //receptionist.position = receptionist.position - (receptionist.forward * 2.5f);
            //FindObjectOfType<StoryManager>().MoveReceptionistNextRoom();
            Debug.Log("ready for message");
            doorMessage2 = true;
            disableTeleport = true;
            FindObjectOfType<StoryManager>().NextLine();
        }
        
        else if (!doorMessage3 && Vector3.Distance(receptionist.position, transform.position) < 10)
        {
            Debug.Log("door message 3");
            doorMessage3 = true;
            disableTeleport = true;
            FindObjectOfType<StoryManager>().NextLine();
        }
    }
}

