using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Transform playerBody;
    private float normalCamHeight;
    public float mouseSensitivity = 10;
    public float cameraHeightAdjustSpeed = 2f;
    private bool isCrouching = false;

    float pitch = 0;
    void Start()
    {
        playerBody = transform.parent.transform;
        normalCamHeight = transform.localPosition.y;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
        }

        // half camera to mimic crouching
        float crouchHeight = isCrouching ? normalCamHeight / 2 : normalCamHeight;
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Lerp(transform.localPosition.y, crouchHeight, cameraHeightAdjustSpeed * Time.deltaTime);
        transform.localPosition = newPosition;


        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // yaw
        playerBody.Rotate(Vector3.up * moveX);

        // pitch
        pitch -= moveY;

        pitch = Mathf.Clamp(pitch, -90f, 90f);
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
