using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float rotationSpeed = 100.0f;
    public CharacterController controller;
    private bool grounded;
    private float jumpHeight = 5.0f;
    private float gravityValue = -200.0f;
    private Vector3 velocity;
    private int layerMask = 1;

    public Vector3 playerLocation;
    private void Start()

    {
        controller= gameObject.AddComponent<CharacterController>();
    }
    

    // Update is called once per frame
    void Update()
    {
        grounded = controller.isGrounded;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask ))
        {
            
        }


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
        

        if (grounded && velocity.y < 1)
        {
            velocity.y = 0f;
        }
        
        if (Input.GetButtonDown("Jump") && hit.distance < 1 )
        {
            Debug.Log("jump");
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerLocation = gameObject.transform.position;
        velocity.y += gravityValue * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerAttack();
        }

    }


    // attack detects that the target hit is an enemy and brings their health down by 1
    void PlayerAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if (hit.collider.gameObject.tag == "Enemy")
            {
                GameObject a = hit.collider.gameObject;
                a.GetComponent<Enemy2>().enemyHealth -= 1;
            }
        }
    }
}