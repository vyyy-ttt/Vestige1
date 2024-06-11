using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public GameObject ghostModel;
    public GameObject humanModel;
    public float detectRange = 10f;
    public float ghostSpeed = 2f;
    public float humanSpeed = 4f;
    public int ghostDamage = 10;
    public int humanDamage = 12;
    public float attackCooldown = 2f;
    public bool isHuman = false;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private float lastAttackTime;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            Debug.Log("Player object found: " + player.name);
        }
        else
        {
            Debug.LogError("Player object not found. Make sure the player GameObject is tagged as 'Player'.");
        }

        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on Boss object.");
        }

        if (!navMeshAgent.isOnNavMesh)
        {
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
            {
                navMeshAgent.Warp(hit.position);
            }
            else
            {
                Debug.LogError("Boss cannot be repositioned to a valid NavMesh position.");
            }
        }

        lastAttackTime = -attackCooldown;
        ActivateGhost();
    }

    void Update()
    {
        if (player == null || navMeshAgent == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= detectRange)
        {
            ChasePlayer();
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                AttackPlayer();
            }
        }
        else
        {
            navMeshAgent.ResetPath();
        }
    }

    void ChasePlayer()
    {
        if (player == null || navMeshAgent == null) return;

        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(player.position);
            navMeshAgent.speed = isHuman ? humanSpeed : ghostSpeed;
        }
        else
        {
            Debug.LogError("NavMeshAgent is not on the NavMesh. Ensure the agent is correctly placed on a baked NavMesh.");
        }
    }

    void AttackPlayer()
    {
        if (player == null || PlayerBehindWall() || navMeshAgent == null)
        {
            return;
        }

        lastAttackTime = Time.time;
        int damage = isHuman ? humanDamage : ghostDamage;

        if (player != null)
        {
            Debug.Log("Attempting to get PlayerHealth component on: " + player.name);
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("PlayerHealth component found. Applying damage.");
                playerHealth.PlayerTakesDamage(damage); 
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on player object.");
            }
        }
    }

    bool PlayerBehindWall()
    {
        if (player == null) return false;

        RaycastHit hit;
        Vector3 direction = (player.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, direction, out hit, detectRange))
        {
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                return true;
            }
        }
        return false;
    }

    public void ChangeToHuman()
    {
        isHuman = true;
        ghostModel.SetActive(false);
        humanModel.SetActive(true);
    }

    public void ActivateGhost()
    {
        isHuman = false;
        ghostModel.SetActive(true);
        humanModel.SetActive(false);
    }
}