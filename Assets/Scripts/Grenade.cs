using UnityEngine;

public class Grenade : MonoBehaviour
{
    Transform camera;
    public Rigidbody rigidbody;
    Vector3 direction;
    public float rotationSpeed;
    public ParticleSystem explosion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GameObject.Find("Camera").GetComponent<Transform>();

        float offset = 0.5f;
        float force = 10;
        direction = camera.transform.forward + new Vector3(0, offset, 0);

        rigidbody.AddForce(direction * force, ForceMode.Impulse);

        float x = Random.Range(-90, 90) * rotationSpeed * Time.deltaTime;
        float y = Random.Range(-90, 90) * rotationSpeed * Time.deltaTime;
        float z = Random.Range(-90, 90) * rotationSpeed * Time.deltaTime;

        transform.Rotate(x, y, z);

        Invoke("Explode", 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        explosion.transform.position = transform.position;
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
