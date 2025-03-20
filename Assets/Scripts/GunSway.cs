using UnityEngine;

public class GunSway : MonoBehaviour
{
    public float intensity;
    public float smooth;

    private Quaternion originRotation; 

    void Start()
    {
        // sets default rotation
        originRotation = transform.localRotation;
    }

    void Update()
    {
        Sway();
    }

    void Sway()
    {
        // gets mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // calculate target rotation
        Quaternion targetXAdjustment = Quaternion.AngleAxis(-intensity * mouseX, Vector3.up);
        Quaternion targetYAdjustment = Quaternion.AngleAxis(intensity * mouseY, Vector3.right);
        Quaternion targetRotation = originRotation * targetXAdjustment * targetYAdjustment;

        // rotate towards target
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }
}
