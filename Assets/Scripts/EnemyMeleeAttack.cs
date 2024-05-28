using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    int weaponDamage;
    // Start is called before the first frame update
    void Start()
    {
        weaponDamage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // move this to be for child object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player took damage");
            FindObjectOfType<PlayerHealth>().PlayerTakesDamage(weaponDamage);
            //var playerHealth = other.GetComponent<PlayerHealth>();
            //playerHealth.TakeDamage(damageAmount);
            // play a sound effect
            // play an animation?

        }
    }
}
