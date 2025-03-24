using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Grenade : MonoBehaviour
{
    Transform camera;
    Transform playerPoint;
    public Rigidbody rigidbody;
    Vector3 direction;
    public float rotationSpeed;
    public ParticleSystem explosion;

    public float radius;
    public ColliderHit hit;
    public LayerMask player;
    public LayerMask enemy;
    Rigidbody playerRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GameObject.Find("Camera").GetComponent<Transform>();
        playerPoint = GameObject.Find("GrenadeCheckPoint").GetComponent<Transform>();
        playerRb = GameObject.Find("PlayerHolder").GetComponent<Rigidbody>();

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Spawn

    void Spawn()
    {
        float offset = 0.5f;
        float force = 10;
        direction = camera.transform.forward + new Vector3(0, offset, 0);

        rigidbody.AddForce(direction * force, ForceMode.Impulse);

        float x = Random.Range(-90, 90) * rotationSpeed * Time.deltaTime;
        float y = Random.Range(-90, 90) * rotationSpeed * Time.deltaTime;
        float z = Random.Range(-90, 90) * rotationSpeed * Time.deltaTime;

        transform.Rotate(x, y, z);

        Invoke("Explode", 3);

        AudioManager.instance.PlaySFX(5);
    }

    #endregion

    void Explode()
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
        AudioManager.instance.PlaySFX(4);

        if (Physics.CheckSphere(transform.position, radius, enemy))
        {
            // Make enemy take damage
        }

        Vector3 playerPosition = playerPoint.transform.position;
        Vector3 position = transform.position;
        float xDirection = playerPosition.x - position.x;
        float yDirection = (playerPosition.y - position.y) / 2 + 0.75f;
        float zDirection = playerPosition.z - position.z;

        Vector3 direction = new Vector3(xDirection, yDirection, zDirection);


        if (Physics.CheckSphere(transform.position, radius, player))
        {
            print(direction);
            playerRb.AddForce(direction * 20, ForceMode.Impulse);
        }
    }
}
