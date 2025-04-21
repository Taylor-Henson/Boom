using UnityEngine;
using TMPro;

public class Shotgun : MonoBehaviour
{
    #region Variables and References

    [Header("Raycast")]
    public Transform cameraPos;
    public LayerMask enemyLayer;
    public RaycastHit rayHit;
    EnemyCombat enemyCombatScript;
    public float spread;

    [Header("Particle Effects")]
    public Transform muzzlePoint;
    public ParticleSystem flash;
    public GameObject hitParticle;

    [Header("Reloading")]
    public Animator animator;
    public TextMeshProUGUI ammoText;
    public bool canShoot;
    public bool reloading;
    public bool firing;
    public float magazineSize = 6;
    public float bulletsLeft;

    [Header("Grenade")]
    public GameObject grenade;
    public Transform grenadeSpawn;
    public TextMeshProUGUI grenadeText;
    public int grenades = 3;
    public bool canUseGrenade;

    [Header("Impulse")]
    public Rigidbody playerRigidbody;

    #endregion

    #region Start and Update

    private void Start()
    {
        // Initially sets the bullets left to the magazine size
        bulletsLeft = magazineSize;
    }

    void Update()
    {
        // Calling methods
        PlayerInput();
        CanPlayerShoot();

        // UI
        ammoText.text = bulletsLeft +  " / " + magazineSize;
        grenadeText.text = "" + grenades;
    }

    #endregion

    #region Input
    void PlayerInput()
    {
        // Input for firing
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            // Makes all shooting effects happen
            Shoot();

            // For loop to shoot 5 times
            for (int i = 0; i < 5; i++)
            {
                Raycasts();
            }
        }

        // Input for reloading
        if (Input.GetKeyDown("r") && !reloading && !firing && bulletsLeft != 6)
        {
            BeginReload();
        }

        // Input for throwing grenade
        if (Input.GetKeyDown("e") && canUseGrenade)
        {
            Instantiate(grenade, grenadeSpawn.transform.position, Quaternion.identity);
            grenades--;
        }

    }

    #endregion

    #region Firing
    void Shoot()
    {
        // All shooting effects

        // Plays firing audio
        AudioManager.instance.PlaySFX(0);

        // Animates the recoil/firing
        animator.SetTrigger("Shoot");

        //Attempts at muzzle flash to be reattempted
        //Instantiate(flash, muzzlePoint.transform.position, Quaternion.Euler(0, -90, 0));

        // Stops player from firing too often
        firing = true;
        Invoke("EndFire", 1);

        // Reduces bullet in magazine by one
        bulletsLeft--;

        // Apply impulse
        Impulse();
    }
    void Raycasts()
    {
        // Randomizes the pellet spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Creates a direction based on the spread
        Vector3 direction = cameraPos.forward + new Vector3(x, y, 0);

        // Raycast from camera position, in the direction calculated, and gets whatever it hits
        if (Physics.Raycast(cameraPos.transform.position, direction, out rayHit, 50f))
        {
            // Instantiates a hit particle effect at the point of whatever is hit, in the forward direction of whatever it has hit
            Instantiate(hitParticle, rayHit.point, Quaternion.LookRotation(rayHit.normal));

            if (rayHit.collider.CompareTag("Enemy"))
            {
                // References component 
                enemyCombatScript = rayHit.collider.GetComponent<EnemyCombat>();

                // Applies damage
                enemyCombatScript.TakeDamage(20);
            }
        }
    }

    #endregion

    #region Checking if player can fire, throw grenades and reloading
    void CanPlayerShoot()
    {
        // Checks if any grenades can be thrown
        if (grenades <= 0)
        {
            canUseGrenade = false;
        }
        else
        {
            canUseGrenade = true;
        }

        // Decided if the player can fire yet
        if (!firing && !reloading && bulletsLeft > 0)
        {
            // Player can shoot
            canShoot = true;
        }
        else
        {
            // Player cannot shoot
            canShoot = false;
        }
    }

    void EndFire()
    {
        // Allows player to fire again
        firing = false;
    }

    void BeginReload()
    {
        // Plays reload animation
        animator.SetTrigger("Reload");

        // Stops player firing and reloading again
        reloading = true;

        //Plays reload sound effect
        AudioManager.instance.PlaySFX(1);
    }

    public void EndReload()
    {
        // Allows player to fire and reload again
        reloading = false;

        // Actually reloads the gun
        bulletsLeft = magazineSize;
    }

    #endregion

    #region Impulse
    void Impulse()
    {
        // Creates a direction in the opposite direction the player is facing
        Vector3 direction = -cameraPos.transform.forward;
        int force = 10;

        // Applies a impulse in that direction
        playerRigidbody.AddForce(direction * force, ForceMode.Impulse);
    }

    #endregion

}
