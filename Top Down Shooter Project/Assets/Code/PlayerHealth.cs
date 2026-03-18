using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Slider hpBar;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHPBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        //reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateHPBar()
    {
        hpBar.value = currentHealth;
    }
}