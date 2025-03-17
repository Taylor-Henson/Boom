using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    // References
    public Transform cameraPosition;

    void Update()
    {
        //Sets cameras position to stay near player
        transform.position = cameraPosition.position;
    }
}
