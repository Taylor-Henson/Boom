using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    #region Variables and References

    [Header("Health")]
    public TextMeshProUGUI healthtext;
    public int health;
    public int maxHealth = 100;

    [Header("Death")]
    public GameObject deathMenu;

    #endregion

    #region Start and Update

    void Start()
    {
        // Sets health to the maximum health at the beginning of the game
        health = maxHealth;

        GameManager.instance.deadOrGameOver = false;
    }

    void Update()
    {
        // UI
        healthtext.text = health + "/" + maxHealth;
    }

    #endregion

    #region Taking Damage and Dying

    // Takes in the amount of damage that is taken
    public void TakeDamage(int damage)
    {
        // Takes that damage away from the health
        health -= damage;

        // Checks if the player should be dead whenever they take damage
        if (health <= 0)
        {
            // Makes player die if so
            Die();
        }
        else if (health >= maxHealth)
        {
            // Caps health at 100
            health = maxHealth = 100;
        }

        
    }

    public void Die()
    {
        // Sets death to true
        GameManager.instance.deadOrGameOver = true;

        // Unlocks cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Turns on death screen
        deathMenu.SetActive(true);
    }

    #endregion
}
