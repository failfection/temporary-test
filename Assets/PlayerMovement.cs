using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //TODO Fix Weapon clipping in walls 

    [SerializeField] private Rigidbody PlayerRigidbody;
    float moveSpeed;
    public float walkSpeed = 800f;
    public float runSpeed = 1500f;
    public float jumpHeight = 1000f;
    public bool playerOnGround = true; // for jumping
    public float GravityStrength = -50f; // Testing Gravity
    public GrapplingGun grappling;

    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 gravityS = new Vector3(0f, GravityStrength, 0f);
        Physics.gravity = gravityS;
        Move();
        Jump();
    }


    private void Move()
    {
        Run();

        Walk();

        print(PlayerRigidbody.velocity);
    
       
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }


        else
        {
            moveSpeed = walkSpeed;
        }
    }
    private void Walk()
    {
        if (Input.GetKey(KeyCode.W))
        {
            PlayerRigidbody.AddRelativeForce(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            PlayerRigidbody.AddRelativeForce(Vector3.left * moveSpeed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.S))
        {
            PlayerRigidbody.AddRelativeForce(Vector3.back *moveSpeed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.D))
        {
            PlayerRigidbody.AddRelativeForce(Vector3.right * moveSpeed * Time.deltaTime);

        }
    }

    private void Jump()
    {
                if (Input.GetKeyDown(KeyCode.Space) && playerOnGround)
                {
                    PlayerRigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                    playerOnGround = false;
            
                }              
    }


    private void OnCollisionEnter(Collision collision)
    {
                if (collision.gameObject.tag == "ground") // Prevents player from doing double or jumping in the air
                {
                    playerOnGround = true;
                }
    }
    

}
