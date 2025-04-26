using UnityEngine;

public class CameraPosSFX : MonoBehaviour
{

    public void MoveSFX()
    {
        // Checks if grounded
        if (GameObject.Find("PlayerHolder").GetComponent<Movement>().grounded)
        {
            AudioManager.instance.PlaySFX(3);
        }
    }
}
