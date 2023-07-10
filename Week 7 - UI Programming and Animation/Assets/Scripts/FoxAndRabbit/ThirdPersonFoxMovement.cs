using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonFoxMovement : MonoBehaviour
{
    // Player's movement parameters
    public Vector3 direction;
    public float speed;
    public float jumpForce;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistanceRadius;
    private bool isGrounded;

    public Transform cameraRig;
    public Animator animator;

    private Rigidbody rb;

    AudioSource audioData;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
        audioData = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Debug.Log(isGrounded);
        // Check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistanceRadius, groundMask);
    }

    private void FixedUpdate()
    {
        if (direction.magnitude >= 0.1f)
        {
            // make world direection into local direction
            Vector3 localDirection = transform.TransformDirection(direction);

            // Get the correct direction forward
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraRig.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // move using physics
            rb.MovePosition(rb.position + (moveDir.normalized * speed * Time.deltaTime));

            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    public void OnMove(InputValue value)
    {
        // A vector with x and y component corresponding to the player's WASD and arrow inputs
        // both components are in the range [-1,1]
        Vector2 inputVector = value.Get<Vector2>();

        // Move in the XZ-plane
        direction.x = inputVector.x;
        direction.z = inputVector.y;

        // just in case
        direction.y = 0f;
        //Debug.Log("Moving");
    }

    public void OnJump(InputValue value)
    {
        float jump = value.Get<float>();
        if (jump == 1 & isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jumping");
            //Debug.Log("Jumping");
            audioData.Play();
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the groundCheck with raius
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundDistanceRadius);
    }

}
