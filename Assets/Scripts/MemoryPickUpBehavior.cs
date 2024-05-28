using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryPickUpBehavior : MonoBehaviour
{

    private bool bonusActive;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            FindObjectOfType<LevelManager>().UpdateMemoryCountText();

            Destroy(gameObject);
    

        }

    }



}
