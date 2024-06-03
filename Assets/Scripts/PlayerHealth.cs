using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public AudioClip deadSFX;
    public Slider healthSlider;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
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
        if (currentHealth > 0)
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
        //AudioSource.PlayClipAtPoint(deadSFX, transform.position);
        transform.Rotate(-90, 0, 0, Space.Self); // Space.Self, according to local rotation
        FindObjectOfType<LevelManager>().LevelLost();
    }

    // increase player health, modify slider
    public void TakeHealth(int healthAmount)
    {
        if (currentHealth < 100)
        {
            currentHealth += healthAmount;
            healthSlider.value = Mathf.Clamp(currentHealth, 0, 100); // so we don't overflow
        }
        Debug.Log("Current health with loot: " + currentHealth);
    }
}
