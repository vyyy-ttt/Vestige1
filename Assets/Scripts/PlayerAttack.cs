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

    // Start is called before the first frame update
    void Start()
    {
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
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10)) // will return true if hits a collider
        { 
            if (hit.collider.CompareTag("Enemy"))
            {
                //currentProjectilePrefab = projectilePrefab;
                reticleImage.color = Color.Lerp(reticleImage.color, Color.red, Time.deltaTime * 2);
                //reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, new Vector3(0.7f, 0.7f, 1), Time.deltaTime * 2);
            }
            else if (hit.collider.CompareTag("InDoor") || hit.collider.CompareTag("OutDoor"))
            {
                reticleImage.color = Color.Lerp(reticleImage.color, Color.yellow, Time.deltaTime * 2);
            }
            else
            {
                //currentProjectilePrefab = projectilePrefab;
                reticleImage.color = Color.Lerp(reticleImage.color, Color.white, Time.deltaTime * 2);
                //reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, Vector3.one, Time.deltaTime * 2);
            }
        }
        else
        {
            reticleImage.color = Color.Lerp(reticleImage.color, Color.white, Time.deltaTime * 2);
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
        {
            if (hit.collider.CompareTag("Enemy") && PlayerSwordBehavior.swordIsActive && Input.GetButtonDown("Fire1"))
            {
                Debug.Log("enemy took damage");
                FindObjectOfType<EnemyBehavior>().EnemyTakesDamage(weaponDamage);
                // call method to give enemy damage
            }
            else if (hit.collider.CompareTag("InDoor") && Input.GetKeyDown(KeyCode.E))
            {
                InDoor();
            }
            else if (hit.collider.CompareTag("OutDoor") && Input.GetKeyDown(KeyCode.E))
            {
                OutDoor();
            }
        }

    }

    // exiting training room
    private void InDoor()
    {
        if (SceneManager.GetActiveScene().name == "Level1d")
        {
            if (EnemyBehavior.enemyDead == true)
            {
                gameText.gameObject.SetActive(true);
            }
        }
        else
        {
            AudioSource.PlayClipAtPoint(doorSFX, cameraTransform.position);
        }
        LevelManager.isGameOver = true;
        FindObjectOfType<LevelManager>().LevelBeat();
    }

    // entering training room
    private void OutDoor()
    {
        AudioSource.PlayClipAtPoint(doorSFX, cameraTransform.position);
        gameObject.transform.position = transform.position + new Vector3(0, 0, 2.5f);
    }
}

