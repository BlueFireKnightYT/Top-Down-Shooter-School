using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // max health
    public int maxHealth = 50;
    // Current health
    public int currentHealth;

    void Start()
    {
        // Set current health
        currentHealth = maxHealth;
    }

    // func for damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // aantal damage
        Debug.Log("Enemy Health: " + currentHealth);

        // Check if enemy has no health left
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died!");
        // Destroy enemy object
        Destroy(gameObject);
    }
}