using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private CharacterController characterController;

    public GameObject projectilePrefab;
    public Transform target;
    public float chargeSpeed = 2.0f;
    public float maxChargeTime = 2.0f;
    public Transform objectToFollow;
    //public Camera playerCamera;

    public float jumpForce = 5.0f; // Adjust the jump force in the Inspector
    private Vector3 verticalVelocity = Vector3.zero;
    private bool isGrounded = false;


    private float currentChargeTime = 0.0f;
    [SerializeField]
    private Transform projectileShooter;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Image chargeBow;
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private GameObject anyText;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 moveDirection = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

        Vector3 movement = (moveDirection * verticalInput + Camera.main.transform.right * horizontalInput) * moveSpeed;

        characterController.SimpleMove(movement);

        if(gm.GetArrowAmount() > 0)
        {
            //arrowAmount = 0;
            if (Input.GetMouseButton(1)) // Left mouse button is held down.
            {
                currentChargeTime += Time.deltaTime;
                currentChargeTime = Mathf.Clamp(currentChargeTime, 0, maxChargeTime);
                chargeBow.fillAmount = currentChargeTime / maxChargeTime;
                animator.SetBool("HoldBow", true);
                if (Camera.main.fieldOfView > 40)
                {
                    Camera.main.fieldOfView = Camera.main.fieldOfView - Time.deltaTime * 100;
                }
            }

            if (Input.GetMouseButtonUp(1)) // Left mouse button is released.
            {
                StartCoroutine(ShootArrow());
                chargeBow.fillAmount = 0;
                animator.SetBool("HoldBow", false);
                Camera.main.fieldOfView = 60;
            }
        }
        else
        {
            gm.SetArrowAmount(0);
        }
        
        
        if(maxChargeTime > 1)
        {
            maxChargeTime = 2 - ((gm.aimMastery * 5) / 100);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        isGrounded = characterController.isGrounded;
        if (!isGrounded)
        {
            verticalVelocity.y -= 9.8f * Time.deltaTime;
        }
        Vector3 raycastOrigin = transform.position - Vector3.up * 0.5f; // Bottom of the character

        if (Physics.Raycast(raycastOrigin, -Vector3.up, out RaycastHit hit, 1.0f))
        {
            isGrounded = true;
            //Debug.DrawRay(raycastOrigin, -Vector3.up * 1.0f, Color.green); // Draw the ray in the Scene view
            //Debug.Log("Hit something: " + hit.collider.gameObject.name);
        }
        else
        {
            isGrounded = false;
            //Debug.DrawRay(raycastOrigin, -Vector3.up * 1.0f, Color.red); // Draw the ray in the Scene view
            //Debug.Log("No collision detected.");
        }
    }

    void FixedUpdate()
    {
        characterController.Move(verticalVelocity * Time.fixedDeltaTime);
    }

    void Jump()
    {
        verticalVelocity.y = jumpForce;
    }
    private void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, projectileShooter.position, Quaternion.identity);

        float chargePercentage = currentChargeTime / maxChargeTime;
        float launchForce = chargePercentage * chargeSpeed;

        Vector3 direction = Camera.main.transform.forward.normalized;

        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        rb.velocity = direction * launchForce * 30;
    }
    private IEnumerator ShootArrow()
    {
        Shoot();
        if (gm.arrowCount > 0)
        {
            gm.arrowCount--;
        }
        
        yield return new WaitForSeconds(0.2f);
        currentChargeTime = 0.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            anyText.SetActive(true);
        }
        if (collision.gameObject.TryGetComponent<YellowObject>(out YellowObject yo))
        {
            Destroy(collision.gameObject);
        }

    }


}
