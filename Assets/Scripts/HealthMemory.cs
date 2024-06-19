using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMemory : MonoBehaviour
{

    public int healthAmount = 10;
    public AudioClip pickupSFX;
    public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 360 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && playerHealth.currentHealth < playerHealth.startingHealth)
            {
                playerHealth.TakeHealth(healthAmount);
                // Play pickup sound
                if (pickupSFX != null)
                {
                    AudioSource.PlayClipAtPoint(pickupSFX, cameraTransform.position);
                }
                // Disable and destroy the item after a short delay
                gameObject.SetActive(false);
                Destroy(gameObject, 0.5f);
            }
            else
            {
                Debug.Log("Player health is already at maximum.");
            }
        }
    }
}
