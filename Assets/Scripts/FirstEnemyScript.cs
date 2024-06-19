using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

// need to make enemy die too

public class FirstEnemyScript : MonoBehaviour
{
    // reference to player child object,a tiny transparent capsule at same y position as ghost
    // prevents ghost from attacking at weird angles
    public Transform playerMoveToward;

    public float moveSpeed = 2f;
    public float minDistance = 1f; // enemy stops here and attacks
    public int damageAmount = 20;

    // when seenPlayer, weapon becaomes active and enemy attacks, otherwise is idle
    public static bool seenPlayer;
    int healthAmount; // of enemy
    public static bool enemiesDead; // for whether player can move on from Level 1

    float distance; // for calculations
    public GameObject ghostRanParticle; // particle prefab
    //public GameObject firstEnemyPrefab;
    public GameObject lootPrefab;
    public AudioClip slashSFX;
    public AudioClip enemyDeathSFX;
    public Transform cameraTransform; // for playing audio (camera.main.transform.position giving errors)

    static bool enemyReset = false;
    // Start is called before the first frame update
    void Start()
    {
        seenPlayer = false;
        enemiesDead = false;
        //enemyReset = false;
        healthAmount = 100;
        //if (playerMoveToward == null)
        //{
        playerMoveToward = GameObject.FindGameObjectWithTag("MoveToward").transform;
        //}
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        gameObject.transform.GetChild(0).gameObject.SetActive(false); // set enemy weapon to inactive
        InvokeRepeating("SwingWeapon", 2, 2);   // also requires conditionals, but enemy will swing axe every 2 seconds when appropriate
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1.975072f, gameObject.transform.position.z);
        // if enemy sees player, look at player
        if (!enemiesDead && seenPlayer)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            transform.LookAt(playerMoveToward);
            var step = moveSpeed * Time.deltaTime;
            var distance = (transform.position - playerMoveToward.position).magnitude;

            // move to player if enough distance between
            if (distance > minDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(playerMoveToward.position.x, transform.position.y, playerMoveToward.position.z), step);
            }
        }
        /*
        // if enemy is alive but hasn't seen player, look at player and draw weapon when 5 units close
        else if (!enemiesDead && enemyReset)
        {
            if ((transform.position - playerMoveToward.position).magnitude < 5.5f)
            {
                Debug.Log("seeing player");
                seenPlayer = true;
                PlayerAttack.disableTeleport = true;
                //gameObject.transform.GetChild(0).gameObject.SetActive(true);
                // should play a sound effect
            }

            //gameObject.transform.position = new Vector3(-1.307f, 1.975072f, 29.961f);
            //gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        }*/

    }

    // if enemy sees player, is alive, and close enough, swing weapon (has collider) and play sound
    void SwingWeapon()
    {
        //var distance = (transform.position - playerMoveToward.position).magnitude;
        if (!enemiesDead && seenPlayer && distance <= minDistance)
        {
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("AxeSwung");
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("AxeReturned");
            AudioSource.PlayClipAtPoint(slashSFX, cameraTransform.position);
            Debug.Log("enemy attacked");
        }
    }

    public void EnemyTakesDamage(int amount)
    {
        // would be cool if ghost turned more transparent as health went down
        healthAmount -= amount;
        if (healthAmount < 0)
        {
            EnemyDies();
        }
    }

    private void EnemyDies()
    {
        AudioSource.PlayClipAtPoint(enemyDeathSFX, cameraTransform.position);
        enemiesDead = true;   // player may continue (can modify this, have an enemiesDead static for clearing room and enemyDead for each)
        Debug.Log("enemy ran away");
        // play particle effect, drop loot, destroy enemy
        Instantiate(ghostRanParticle, transform.position, Quaternion.Euler(0, 1, 0));
        gameObject.SetActive(false);
        Instantiate(lootPrefab, transform.position + Vector3.up, transform.rotation);
        Destroy(gameObject, 0.5f);
    }
    /*
    // called when player dies
    public void ResetEnemy()
    {
        Debug.Log("move enemy back");
        Instantiate(firstEnemyPrefab, new Vector3(-1.307f, 1.975072f, 29.961f), Quaternion.Euler(Vector3.zero)); 
        //gameObject.transform.position = new Vector3(-1.307f, 1.975072f, 29.961f);
        //gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        seenPlayer = false;
        enemyReset = true;
        Destroy(gameObject);
        // within 5.5 units
    }
    */
}
