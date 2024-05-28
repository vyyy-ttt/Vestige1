using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

// need to make enemy die too

public class EnemyBehavior : MonoBehaviour
{
    public Transform playerMoveToward;
    public float moveSpeed = 10f;
    public float minDistance = 4f;
    public int damageAmount = 20;
    bool seenPlayer = false;
    int healthAmount;
    bool enemyDead;
    float distance;
    public GameObject ghostRanParticle; // particle prefab
    public GameObject lootPrefab;
    //Renderer renderer;
    //Color tempcolor;

    //bool enemyAttack;

    // Start is called before the first frame update
    void Start()
    {
        enemyDead = false;
        healthAmount = 100;
        //renderer = gameObject.GetComponent<Renderer>();
        if (playerMoveToward == null)
        {
            playerMoveToward = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.transform;
        }
        gameObject.transform.GetChild(0).gameObject.SetActive(false); // should be false
        InvokeRepeating("SwingWeapon", 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!enemyDead && seenPlayer)
        {
            transform.LookAt(playerMoveToward);
            var step = moveSpeed * Time.deltaTime;
            var distance = (transform.position - playerMoveToward.position).magnitude;
            if (distance > minDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(playerMoveToward.position.x, transform.position.y, playerMoveToward.position.z), step);
            }
            else
            {
                Debug.Log("wait");
                //enemyAttack = true;
                //Invoke("SwingWeapon", 2);


                // play animation on child object
                // check if child hit player (using a different script)
            }
        }
        else if (!enemyDead)
        {
            if ((transform.position - playerMoveToward.position).magnitude < 5)
            {
                seenPlayer = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                // play a sound effect
            }
        }
        //enemyAttack = false;
    }
    /*
    void EnemyAttack()
    {
        while(enemyAttack)
        {
            InvokeRepeating("SwingAxe", 1, 2);
        }
    }
    */
    void SwingWeapon()
    {
        if (!enemyDead && seenPlayer && distance <= minDistance)
        {
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("AxeSwung");
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("AxeReturned");
            Debug.Log("enemy attacked");
        }
    }

    public void EnemyTakesDamage(int amount)
    {
        //SwingWeapon();
        /*tempcolor = GetComponent<MeshRenderer>().material.color;
        tempcolor.a = 1f - ((amount / 100f) / 1.5f);
        GetComponent<MeshRenderer>().material.color = tempcolor;*/
        //renderer.material.color.a = 
        healthAmount -= amount;
        if (healthAmount < 0)
        {
            EnemyDies();
        }
    }

    private void EnemyDies()
    {
        enemyDead = true;
        Debug.Log("enemy ran away");
        // play an animation
        // drop health that player can pick up
        Instantiate(ghostRanParticle, transform.position, Quaternion.Euler(0, 1, 0));
        gameObject.SetActive(false); // dementor will disappear from view
        Instantiate(lootPrefab, transform.position + Vector3.up, transform.rotation);
        Destroy(gameObject, 0.5f); // delay destroying dementor because we reference its posiition and rotation when instantiating

    }
}
