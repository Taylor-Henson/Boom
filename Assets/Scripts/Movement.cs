using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    // Animations
    public Animator animator;
    
    // Input
    float horizontalInput;
    float verticalInput;

    // Movement
    Vector3 moveDirection;
    Rigidbody rb;
    public Transform orientation;
    public float moveSpeed;
    public float groundDrag;

    // Jumping
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump = true;

    [Header("Ground Check")]
    public LayerMask whatIsGround;
    public float playerHeight;
    public bool grounded;

    #region Start and Update

    void Start()
    {
        // References
        rb = GetComponent<Rigidbody>();

        //Freezes the rotation of the Rigidbody
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Calling Methods
        MyInput();
        GroundCheck();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        // Calling Methods
        MovePlayer();
    }

    #endregion

    #region Input

    void MyInput()
    {
        // Movement Input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Jumping Input
        if (Input.GetButton("Jump") && grounded && readyToJump)
        {
            // Calls jumping method
            Jump();

            // Jump Cooldown
            readyToJump = false;
            Invoke("ResetJump", jumpCooldown);
        }

        if (Input.GetButton("Fire2"))
        {
            StartCoroutine(AudioManager.instance.PlayMusic(0));
        }
        if (Input.GetButton("Fire1"))
        {
            AudioManager.instance.PlaySFX(0);
        }
    }

    #endregion

    #region Ground Movement

    void MovePlayer()
    {
        // Calculates the direction the player should move by taking local axis and multiplying them by the values found from inputs
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Applies a force on the player in the direction found before
        if (grounded)
        {
            // If on ground
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            // If not on ground
            rb.AddForce(moveDirection.normalized * moveSpeed * 5f * airMultiplier, ForceMode.Force);
        }
    }

    #endregion

    #region Groundcheck

    void GroundCheck()
    {
        // Raycast from the player downwards checking for ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded)
        {
            // If grounded, apply a ground drag
            rb.linearDamping = groundDrag;
        }
        else
        {
            // If not grounded, remove drag
            rb.linearDamping = 0;
        }
    }

    #endregion

    #region SpeedControl

    void SpeedControl()
    {
        // Creates a Vector3 based off of the current velocity in the x and z axis
        Vector3 flatVel = new Vector3(rb.angularVelocity.x, 0f, rb.angularVelocity.z);

        // Checks if the new Vector3 magnitude is greater than the moveSpeed
        if (flatVel.magnitude > moveSpeed)
        {
            // If so, create a limited velocity of the flat velocity * moveSpeed, and make it the new velocity
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.angularVelocity = new Vector3(limitedVel.x, rb.angularVelocity.y, limitedVel.z);
        }
    }

    #endregion

    #region Jump
    void Jump()
    {
        // Reset and Y velocity currently had
        rb.angularVelocity = new Vector3(rb.angularVelocity.x, 0f, rb.angularVelocity.z);

        // Add jumpForce in the transfornm.up
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        // Makes the jump ready again
        readyToJump = true;
    }

    #endregion
}
