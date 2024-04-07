using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 2.0f;

    private Transform player;

    private float rotationX = 0;

    private void Start()
    {
        player = transform.parent;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        player.transform.Rotate(Vector3.up * mouseX);
    }
}
