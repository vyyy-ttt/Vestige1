using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool pauseMovement;
    public float moveSpeed = 3f;
    public float jumpHeight = 10;
    public float gravity = 9.81f;
    public float airControl = 10;
    private bool isCrouching = false;
    public AudioClip jumpSFX;

    private bool isInSafeZone = false;
    public float health = 100f;
    public float maxHealth = 100f;

    CharacterController controller;
    Vector3 input, moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!pauseMovement)
        {
            float currentSpeed = moveSpeed;

            // crouching toggle
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isCrouching = !isCrouching;
            }

            // crouch speed is set to 5
            if (isCrouching)
            {
                currentSpeed = 5f;
            }

            // run speed is doubled
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed *= 2;
            }

            // controller movements from WASD
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // diagonal doesn't give extra speed
            input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
            input *= currentSpeed;

            // can jump if character grounded
            if (controller.isGrounded)
            {
                moveDirection = input;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                    AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position);
                }
                else
                {
                    moveDirection.y = 0.0f;
                }

            }
            // can move mid-air
            else
            {
                input.y = moveDirection.y;
                moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
            }
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void EnterSafeZone()
    {
        isInSafeZone = true;
        Debug.Log("Player has entered the safe zone.");
    }

    public void ExitSafeZone()
    {
        isInSafeZone = false;
        Debug.Log("Player has exited the safe zone.");
    }

    public bool IsInSafeZone()
    {
        return isInSafeZone;
    }
}