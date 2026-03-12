using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Maximum health of the player
    public int maxHealth = 100;
    // Current health of the player
    public int currentHealth;

    void Start()
    {
        // Set current health to maximum at start
        currentHealth = maxHealth;
    }

    // Call this function to deal damage to the player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health by damage amount
        Debug.Log("Player Health: " + currentHealth);

        // Check if player has no health left
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        // Disable player object or handle game over
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}