using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// need to make enemy die too

public class Level2EnemyBehavior : MonoBehaviour
{
    // reference to player child object,a tiny transparent capsule at same y position as ghost
    // prevents ghost from attacking at weird angles
    public Transform playerMoveToward;

    public float moveSpeed = 2f;
    public float minDistance = 1f; // enemy stops here and attacks
    public int damageAmount = 20;
    public float enemySeesPlayerDistance = 5f;

    // when seenPlayer, weapon becaomes active and enemy attacks, otherwise is idle
    bool seenPlayer = false;
    int healthAmount; // of enemy
    bool isDead; 

    float distance; // for calculations
    public GameObject ghostRanParticle; // particle prefab
    public GameObject lootPrefab;
    public AudioClip slashSFX;
    public Transform cameraTransform; // for playing audio (camera.main.transform.position giving errors)

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        healthAmount = 270;
        if (playerMoveToward == null)
        {
            playerMoveToward = GameObject.FindGameObjectWithTag("MoveToward").transform;
        }
        gameObject.transform.GetChild(0).gameObject.SetActive(false); // set enemy weapon to inactive
        InvokeRepeating("SwingWeapon", 2, 2);   // also requires conditionals, but enemy will swing axe every 2 seconds when appropriate
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y > 4f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 4f, gameObject.transform.position.z);
        }
        if (!isDead)
        {
            if ((transform.position - playerMoveToward.position).magnitude > enemySeesPlayerDistance)
            {
                seenPlayer = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }

            if ((transform.position - playerMoveToward.position).magnitude < enemySeesPlayerDistance)
            {
                seenPlayer = true;
            }
            
            if (seenPlayer)
            {
                PlayerAttack.disableTeleport = false;
                transform.LookAt(playerMoveToward);
            }
        }
    }

    // if enemy sees player, is alive, and close enough, swing weapon (has collider) and play sound
    void SwingWeapon()
    {
        if (!isDead && seenPlayer && distance <= minDistance)
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
        if (healthAmount <= 0)
        {
            EnemyDies();
        }
        Debug.Log("Enemy took damage, current health: " + healthAmount);
    }

    private void EnemyDies()
    {
        isDead = true;
        PlayerAttack.disableTeleport = false;
        Debug.Log("enemy ran away");
        // play particle effect, drop loot, destroy enemy
        Instantiate(ghostRanParticle, transform.position, Quaternion.Euler(0, 1, 0));
        gameObject.SetActive(false);
        Instantiate(lootPrefab, transform.position + Vector3.up, transform.rotation);
        Destroy(gameObject, 0.5f);
        FindObjectOfType<LevelManager>().UpdateKillCountText();
    }
}
