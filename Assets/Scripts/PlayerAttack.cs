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

    // Start is called before the first frame update
    void Start()
    {
        disableTeleport = false;
        gameText.gameObject.SetActive(false);
        originalReticleColor = Color.white;
        weaponDamage = 20;
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
            if (hit.collider.CompareTag("Enemy"))
            {
                reticleImage.color = Color.Lerp(reticleImage.color, Color.red, Time.deltaTime * 2);
                //reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(0.7f, 0.7f, 1), Time.deltaTime * 2);
            }
            // if an interactable (currently doors) 10 units away, turn cursor yellow
            else if (hit.collider.CompareTag("InDoor") || hit.collider.CompareTag("OutDoor"))
            {
                reticleImage.color = Color.Lerp(reticleImage.color, Color.yellow, Time.deltaTime * 2);
            }
            // otherwise turn white
            else
            {
                reticleImage.color = Color.Lerp(reticleImage.color, Color.white, Time.deltaTime * 2);
                //reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, Vector3.one, Time.deltaTime * 2);
            }
        }
        // if nothing at distance, turn white
        else
        {
            reticleImage.color = Color.Lerp(reticleImage.color, Color.white, Time.deltaTime * 2);
        }

        //At distance of 2:
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
        {
            // if enemy within 2 units and player has sword out and hits mouse, enemy takes damage
            if (hit.collider.CompareTag("Enemy") && PlayerSwordBehavior.swordIsActive && Input.GetButtonDown("Fire1"))
            {
                Debug.Log("enemy took damage");
                FindObjectOfType<EnemyBehavior>().EnemyTakesDamage(weaponDamage);
            }
            else if (hit.collider.CompareTag("InDoor") && Input.GetKeyDown(KeyCode.E))
            {
                ElevatorNextLevel();
            }
            else if (hit.collider.CompareTag("OutDoor") && Input.GetKeyDown(KeyCode.E) && !disableTeleport)
            {
                DoorTeleport();
            }
        }

    }

    // if player hits E on closeby InDoor, trigger next level
    private void ElevatorNextLevel()
    {
        /*
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (EnemyBehavior.enemiesDead == true)
            {
                gameText.gameObject.SetActive(true);
            }
        }
        else
        {
            AudioSource.PlayClipAtPoint(doorSFX, cameraTransform.position);
        }*/
        LevelManager.isGameOver = true;
        FindObjectOfType<LevelManager>().LevelBeat();
    }

    // if player hits E on closeby OutDoor, enter next room
    private void DoorTeleport()
    {
        AudioSource.PlayClipAtPoint(doorSFX, cameraTransform.position);
        gameObject.transform.position = transform.position + (transform.forward * 2.5f);//.normalized;
    }
}

