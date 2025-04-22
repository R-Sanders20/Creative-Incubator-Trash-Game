using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController characterController;
    private Vector3 velocity;
    private float gravity = -10f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        Vector3 movement = movementDirection * speed * Time.deltaTime;

        if (!characterController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0; 
        }

        Vector3 finalMovement = new Vector3(movement.x, velocity.y * Time.deltaTime, movement.z);
        characterController.Move(finalMovement);

        // Rotate playermodel
        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }
    }
}