using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables and References

    [Header("Firing")]
    public Rigidbody rb;
    public float force;

    [Header("Hit")]
    PlayerCombat playerCombatScript;
    int bulletDamage = 8;

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //References
        playerCombatScript = GameObject.Find("PlayerHolder").GetComponent<PlayerCombat>();
        
        // Applies original force
        rb.AddForce(transform.forward * force, ForceMode.Force);

        // Makes bullet destroy itself after 8 seconds
        Invoke("DeleteBullet", 8);
    }

    #region Hit

    private void OnTriggerEnter(Collider other)
    {
        // Deletes bullet
        DeleteBullet();

        // If player is hit
        if (other.tag == "Player")
        {
            // Makes player take damage
            playerCombatScript.TakeDamage(bulletDamage);

            // Makes random sound and plays it
            int sound = Random.Range(7, 9);
            AudioManager.instance.PlaySFX(sound);
        }
    }

    #endregion

    void DeleteBullet()
    {
        Destroy(gameObject);
    }
}
