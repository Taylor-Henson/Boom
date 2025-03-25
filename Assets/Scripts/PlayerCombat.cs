using UnityEngine;
using TMPro;

public class PlayerCombat : MonoBehaviour
{
    public float health;
    public float maxHealth = 100;
    public TextMeshProUGUI healthtext;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // UI
        healthtext.text = health + "/" + maxHealth;
    }

    // Takes in the amount of damage that is taken
    public void TakeDamage(float damage)
    {
        // Takes that damage away from the health
        health -= damage;
    }
}
