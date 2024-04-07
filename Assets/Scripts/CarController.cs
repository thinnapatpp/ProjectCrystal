using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CarController : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    [SerializeField] private GameObject carBody;
    [SerializeField] private InventoryManager inventoryManager;

    
    private WaitForSeconds waitTime = new WaitForSeconds(5f);

    public Camera camera;

    public bool isOnCar = false;
    public bool GetIsOnCar => isOnCar;

    private float epVal = 100;
    
    public float plusMotorTorque = 0f;
    public float plusBrakeTorque = 0f;
    public int plusEpRateConsumption = 0;

    public float defaultMotorTorque = 500f;
    public float defaultBrakeTorque = 1000f;
    public int defaultEpRateConsumption = 10;
    
    public float motorTorque;
    public float brakeTorque;
    public float epRateConsumption;

    private float horizontalInput;
    private float verticalInput;

    public FPSPlayer playerController;
    public CarController carController;

    [SerializeField]
    private TextMeshProUGUI speedText;

    private void Start()
    {
        ApplyCarMod();
    }

    private void ApplyCarMod()
    {
        motorTorque = defaultMotorTorque + plusMotorTorque;
        brakeTorque = defaultBrakeTorque + plusBrakeTorque;
        epRateConsumption = defaultEpRateConsumption + plusEpRateConsumption;
    }

    public void ApplyModole(ShopManager.CarModule carMod)
    {
        plusMotorTorque = carMod.motorPower;
        plusBrakeTorque = 0;
        plusEpRateConsumption = carMod.energyConsumeRate;
        ApplyCarMod();
    }

    private IEnumerator DecreaseFloatOverTime()
    {
        while (true)
        {
            yield return waitTime;

            
        }
    }



    void Update()
    {
        if (!isOnCar)
        {
            ApplyBrake();
        }
        else
        {
            if(inventoryManager.CarPower > 0)
            {
                Drive();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerController.SetEnable(true);
                carController.SetEnable(false);
                playerController.GetComponent<Rigidbody>().useGravity = false;
                playerController.GetComponent<Rigidbody>().velocity = Vector3.zero;
                playerController.transform.position = carBody.transform.position + Vector3.up * 7f;
                playerController.GetComponent<Rigidbody>().useGravity = true;

            }
        }
        Debug.DrawLine(carBody.transform.position, carBody.transform.position + Vector3.up * 7f, Color.red);
        speedText.text = carBody.GetComponent<Rigidbody>().velocity.magnitude.ToString("F1") + " Kmh";
    }

    private void Drive()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        float motor = verticalInput * motorTorque;

        if (horizontalInput != 0)
        {
            epVal -= 1;
            //Debug.Log("Float value decreased to: " + epVal);
        }

        // Motor Torque
        frontLeftWheel.motorTorque = motor;
        frontRightWheel.motorTorque = motor;
        rearLeftWheel.motorTorque = motor;
        rearRightWheel.motorTorque = motor;

        // Steering
        float steering = horizontalInput * 20f;
        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;

        if (Input.GetKey(KeyCode.Space))
        {
            ApplyBrake();
        }
        else
        {
            ReleaseBrake();
        }

        
        
    }
    
    void ApplyBrake()
    {
        // Apply brake torque to all wheels
        frontLeftWheel.brakeTorque = brakeTorque;
        frontRightWheel.brakeTorque = brakeTorque;
        rearLeftWheel.brakeTorque = brakeTorque;
        rearRightWheel.brakeTorque = brakeTorque;

        // Optionally, you can set motor torque to zero for stronger braking effect
        frontLeftWheel.motorTorque = 0f;
        frontRightWheel.motorTorque = 0f;
        rearLeftWheel.motorTorque = 0f;
        rearRightWheel.motorTorque = 0f;
    }

    void ReleaseBrake()
    {
        // Release the brake torque
        frontLeftWheel.brakeTorque = 0f;
        frontRightWheel.brakeTorque = 0f;
        rearLeftWheel.brakeTorque = 0f;
        rearRightWheel.brakeTorque = 0f;
    }

    void CheckTopContactWithTerrain()
    {
        Vector3 newRotation = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Euler(newRotation);
    }

    public void SetEnable(bool isEnable)
    {
        isOnCar = isEnable;
        camera.gameObject.SetActive(isEnable);
    }
}
