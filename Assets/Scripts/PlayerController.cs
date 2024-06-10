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

    CharacterController controller;
    Vector3 input, moveDirection;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
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

            // crouch speed is halved
            if (isCrouching)
            {
                currentSpeed /= 2;
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
                    AudioSource.PlayClipAtPoint(jumpSFX, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
                }
                else
                {
                    moveDirection.y = 0.0f;
                }

            }
            // can move middair
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
}