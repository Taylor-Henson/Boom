using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnemyCombat : MonoBehaviour
{
    #region Variables and References

    [Header("Health")]
    public float maxHealth = 100;
    public float health;

    [Header("Death")]
    public Rig rig;
    public Animator animator;
    public GameObject shotgun;
    public bool dead = false;

    #endregion

    #region Start and Update

    void Start()
    {
        // References
        rig = transform.Find("Rig Setup").GetComponent<Rig>();

        // Sets health to the maximum health at the beginning of the game
        health = maxHealth;
    }

    #endregion

    #region Taking Damage

    // Takes in the amount of damage that is taken
    public void TakeDamage(float damage)
    {
        // Takes that damage away from the health
        health -= damage;

        // Checks if the enemy should be dead whenever they take damage
        if (health <= 0)
        {
            // Makes enemy die if so
            StartCoroutine(Die());
        }
    }

    #endregion

    #region Dying

    IEnumerator Die()
    {
        if (!dead)
        {
            // Bool
            dead = true;

            // Animations
            animator.SetTrigger("Death");
            animator.SetBool("Dead", true);
        }

        // Disables rig
        rig.weight = 0;

        // Disables shotgun
        if (shotgun != null)
        {
            shotgun.SetActive(false);
        }

        // IEnumerator waiting
        yield return new WaitForSeconds(5);

        if (gameObject != null)
        {
            // Destroys Enemy
            Destroy(gameObject);
        }
    }

    #endregion
}
