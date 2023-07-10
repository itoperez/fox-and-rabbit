using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonFoxMovement : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float jumpForce;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistanceRadius;
    private bool isGrounded;

    private Rigidbody rb;
    AudioSource audioData;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioData = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistanceRadius, groundMask);
    }

    private void FixedUpdate()
    {
        // make world direction into local direction
        Vector3 localDirection = transform.TransformDirection(direction);
        // move using physics
        rb.MovePosition(rb.position + (localDirection * speed * Time.deltaTime));
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
            //Debug.Log("Jumping");
            audioData.Play();
        }
    }
}
