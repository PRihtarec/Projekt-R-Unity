using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource hodanjeSource;
    public AudioSource brzoHodanjeSource;


    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float normalHeight = 1.3f;
    public float crouchingHeight = 0.8f;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;
    [HideInInspector] public float crouchSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [HideInInspector] public TextMeshProUGUI text_speed;
    GamesController gamesController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        gamesController = FindObjectOfType<GamesController>();

        walkSpeed = moveSpeed; 
        sprintSpeed = moveSpeed * 1.5f; 
        crouchSpeed = moveSpeed  * 0.5f;
    }

    private void Update()
    {
        if (!gamesController.isMinigameInProgress()){
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        }
    }

    private void FixedUpdate()
    {
        if (!gamesController.isMinigameInProgress()){
        MovePlayer();
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if(!isMoving)
        {
            if (hodanjeSource.isPlaying)
            {
                hodanjeSource.Stop();
            }
            if (brzoHodanjeSource.isPlaying)
            {
                brzoHodanjeSource.Stop();
            }
        }

        // when to jump
     //   if(Input.GetKey(jumpKey) && readyToJump && grounded)
     //   {
     //       readyToJump = false;
//
     //       Jump();
//
    //        Invoke(nameof(ResetJump), jumpCooldown);
    //    }

        // Check if sprinting
        if (Input.GetKey(sprintKey) && !Input.GetKey(KeyCode.LeftControl))
        {
            if (!brzoHodanjeSource.isPlaying)
            {
                brzoHodanjeSource.Play();
            }
            if (hodanjeSource.isPlaying)
            {
                hodanjeSource.Stop();
            }
            moveSpeed = sprintSpeed;
        }
        else
        {
            if (!hodanjeSource.isPlaying)
            {
                hodanjeSource.Play();
            }
            if (brzoHodanjeSource.isPlaying)
            {
                brzoHodanjeSource.Stop();
            }
            moveSpeed = walkSpeed;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Vector3 currentScale = transform.localScale;
            transform.localScale = new Vector3(currentScale.x, crouchingHeight, currentScale.z);
            moveSpeed = crouchSpeed;
        }
        else
        {
            Vector3 currentScale = transform.localScale;
            transform.localScale = new Vector3(currentScale.x, normalHeight, currentScale.z);
        }

    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

       // text_speed.SetText("Speed: " + flatVel.magnitude);
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
    public bool IsPlayerMovingByInput()
{
    // Check if there's any non-zero input
    return horizontalInput != 0 || verticalInput != 0;
}
}
