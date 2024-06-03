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
    
    // colliding with weapon deals damage
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player took damage");
            FindObjectOfType<PlayerHealth>().PlayerTakesDamage(weaponDamage);
            // play a sound effect
            // play an animation? player moves?
        }
    }
}
