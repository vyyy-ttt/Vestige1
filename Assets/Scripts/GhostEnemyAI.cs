using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyAI : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }

    public FSMStates currentState;

    public float attackDistance = 5;
    public float chaseDistance = 10;
    public float enemySpeed;
    public GameObject player;
    public Transform enemyEyes;
    public float fieldOfView = 45f;
    GameObject[] wanderPoints;
    Vector3 nextDestination;
    float distanceToPlayer;
    float elapsedTime = 0;
    UnityEngine.AI.NavMeshAgent agent;

    int currentDestinationIndex = 0;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        player = GameObject.FindGameObjectWithTag("Player");
       currentState = FSMStates.Patrol;
        FindNextPoint();


    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch(currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;

        }
        elapsedTime += Time.deltaTime;
    }

    void UpdatePatrolState()
    {
        //print("Patrolling!");
        //Debug.Log(distanceToPlayer);

        agent.stoppingDistance = 0;
        agent.speed = 5f;

        if(Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else if(distanceToPlayer <= chaseDistance && IsPlayerInClearFOV() && PlayerAttack.playerAttackedEnemy == true)
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);

        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
        agent.SetDestination(nextDestination);

    }
    void UpdateChaseState()
    {

        nextDestination = player.transform.position;
        agent.stoppingDistance = attackDistance;
        agent.speed = 7;

        // if(distanceToPlayer <= attackDistance) // disable to switch to attack for now
        // {
        //     currentState = FSMStates.Attack;
        // }
        if(distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
        agent.SetDestination(nextDestination);
    }
    void UpdateAttackState()
    {
        //print("attack");

        nextDestination = player.transform.position;

        if(distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;

        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }
        
        FaceTarget(nextDestination);

    }
    void UpdateDeadState()
    {
        
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
        agent.SetDestination(nextDestination);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        //attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) * frontRayPoint;

        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.yellow);
        Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.yellow);

    }

    bool IsPlayerInClearFOV()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;

        if(Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView)
        {
            if(Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance))
            {
                if(hit.collider.CompareTag("Player"))
                {
                    print("Player in sight!");
                    return true;
                }
                return false;
            }
            return false;
        }
        return false;
    }
}