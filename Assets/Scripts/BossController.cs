using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossController : MonoBehaviour
{
    public int attackDamage = 10; 
    public float attackRange = 5.0f;
    public float attackCooldown = 2.0f; 
    private float lastAttackTime;

    private Transform player; 
    private NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        TracePlayer();

        if (Time.time > lastAttackTime + attackCooldown && IsPlayerInRange())
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void TracePlayer()
    {
        if (player != null)
        {
            navAgent.SetDestination(player.position);
        }
    }

    bool IsPlayerInRange()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            return distanceToPlayer <= attackRange;
        }
        return false;
    }

    void Attack()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.PlayerTakesDamage(attackDamage);
            }
        }
    }
}
