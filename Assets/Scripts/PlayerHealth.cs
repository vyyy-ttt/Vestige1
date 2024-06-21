using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip hurtSFX;
    public Slider healthSlider;
    public int currentHealth;
    public static bool playerDied;

    // Start is called before the first frame update
    void Start()
    {
        playerDied = false;
        if (SceneManager.GetActiveScene().name == "Level3" || SceneManager.GetActiveScene().name == "Level4")
        {
            startingHealth = 120;
        }
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // take damage, modify slider
    public void PlayerTakesDamage(int damageAmount)
    {
        Debug.Log("Player takes damage: " + damageAmount);
        if (SceneManager.GetActiveScene().name == "Level4")
        {
            if (currentHealth % 20 == 0 && currentHealth != 100 && currentHealth > 0 && !BossHealth.bossDead)
            {
                AudioSource.PlayClipAtPoint(hurtSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
            }
        }
        else
        {
            AudioSource.PlayClipAtPoint(hurtSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
        }
        if (currentHealth > 0 && !BossHealth.bossDead)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            PlayerDies();
        }
        Debug.Log("Current health: " + currentHealth);
    }

    // rotoate player upon death, LevelLost()
    void PlayerDies()
    {
        Debug.Log("Player ran away...");
        playerDied = true;
        //AudioSource.PlayClipAtPoint(deadSFX, transform.position);
        transform.Rotate(-90, 0, 0, Space.Self); // Space.Self, according to local rotation
        FindObjectOfType<LevelManager>().LevelLost();
        //Invoke("DeathProtocol", 2);

    }

    // increase player health, modify slider
    public void TakeHealth(int healthAmount)
    {
        if (currentHealth < 100)
        {
            currentHealth += healthAmount;
            healthSlider.value = Mathf.Clamp(currentHealth, 0, 100); // so we don't overflow
            healthSlider.value = currentHealth;
        }
        Debug.Log("Current health with loot: " + currentHealth);
    }
    /*
    void DeathProtocol()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {

            FindObjectOfType<StoryManager>().PlayerDied1();
        }
    }
    */
}
