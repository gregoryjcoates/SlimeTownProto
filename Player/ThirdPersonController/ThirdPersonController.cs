using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float rotationSpeed = 100.0f;
    private CharacterController controller;
    private bool grounded;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.0f;
    private Vector3 velocity;

    private void Start()
    {
        controller= gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * movementSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (move != Vector3.zero)
        {
            gameObject.transform.Rotate(0,move.x,0);
        }

        grounded = controller.isGrounded;

        if (grounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        velocity.y += gravityValue * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}