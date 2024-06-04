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

    // Start is called before the first frame update
    void Start()
    {
        if (playerMoveToward == null)
        {
            playerMoveToward = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.transform;
        }
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (seenPlayer)
        {
<<<<<<< Updated upstream
=======
            PlayerAttack.disableTeleport = false;
>>>>>>> Stashed changes
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
                gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("AxeSwung");
                gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("AxeReturned");


                // play animation on child object
                // check if child hit player (using a different script)
            }
        }
        else
        {
            if ((transform.position - playerMoveToward.position).magnitude < 5)
            {
                seenPlayer = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                // play a sound effect
            }
        }
    }
}
