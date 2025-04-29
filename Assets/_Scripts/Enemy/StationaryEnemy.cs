using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StationaryEnemy : MonoBehaviour
{
    #region Variables and References

    [Header("Field Of View")]
    Rig rig;
    public bool inSight;
    public bool check;
    Transform spine;
    Transform upperChest;
    Transform rightShoulder;
    Transform child;
    Transform ikPoint;

    [Header("Line Of Sight")]
    GameObject player;
    GameObject cameraPos;
    public GameObject rayOrigin;
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
        // References
        rig = gameObject.GetComponentInChildren<Rig>();
        enemyCombatScript = gameObject.GetComponent<EnemyCombat>();
        spine = transform.Find("Rig/Spine");
        upperChest = transform.Find("Rig/UpperChest");
        rightShoulder = transform.Find("Rig/RightShoulder");

        // Sets health to 100
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Calling methods
        PlayerInSight();
    }

    private void LateUpdate()
    {
        RigBuilder rigBuilder = GetComponent<RigBuilder>();
        if (rigBuilder != null)
        {
            rigBuilder.Build(); 
        }
    }

    #endregion

    #region FieldOfView
    // Checks if the player is in or out of the collider

    private void OnTriggerEnter(Collider other)
    {
        // Checks if player is caught in trigger
        if (other.gameObject.name == "PlayerHolder" && !enemyCombatScript.dead)
        {
            // Is in range
            inSight = true;
            rig.weight = 1;
            
            // Stores player
            player = other.gameObject;

            // Stores player IK point
            child = player.transform.GetChild(0);
            ikPoint = child.transform.GetChild(3);

            // Makes an array to store transforms of objects, as well as the weighting of that object
            WeightedTransformArray sources = new WeightedTransformArray();
            sources.Add(new WeightedTransform(ikPoint.transform, 1));

            // Gets the MAC component and its data as variables, sets the data to the array object, then assigns that data to the component
            // This is done as changing the data alone without reassigning will not change the constraint itself
            var spineConstraint = spine.GetComponent<MultiAimConstraint>();
            var spineData = spineConstraint.data;
            spineData.sourceObjects = sources;
            spineConstraint.data = spineData;

            var upperChestConstraint = upperChest.GetComponent<MultiAimConstraint>();
            var upperChestData = upperChestConstraint.data;
            upperChestData.sourceObjects = sources;
            upperChestConstraint.data = upperChestData;

            var rightShoulderConstraint = rightShoulder.GetComponent<MultiAimConstraint>();
            var rightShoulderData = rightShoulderConstraint.data;
            rightShoulderData.sourceObjects = sources;
            rightShoulderConstraint.data = rightShoulderData;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PlayerHolder" && !enemyCombatScript.dead)
        {
            // Is not in range
            inSight = false;
            rig.weight = 0;
        }
    }

    void PlayerInSight()
    {
        // If the player is in sight and a check hasnt been done
        if (inSight && !check)
        {
            // Makes the enemy check if it has a line of sight
            LineOfSight(player.transform.position);

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
        cameraPos = GameObject.Find("CameraPos");

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
        if (playerHit.distance < groundHit.distance && !enemyCombatScript.dead)
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
