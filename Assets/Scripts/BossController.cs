using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossController : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public float summonInterval = 10f;
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    private Transform player;
    private UnityEngine.AI.NavMeshAgent navAgent;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SummonEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange)
            {
                if (Time.time > lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                navAgent.SetDestination(player.position);
            }
        }
    }

    IEnumerator SummonEnemies()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(summonInterval);
            if (!isDead)
            {
                foreach (var spawnPoint in spawnPoints)
                {
                    Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                }
            }
        }
    }

    void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        // FindObjectOfType<LevelManager>().BossDefeated();
    }
}
