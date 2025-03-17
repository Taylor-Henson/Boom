using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    // References
    public Transform orientation;

    // Sensitivities
    public float sensX;
    public float sensY;

    // Rotations
    float xRotation;
    float yRotation;

    void Start()
    {
        // Locking and hiding the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Creates a float from mouse inputs taken, multiplied by time and sensitivity
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Necessary conversions from input to rotation
        yRotation += mouseX;
        xRotation -= mouseY;

        // Clamping mouse rotation on the Y axis
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //applies rotations onto the camera and the orientation of the player respectively
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
