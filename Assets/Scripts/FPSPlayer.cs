using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSPlayer : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2f;

    private Rigidbody playerRigidbody;
    private Camera playerCamera;

    public float interactionDistance = 2f;

    public FPSPlayer playerController;
    public CarController carController;
    public ShopManager shop;
    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private TextMeshProUGUI driveText;

    private void Start()
    {
        // Assuming you have a Rigidbody and Camera component attached to the GameObject
        playerRigidbody = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();

        // Disable the cursor and lock it to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!shop.GetIsOpenShop)
        {
            HandleInput();
        }
        if (!carController.GetIsOnCar)
        {
            driveText.text = "Drive [E]";
            DetectCar();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check if the player is close to a car
            if (TrySwitchController())
            {
                if (!carController.isActiveAndEnabled)
                {
                    carController.enabled = true;
                }
                if (inventoryManager.CarPower > 0)
                {
                    DialogueManager.Instance.StartDialogue(1);
                }
                carController.SetEnable(true);
                playerController.SetEnable(false);
                driveText.text = "Leave [E]";
            }
            else if (TryPressDayPassing())
            {
                DayManager.Instance.OnDayPass();
            }
        }
    }

    public void SetDriveText(string d)
    {
        driveText.text = d;
    }

    private void HandleInput()
    {
        // Player movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * movementSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + transform.TransformDirection(movement));

        // Player camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        float currentRotationX = playerCamera.transform.eulerAngles.x;
        float newRotationX = currentRotationX - mouseY;

        newRotationX = Mathf.Clamp(newRotationX, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(newRotationX, 0f, 0f);
        //Debug.Log("current local rotation : " + playerCamera.transform.localRotation);
    }

    private bool TrySwitchController()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Car"))
            {
                return true;
            }
        }
        

        return false;
    }

    private bool TryPressDayPassing()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("DayButton"))
            {
                return true;
            }
        }
        return false;
    }


    private void DetectCar()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Car"))
            {
                driveText.gameObject.SetActive(true);
            }
        }
        else
        {
            driveText.gameObject.SetActive(false);
        }

    }

    public void SetEnable(bool isEnable)
    {
        playerController.GetComponent<MeshRenderer>().enabled = isEnable;
        playerController.GetComponent<CapsuleCollider>().enabled = isEnable;
        //playerController.GetComponent<Rigidbody>().isKinematic = !isEnable;
        this.enabled = isEnable;
        playerCamera.gameObject.SetActive(isEnable);
    }
}
