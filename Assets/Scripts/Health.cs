using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private HealthBar healthBar;
    private int currentHealth;


    private void OnEnable()
    {
        // Set health to max
        currentHealth = maxHealth;

        // Set health bar values
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);

    }

    public void TakeDamage(int damage)
    {
        // If object is alive
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

        } else
        {
            // Todo use pool system
            gameObject.SetActive(false);
        }
    }
}
