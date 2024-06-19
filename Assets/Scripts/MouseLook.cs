using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLook : MonoBehaviour
{
    Transform playerBody;
    private float normalCamHeight;
    public float mouseSensitivity = 100f;
    public float cameraHeightAdjustSpeed = 5f;
    public static bool isCrouching = false;
    

    float pitch = 0;
    void Start()
    {
        // get player pbject and camera height
        playerBody = transform.parent.transform;
        normalCamHeight = transform.localPosition.y;
        if (MouseSetting.mouseSensitivitySet <= 0)
        {
            mouseSensitivity = 100f;
        }
        else
        {
            mouseSensitivity = MouseSetting.mouseSensitivitySet;
        }

        // deal with cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        // toggle crouching boolean
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
        }

        // set half camera to mimic crouching if crouching
        float crouchHeight = isCrouching ? normalCamHeight / 2 : normalCamHeight;
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Lerp(transform.localPosition.y, crouchHeight, cameraHeightAdjustSpeed * Time.deltaTime);
        transform.localPosition = newPosition;

        // get mouse input for mouse look
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
