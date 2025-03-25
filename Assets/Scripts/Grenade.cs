using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using TMPro;

public class Grenade : MonoBehaviour
{
    [Header("Throwing and Rotation")]
    Vector3 direction;
    public Rigidbody rigidbody;
    public float rotationSpeed;

    [Header("Boosting")]
    Transform camera;
    Transform playerPoint;
    Rigidbody playerRb;

    [Header("Checksphere")]
    public LayerMask player;
    public LayerMask enemy;
    public float radius;

    [Header("Explosion")]
    public ParticleSystem explosion;
    public PlayerCombat playerCombatScript;

    #region Start and Update

    void Start()
    {
        // References
        camera = GameObject.Find("Camera").GetComponent<Transform>();
        playerPoint = GameObject.Find("GrenadeCheckPoint").GetComponent<Transform>();
        playerRb = GameObject.Find("PlayerHolder").GetComponent<Rigidbody>();
        playerCombatScript = GameObject.Find("PlayerHolder").GetComponent<PlayerCombat>();

        // Calling methods
        Spawn();
    }

    #endregion

    #region Spawn

    void Spawn()
    {
        // Creates a direction based off of the cameras forward transform with a small boost up to simulate throwing
        float offset = 0.5f;
        float force = 10;
        direction = camera.transform.forward + new Vector3(0, offset, 0);

        // Applies that direction in a force
        rigidbody.AddForce(direction * force, ForceMode.Impulse);

        // Randomizes how much rotation will happen on each axis while being thrown
        float x = Random.Range(-90, 90) * rotationSpeed * Time.deltaTime;
        float y = Random.Range(-90, 90) * rotationSpeed * Time.deltaTime;
        float z = Random.Range(-90, 90) * rotationSpeed * Time.deltaTime;

        // Applies the rotations created across time as a Torque
        rigidbody.AddTorque(x, y, z);

        // Sets timer before explosion
        Invoke("Explode", 3);
        Invoke("Boost", 3);

        // Plays throwing sound effect
        AudioManager.instance.PlaySFX(5);
    }

    #endregion

    #region Explosion

    void Explode()
    {
        // Destroys itself
        Destroy(gameObject);

        // Instantiates explosion particle effect
        Instantiate(explosion, transform.position, Quaternion.identity);

        // Plays explosion sound effect
        AudioManager.instance.PlaySFX(4);

        // Checks if an enemy is caught in the explosion
        if (Physics.CheckSphere(transform.position, radius, enemy))
        {
            // Make enemy take damage
        }
    }

    void Boost()
    {
        // Finds player's and its own position
        Vector3 playerPosition = playerPoint.transform.position;
        Vector3 position = transform.position;

        // Uses them to calculate the direction between the two gameObjects and adds less of a weighting to the Y
        float xDirection = playerPosition.x - position.x;
        float yDirection = (playerPosition.y - position.y) / 2 + 0.75f;
        float zDirection = playerPosition.z - position.z;

        // Creates a direction to send the player using the directions created
        Vector3 direction = new Vector3(xDirection, yDirection, zDirection);

        // Checks if player is caught in explosion
        if (Physics.CheckSphere(transform.position, radius, player))
        {
            // Applies force in the direction calculated
            playerRb.AddForce(direction * 20, ForceMode.Impulse);

            // Makes player take damage
            playerCombatScript.TakeDamage(10);
        }
    }

    #endregion

}
