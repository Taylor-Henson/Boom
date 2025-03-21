using UnityEngine;

public class Particles : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Destroy", 3);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
