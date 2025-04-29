using UnityEngine;

public class GunSway : MonoBehaviour
{
    [Header("Swaying variables")]
    private Quaternion originRotation; 
    public float intensity;
    public float smooth;

    void Start()
    {
        // Sets default rotation
        originRotation = transform.localRotation;
    }

    void Update()
    {
        // Calling methods
        Sway();
    }

    void Sway()
    {
        // Gets mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate target rotation
        Quaternion targetXAdjustment = Quaternion.AngleAxis(-intensity * mouseX, Vector3.up);
        Quaternion targetYAdjustment = Quaternion.AngleAxis(intensity * mouseY, Vector3.right);
        Quaternion targetRotation = originRotation * targetXAdjustment * targetYAdjustment;

        // Rsotate towards target
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }
}
