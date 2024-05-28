using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePickups : MonoBehaviour
{
    public float rotationAmount = 45f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationAmount * Time.deltaTime);
    }
}
