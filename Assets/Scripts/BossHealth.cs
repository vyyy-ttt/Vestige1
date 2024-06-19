using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 100;
    private BossController bossController;
    private EndGameManager endGameManager;

    void Start()
    {
        bossController = GetComponent<BossController>();
        endGameManager = FindObjectOfType<EndGameManager>(); 
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
        if (endGameManager != null)
        {
            endGameManager.ShowEndGameMessage("You have killed your enemy, now it is time to go to the real afterlife");
        }
        Destroy(gameObject);
    }
}