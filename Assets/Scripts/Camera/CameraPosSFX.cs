using UnityEngine;

public class CameraPosSFX : MonoBehaviour
{
    public void MoveSFX()
    {
        //print("MoveSFX");
        AudioManager.instance.PlaySFX(3);
    }
}
