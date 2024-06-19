using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public AudioClip attackSFX;
    public Transform cameraTransform;
    public float attackRange = 2f; 

    private Transform playerTransform;
    private Transform bossTransform;
    private bool canAttack = false;
    private int searchAttempts = 0;
    private const int maxSearchAttempts = 2;

    void Start()
    {
        playerTransform = transform.root;

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        if (searchAttempts < maxSearchAttempts)
        {
            CheckBossProximity();
            if (canAttack && Input.GetKeyDown(KeyCode.B))
            {
                Attack();
            }
        }
    }

    void CheckBossProximity()
    {
        GameObject bossObject = GameObject.FindWithTag("Boss");
        if (bossObject != null)
        {
            bossTransform = bossObject.transform;
            float distanceToBoss = Vector3.Distance(playerTransform.position, bossTransform.position);
            Debug.Log("Distance to boss: " + distanceToBoss);
            canAttack = distanceToBoss <= attackRange;

            PrintHierarchy(bossObject.transform);
        }
        else
        {
            Debug.LogError("Boss not found.");
            searchAttempts++;
        }
    }

    void Attack()
    {
        Debug.Log("Attempting to attack.");
        if (bossTransform != null)
        {
            BossHealth bossHealth = bossTransform.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(attackDamage);
                Debug.Log("Attacked boss for " + attackDamage + " damage.");
                if (attackSFX != null)
                {
                    AudioSource.PlayClipAtPoint(attackSFX, cameraTransform.position);
                }
            }
            else
            {
                Debug.LogError("BossHealth component not found on boss object.");
                PrintHierarchy(bossTransform);
            }
        }
    }

    void PrintHierarchy(Transform parent)
    {
        Debug.Log("Hierarchy of " + parent.name);
        foreach (Transform child in parent)
        {
            Debug.Log("Child: " + child.name);
            PrintHierarchy(child);
        }
    }
}