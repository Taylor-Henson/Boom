using JetBrains.Annotations;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    #region Variables and Refences

    [Header("Input")]  
    float horizontalInput;
    float verticalInput;

    [Header("Movement")]
    Vector3 moveDirection;
    Rigidbody rb;
    public Transform orientation;
    public float moveSpeed;
    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump = true;

    [Header("Ground Check")]
    public LayerMask whatIsGround;
    public float playerHeight = 2;
    public bool grounded;

    [Header("Animations")]
    public Animator cameraAnim;

    [Header("Dash")]
    public TextMeshProUGUI dashesText;
    public int dashes = 3;
    public float dashCoolDown;
    public bool canDash = true;

    #endregion

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
        Animations();

        // Text
        dashesText.text = "" + dashes;

        // Checks if the player has dashes available
        if (dashes <= 0)
        {
            canDash = false;
        }
        else
        {
            canDash = true;
        }
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

        // Dash input
        if (Input.GetKeyDown("c") && canDash)
        {
            // Calls the dash method
            Dash();
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
        grounded = Physics.Raycast(transform.position, Vector3.down, 1.5f, whatIsGround);

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

    #region Animations

    void Animations()
    {
        // Checks for if the player is grounded and moving
        if (grounded && horizontalInput != 0 || verticalInput != 0)
        {
            // Makes the camera bob more
            cameraAnim.SetBool("Moving", true);
        }
        else
        {
            // Ends the animation
            cameraAnim.SetBool("Moving", false);
        }
    }

    #endregion

    #region Dash
    void Dash()
    {
        int force = 20;

        // Applies the dash force
        rb.AddForce(moveDirection.normalized * force, ForceMode.Impulse);

        // Takes away one dash and begins cooldown
        dashes--;
        Invoke("DashCooldown", dashCoolDown);

        // Plays audio
        AudioManager.instance.PlaySFX(2);
    }

    void DashCooldown()
    {
        // Adds one dash store
        dashes++;
    }

    #endregion

    #region Sound

    #endregion
}
