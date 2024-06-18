using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public int attackDamage = 20; 
    public AudioClip attackSFX; 
    public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            BossHealth bossHealth = other.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(attackDamage);
                if (attackSFX != null)
                {
                    AudioSource.PlayClipAtPoint(attackSFX, cameraTransform.position);
                }
            }
            else
            {
                Debug.LogError("BossHealth component not found on boss object.");
            }
        }
    }
}
