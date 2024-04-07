using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public float interactionDistance = 2f;
    public KeyCode switchKey = KeyCode.E;

    public FPSPlayer playerController;
    public CarController carController;

    private bool isPlayerControllerActive = true;

    private void Update()
    {
        // Detect "E" key press
        if (Input.GetKeyDown(switchKey))
        {
            // Check if the player is close to a car
            if (TrySwitchController())
            {
                // Toggle between player and car controllers
                isPlayerControllerActive = !isPlayerControllerActive;
                SetControllerActive(isPlayerControllerActive);
            }
        }
    }

    private bool TrySwitchController()
    {
        // Raycast to check if the player is close to a car
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            // Check if the object hit is a car
            if (hit.collider.CompareTag("Car"))
            {
                return true;
            }
        }

        return false;
    }

    private void SetControllerActive(bool isPlayerActive)
    {
        // Enable/disable the corresponding controllers
        playerController.GetComponent<MeshCollider>().enabled = false;
        playerController.GetComponent<CapsuleCollider>().enabled = false;
        playerController.SetEnable(isPlayerActive);
        carController.enabled = !isPlayerActive;
        

        // Optionally, you may want to hide/show the corresponding GameObjects or perform other actions.
    }
}
