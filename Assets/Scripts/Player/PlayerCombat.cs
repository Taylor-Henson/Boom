using UnityEngine;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    #region Variables and References

    [Header("Health")]
    public TextMeshProUGUI healthtext;
    public float maxHealth = 100;
    public float health;

    #endregion

    #region Start and Update

    void Start()
    {
        // Sets health to the maximum health at the beginning of the game
        health = maxHealth;
    }

    void Update()
    {
        // UI
        healthtext.text = health + "/" + maxHealth;
    }

    #endregion

    #region Taking Damage and Dying

    // Takes in the amount of damage that is taken
    public void TakeDamage(float damage)
    {
        // Takes that damage away from the health
        health -= damage;

        // Checks if the player should be dead whenever they take damage
        if (health <= 0)
        {
            // Makes player die if so
            Die();
        }
    }

    void Die()
    {
        print("You died");
    }

    #endregion

}
