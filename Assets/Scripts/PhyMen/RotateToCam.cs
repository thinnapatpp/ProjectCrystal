using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCam : MonoBehaviour
{
    private Camera mainCamera; // Reference to the main camera

    void Start()
    {
        // Find and store a reference to the main camera
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Make sure you have a camera in the scene tagged as 'MainCamera'.");
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Make the object face the camera
            transform.LookAt(mainCamera.transform);
        }
    }
}
