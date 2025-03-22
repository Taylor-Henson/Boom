using UnityEngine;

public class Particles : MonoBehaviour
{
    void Start()
    {
        // Destroys particle after three seconds
        Invoke("Destroy", 3);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
