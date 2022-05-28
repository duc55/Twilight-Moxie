using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // === PARAMETERS =====
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravityMultiplier;
    [SerializeField] Vector3 velocity;
    [SerializeField] bool isGrounded;

    // === COMPONENTS =====
    CharacterController characterController;


    void Start() 
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Check if landing
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0f) {
            velocity.y = 0f;
        }

        Move();

        Jump();

        //Fall with gravity
        velocity += Physics.gravity * Time.deltaTime * gravityMultiplier;
        characterController.Move(velocity * Time.deltaTime);
    }

    void Move() 
    {
        // Move with input
        Vector3 inputDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        characterController.Move(inputDir * moveSpeed * Time.deltaTime);

        // Turn towards move direction
        if (inputDir != Vector3.zero) {
            Quaternion toRotation = Quaternion.LookRotation(inputDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        // Changes the height position of the player
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity += Vector3.up * jumpForce;
        }
    }
}
