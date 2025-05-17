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
    Rigidbody rb;
    public Transform orientation;
    Vector3 moveDirection;
    public float groundDrag;
    float moveSpeed = 60;

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
        // Calling methods
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

        // Calculates the force in each direction the player can be moving in using input combines with directions
        // This is called from an update based method to sync with the orientation being calculated based on each frame
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Jumping Input
        if (Input.GetButton("Jump") && grounded && readyToJump && GameObject.Find("Canvas").GetComponent<PauseMenu>().menu != true && !GameManager.instance.dead && !GameManager.instance.gameOver)
        {
            // Calls jumping method
            Jump();

            // Jump Cooldown
            readyToJump = false;
            Invoke("ResetJump", jumpCooldown);
        }

        // Dash input
        if (Input.GetKeyDown("c") && canDash && GameObject.Find("Canvas").GetComponent<PauseMenu>().menu != true && !GameManager.instance.dead && !GameManager.instance.gameOver)
        {
            // Calls the dash method
            Dash();
        }
    }

    #endregion

    #region Ground Movement

    void MovePlayer()
    {
        // Applies a force on the player in the direction found before
        // Force is applied in a FixedUpdate based method so it is applied evenly, not dependant on frame rate
        if (grounded && !GameManager.instance.dead && !GameManager.instance.gameOver)
        {
            // If on ground
            rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
        }
        else if (!grounded && !GameManager.instance.dead && !GameManager.instance.gameOver)
        {
            // If not on ground
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
        }

        // Plays moving animation
        if (moveDirection.magnitude >= 0.1 && grounded && !GameManager.instance.dead)
        {
            cameraAnim.SetBool("Moving", true);
        }
        else
        {
            cameraAnim.SetBool("Moving", false);
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
        int force = 10;

        // Reset velocity
        rb.angularVelocity = new Vector3(0f, rb.angularVelocity.y, 0f);

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
}
