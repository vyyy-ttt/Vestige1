using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 100;
    private BossController bossController;
    private EndGameManager endGameManager;
    public Transform player;
    public static bool bossDead;
    public static Vector3 bossDeathPosition;

    void Start()
    {
        bossDead = false;
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
        bossDead = true;
        bossDeathPosition = transform.position = new Vector3(player.position.x, 0.6f, player.position.z) + 2 * player.forward;
        Debug.Log("Boss died!");
        if (endGameManager != null)
        {
            endGameManager.ShowEndGameMessage("You have killed your enemy, now it is time to go to the real afterlife");
        }
        gameObject.transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("dies");
        FindObjectOfType<StoryManager4>().NextLine();
        
        
        //Destroy(gameObject);
    }
}