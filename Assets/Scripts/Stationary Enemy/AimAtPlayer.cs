using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class AimAtPlayer : MonoBehaviour
{
    [Header("PlayerDetection")]
    public bool playerSeen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region PlayerDetection

    // Checks if player is in trigger and makes the enemy see them if true

    void OnTriggerEnter(Collider other)
    {
        playerSeen = true;
    }

    void OnTriggerExit(Collider other)
    {
        playerSeen = false;
    }

    #endregion
}
