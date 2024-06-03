using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordBehavior : MonoBehaviour
{
    public AudioClip slashSFX;
    public Transform cameraTransform;
    public static bool swordIsActive; // whether sword is out
    public static bool hasSword = false;
    // Start is called before the first frame update
    void Start()
    {
        // start with no sword
        bool swordIsActive = false;
        gameObject.transform.GetChild(2).gameObject.SetActive(swordIsActive);   
    }

    // Update is called once per frame
    void Update()
    {
        // toggle sword when click R
        if (Input.GetKeyDown(KeyCode.R) && hasSword)
        {
            swordIsActive = !swordIsActive;
            gameObject.transform.GetChild(2).gameObject.SetActive(swordIsActive);
        }
        // play animation is sword out and click mouse button, play audio
        if (swordIsActive && Input.GetButtonDown("Fire1"))
        {
            gameObject.transform.GetChild(2).GetComponent<Animator>().SetTrigger("SwordSwung");
            gameObject.transform.GetChild(2).GetComponent<Animator>().SetTrigger("SwordReturned");
            AudioSource.PlayClipAtPoint(slashSFX, cameraTransform.position);
        }
    }
}
