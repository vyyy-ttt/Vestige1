using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 100;
    private BossController bossController;

    // Start is called before the first frame update
    void Start()
    {
        bossController = GetComponent<BossController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (!bossController.isHuman)
            {
                bossController.ChangeToHuman();
                health = 150; 
            }
            else
            {
                Die();
            }
        }
    }

    void Die()
    {
        Debug.Log("Boss died!");
    }
}
