using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StationaryEnemy : MonoBehaviour
{
    #region Variables and References

    [Header("Field Of View")]
    Rig rig;
    public bool inSight;
    public bool check;

    [Header("Line Of Sight")]
    public GameObject rayOrigin;
    public GameObject cameraPos;
    public LayerMask playerMask;
    public LayerMask groundMask;

    [Header("Shooting")]
    public Animator anim;
    public Transform bulletSpawnPos;
    public GameObject enemyGun;
    public GameObject bullet;
    public GameObject muzzleFlashPoint;
    public ParticleSystem muzzleFlash;

    [Header("Health and Dying")]
    EnemyCombat enemyCombatScript;
    int maxHealth = 100;
    public int health;

    #endregion

    #region Start and Update

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim.applyRootMotion = false;
        // References
        rig = gameObject.GetComponentInChildren<Rig>();
        enemyCombatScript = gameObject.GetComponent<EnemyCombat>();

        // Sets health to 100
        health = maxHealth;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region FieldOfView

    private void OnTriggerStay(Collider other)
    {
        // Checks if player is caught in trigger
        if (other.gameObject.name == "PlayerHolder" && !enemyCombatScript.dead)
        {
            // Is in range
            inSight = true;

            // Activates rig;
            rig.enabled = true;
            print("enable rig");
        }
        else
        {
            print("disable rig");
            // Is not in range
            inSight = false;

            // Deactivates rig
            rig.enabled = false;
        }

        // If the player is in sight and a check hasnt been done
        if (inSight && !check)
        {
            // Makes the enemy check if it has a line of sight
            LineOfSight(other.transform.position);

            // Waits some time before doing the method again
            Invoke("ResetCheck", 1f);
            check = true;
        }
    }

    void ResetCheck()
    {
        check = false;
    }

    #endregion

    #region LineOfSight

    void LineOfSight(Vector3 playerPosition)
    {
        // Raycast variables
        Vector3 position = rayOrigin.transform.position;
        Vector3 direction = cameraPos.transform.position - position;
        float distance = 20f;

        // Hits
        RaycastHit playerHit;
        RaycastHit groundHit;

        // Fires raycast at the player
        bool ground = Physics.Raycast(position, direction, out playerHit, distance, playerMask);
        bool player = Physics.Raycast(position, direction, out groundHit, distance, groundMask);
        // Debug.DrawRay(position, direction, Color.green);

        // Checks if player is closer than cover
        if (playerHit.distance < groundHit.distance)
        {
            // Calls for method to fire at player
            Shoot();
        }
    }

    #endregion

    #region Shooting

    void Shoot()
    {
        // Play sound
        AudioManager.instance.PlaySFX(6);

        // Play animation
        anim.SetTrigger("Shoot");

        // Play muzzle flash
        ParticleSystem flash = Instantiate(muzzleFlash, muzzleFlashPoint.transform.position, enemyGun.transform.rotation * Quaternion.Euler(0, -90, 0));
        flash.Play();

        // Instantiate bullet
        Instantiate(bullet, bulletSpawnPos.transform.position, enemyGun.transform.rotation);
    }

    #endregion
}
