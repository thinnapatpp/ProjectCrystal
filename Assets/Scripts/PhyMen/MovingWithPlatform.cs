using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWithPlatform : MonoBehaviour
{
    private Transform platformTransform;
    private CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    void Update()
    {
        platformTransform = transform.parent;
        if (platformTransform != null)
        {
            // Calculate the offset between player and platform
            Vector3 platformMovement = platformTransform.position - transform.position;

            // Move the player along with the platform
            controller.Move(platformMovement);
            //transform.position += ;
        }
    }
}
