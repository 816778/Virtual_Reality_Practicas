using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    public float mouseSensitivity = 1.0f; // Adjust mouse speed
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Update()
    {
        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Adjust rotation based on mouse input
        rotationX -= mouseY * mouseSensitivity;
        rotationY += mouseX * mouseSensitivity;

        // Apply rotation
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}

