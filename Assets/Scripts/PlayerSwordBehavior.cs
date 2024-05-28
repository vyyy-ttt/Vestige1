using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordBehavior : MonoBehaviour
{
    public static bool swordIsActive;
    // Start is called before the first frame update
    void Start()
    {
        bool swordIsActive = false;
        gameObject.transform.GetChild(2).gameObject.SetActive(swordIsActive);   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            swordIsActive = !swordIsActive;
            gameObject.transform.GetChild(2).gameObject.SetActive(swordIsActive);
        }
        if (swordIsActive && Input.GetButtonDown("Fire1"))
        {
            gameObject.transform.GetChild(2).GetComponent<Animator>().SetTrigger("SwordSwung");
            gameObject.transform.GetChild(2).GetComponent<Animator>().SetTrigger("SwordReturned");
        }
    }
}
