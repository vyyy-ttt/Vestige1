using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLootBehavior : MonoBehaviour
{
    public int healthAmount = 15;
    public AudioClip healthSFX;
    Vector3 startPosition;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        speed = Random.Range(0.5f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // rotate object
        transform.Rotate(Vector3.forward, 360 * Time.deltaTime);



        Vector3 newPosition = transform.position;
        newPosition.y = startPosition.y + Mathf.Sin(Time.time * speed) * .2f; // returns time from beginning of the game, between -1 and 1
        transform.position = newPosition;


    }

    // give player health and then self destroy
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(healthSFX, other.transform.position);
            FindObjectOfType<PlayerHealth>().TakeHealth(healthAmount);

            Destroy(gameObject, 0.5f);
        }
    }

}
