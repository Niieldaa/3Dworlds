using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
   // Variables
   [SerializeField] private float moveSpeed;
   [SerializeField] private float walkSpeed;
   [SerializeField] private float runSpeed;

   [SerializeField] private bool isGrounded;
   [SerializeField] private float groundCheckDistance;
   [SerializeField] private LayerMask groundMask;
   [SerializeField] private float gravity;
   
   public Transform cameraTransform;        // Reference to the camera transform
   public float jumpCameraOffset = 0.2f;    // Camera offset when jumping
   public float landCameraOffset = -0.2f;   // Camera offset when landing
   public float cameraReturnSpeed = 5f;     //
   private Vector3 initialCameraPosition; 
   
   [SerializeField] private float jumpHeight;
   
   private Vector3 moveDirection;
   private Vector3 velocity;
   
   // References
   private CharacterController controller;
   private Animator anim;

   // New jump control variables
   private bool isJumping = false; // Ensures jump only triggers once until landing

   private void Start()
   {
      initialCameraPosition = cameraTransform.localPosition;
      controller = GetComponent<CharacterController>();
      anim = GetComponentInChildren<Animator>();
   }

   private void Update()
   {
      Move();
      // Check if player has landed
      if (isJumping && velocity.y <= 0 && isGrounded)
      {
         OnLand();
      }
   }

   private void Move()
   {
      // Check if grounded and update IsJumping based on that
      isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

      // If grounded, reset vertical velocity and jumping state
      if (isGrounded && velocity.y < 0)
      {
         velocity.y = -2f;   // Apply a slight downward force to help stay grounded
         if (isJumping)
         {
            isJumping = false;    // Reset jumping flag
            anim.SetBool("IsJumping", false); // Update animation to return to blend tree
         }
      }
      else if (!isGrounded)
      {
         // Apply gravity if not grounded
         velocity.y += gravity * Time.deltaTime;
      }
      

      // Handle movement input
      float moveZ = Input.GetAxis("Vertical");
      float moveX = Input.GetAxis("Horizontal");
      moveDirection = new Vector3(moveX, 0, moveZ).normalized;
      //moveDirection = transform.TransformDirection(moveDirection);
      if (isGrounded)
      {
         if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
         {
            Run();
         }
         else if (moveDirection != Vector3.zero)
         {
            Walk();
         }
         else
         {
            Idle();
         }

         // Apply movement speed based on walking or running
         moveDirection *= moveSpeed;

         // Jumping input
         if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
         {
            Jump();
         }
      }

      // Convert move direction to world space and apply it
      Vector3 move = transform.TransformDirection(moveDirection);
      controller.Move(move * Time.deltaTime);

      // Apply only the Y component of velocity to handle jumping and gravity
      controller.Move(new Vector3(0, velocity.y, 0) * Time.deltaTime);
   }

   private void Idle()
   {
      anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
   }
   
   private void Walk()
   {
      moveSpeed = walkSpeed;
      anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
   }
   
   private void Run()
   {
      moveSpeed = runSpeed;
      anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
   }
   
   private void Jump()
   {
      // Calculate jump velocity based on desired height and gravity
      velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
      anim.SetBool("IsJumping", true);   // Set jumping animation
      isJumping = true;                  // Prevent multiple jumps until landing
      cameraTransform.localPosition = initialCameraPosition + new Vector3(0, jumpCameraOffset, 0);
   }
   private void OnLand()
   {
      anim.SetBool("IsJumping", false);     // Reset jumping animation
      isJumping = false;

      // Move the camera down slightly when landing
      cameraTransform.localPosition = initialCameraPosition + new Vector3(0, landCameraOffset, 0);
   }

   private void LateUpdate()
   {
      // Smoothly return the camera to its original position
      cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, initialCameraPosition, Time.deltaTime * cameraReturnSpeed);
   }
}
