using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StationaryEnemy : MonoBehaviour
{
    public LayerMask playerMask;
    public LayerMask groundMask;

    public bool inSight;
    public bool check;

    Rig rig;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = gameObject.GetComponentInChildren<Rig>();
        print(rig.weight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // Checks if player is caught in trigger
        if (other.gameObject.name == "PlayerHolder")
        {
            // Is in range
            inSight = true;

            // Activates rig
            rig.weight = 1;
            print(rig.weight);
        }
        else
        {
            // Is not in range
            inSight = false;

            // Deactivates rig
            rig.weight = 0;
            print(rig.weight);
        }

        // If the player is in sight and a check hasnt been done
        if (inSight && !check)
        {
            // Makes the enemy check if it has a line of sight
            LineOfSight(other.transform.position);

            // Waits some time before doing the method again
            Invoke("ResetCheck", 2);
            check = true;
        }
    }

    void ResetCheck()
    {
        check = false;
    }

    void LineOfSight(Vector3 playerPosition)
    {
        // Raycast variables
        Vector3 position = transform.position;
        Vector3 direction = playerPosition - transform.position;
        float distance = 20f;

        // Hits
        RaycastHit playerHit;
        RaycastHit groundHit;

        // Fires raycast at the player
        bool ground = Physics.Raycast(position, direction, out playerHit, distance, playerMask);
        bool player = Physics.Raycast(position, direction, out groundHit, distance, groundMask);
        Debug.DrawRay(position, direction, Color.green);

        // Checks if player is closer than cover
        if (playerHit.distance < groundHit.distance)
        {
            print("spotted");
        }
    }
}
