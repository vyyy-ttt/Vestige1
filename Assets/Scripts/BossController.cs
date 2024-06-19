using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public GameObject ghostModel;
    public GameObject humanModel;
    public float detectRange = 15f;
    public float ghostSpeed = 2f;
    public float humanSpeed = 4f;
    public int ghostDamage = 10;
    public int humanDamage = 12;
    public float attackCooldown = 2f;
    public bool isHuman = false;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private float lastAttackTime;
    private bool isAttackingPlayer = false;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
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

        lastAttackTime = -attackCooldown;
        ActivateGhost();
    }

    void Update()
    {
        if (player == null || navMeshAgent == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        Debug.Log("Distance to player: " + distance);
        if (distance <= detectRange)
        {
            ChasePlayer();
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
            navMeshAgent.stoppingDistance = 1f;
        }
        else
        {
            Debug.LogError("NavMeshAgent is not on the NavMesh. Ensure the agent is correctly placed on a baked NavMesh.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isAttackingPlayer)
            {
                isAttackingPlayer = true;
                StartCoroutine(ContinuousAttack(other.GetComponent<PlayerHealth>()));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttackingPlayer = false;
            StopAllCoroutines();
        }
    }

    IEnumerator ContinuousAttack(PlayerHealth playerHealth)
    {
        while (isAttackingPlayer)
        {
            AttackPlayer(playerHealth);
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    void AttackPlayer(PlayerHealth playerHealth)
    {
        Debug.Log("Attempting to attack player.");
        if (playerHealth == null) return;

        int damage = isHuman ? humanDamage : ghostDamage;

        playerHealth.PlayerTakesDamage(damage);
        Debug.Log("Attacked player for " + damage + " damage.");
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
        navMeshAgent.speed = humanSpeed;
        Debug.Log("Boss changed to human stage.");
    }

    public void ActivateGhost()
    {
        isHuman = false;
        ghostModel.SetActive(true);
        humanModel.SetActive(false);
        navMeshAgent.speed = ghostSpeed;
    }
}