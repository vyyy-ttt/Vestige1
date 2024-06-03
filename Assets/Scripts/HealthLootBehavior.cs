using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLootBehavior : MonoBehaviour
{
    public int healthAmount = 15;
    public AudioClip pickupSFX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // rotate object
        transform.Rotate(Vector3.forward, 360 * Time.deltaTime);
    }

    // give player health and then self destroy
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(pickupSFX, other.transform.position);
            FindObjectOfType<PlayerHealth>().TakeHealth(healthAmount);

            Destroy(gameObject, 0.5f);
        }
    }

}
