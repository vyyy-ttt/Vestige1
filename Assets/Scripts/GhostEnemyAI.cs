using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyAI : MonoBehaviour
{
    // public enum FSMStates
    // {
    //     Idle,
    //     Patrol,
    //     Chase,
    //     Attack,
    //     Dead
    // }

    // public FSMStates currentState;

    // public float attackDistance = 5;
    // public float chaseDistance = 10;
    // public float enemySpeed;
    // public GameObject player;
    // public GameObject spellProjectile;
    // public GameObject wandTip;
    // GameObject[] wanderPoints;
    // Vector3 nextDestination;
    // Animator anim;
    // float distanceToPlayer;
    // public float shootRate = 2;
    // float elapsedTime = 0;

    // int currentDestinationIndex = 0;
    // void Start()
    // {
        
<<<<<<< Updated upstream
    //    Initialize();
=======

        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        Debug.Log("Wander points:" + wanderPoints);
        player = GameObject.FindGameObjectWithTag("Player");
        FindNextPoint();
        currentState = FSMStates.Patrol;
>>>>>>> Stashed changes

    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

    //     switch(currentState)
    //     {
    //         case FSMStates.Patrol:
    //             UpdatePatrolState();
    //             break;
    //         case FSMStates.Chase:
    //             UpdateChaseState();
    //             break;
    //         case FSMStates.Attack:
    //             UpdateAttackState();
    //             break;
    //         case FSMStates.Dead:
    //             UpdateDeadState();
    //             break;

    //     }
    //     elapsedTime += Time.deltaTime;
    // }

<<<<<<< Updated upstream
    // private void Initialize()
    // {
    //     currentState = FSMStates.Patrol;
    //     FindNextPoint();

    //     wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
    //     anim = GetComponent<Animator>();
    //     player = GameObject.FindGameObjectWithTag("Player");
    //     wandTip = GameObject.FindGameObjectWithTag("WandTip");
    // }
    // void UpdatePatrolState()
    // {
    //     print("Patrolling!");
    //     anim.SetInteger("animState", 1);

    //     if(Vector3.Distance(transform.position, nextDestination) < 1)
    //     {
    //         FindNextPoint();
    //     }
    //     else if(distanceToPlayer <= chaseDistance)
    //     {
    //         currentState = FSMStates.Chase;
    //     }
=======
    void Initialize()
    {
        // currentState = FSMStates.Patrol;
        // FindNextPoint();

        // wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        // player = GameObject.FindGameObjectWithTag("Player");
    }
    void UpdatePatrolState()
    {
        //print("Patrolling!");
        //Debug.Log(distanceToPlayer);
        //Debug.Log(nextDestination);
        if(Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else if(distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
>>>>>>> Stashed changes

    //     FaceTarget(nextDestination);

    //     // transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);


    // }
    // void UpdateChaseState()
    // {
    //     anim.SetInteger("animState", 2);

    //     nextDestination = player.transform.position;

    //     if(distanceToPlayer <= attackDistance)
    //     {
    //         currentState = FSMStates.Attack;
    //     }
    //     else if(distanceToPlayer > chaseDistance)
    //     {
    //         currentState = FSMStates.Patrol;
    //     }

    //     FaceTarget(nextDestination);

    //     // transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
    // }
    // void UpdateAttackState()
    // {
    //     print("attack");

    //     nextDestination = player.transform.position;

    //     if(distanceToPlayer <= attackDistance)
    //     {
    //         currentState = FSMStates.Attack;

    //     }
    //     else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
    //     {
    //         currentState = FSMStates.Chase;
    //     }
    //     else if (distanceToPlayer > chaseDistance)
    //     {
    //         currentState = FSMStates.Patrol;
    //     }
        
    //     FaceTarget(nextDestination);

<<<<<<< Updated upstream
    //     anim.SetInteger("animState", 3);

    //     EnemySpellCast(); 

    // }
    // void UpdateDeadState()
    // {
=======
        // should be attacking because of enemy melee attack ?
    }
    void UpdateDeadState()
    {
>>>>>>> Stashed changes
        
    // }

<<<<<<< Updated upstream
    // void FindNextPoint()
    // {
    //     nextDestination = wanderPoints[currentDestinationIndex].transform.position;
=======
    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;
        Debug.Log(nextDestination);
>>>>>>> Stashed changes

    //     currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
    // }

    // void FaceTarget(Vector3 target)
    // {
    //     Vector3 directionToTarget = (target - transform.position).normalized;
    //     directionToTarget.y = 0;
    //     Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
    //     transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    // }

<<<<<<< Updated upstream
    // void EnemySpellCast()
    // {
    //     if(elapsedTime >= shootRate)
    //     {
    //         var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
    //         Invoke("SpellCasting", animDuration);
    //         elapsedTime = 0.0f;

    //     }

    // }

    // void SpellCasting()
    // {
    //     Instantiate(spellProjectile, wandTip.transform.position, wandTip.transform.rotation);
    // }

    // private void OnDrawGizmos()
    // {
    //     //attack
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, attackDistance);

    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere(transform.position, chaseDistance);


    // }
=======
    


    private void OnDrawGizmos()
    {
        //attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);


    }
>>>>>>> Stashed changes
}
