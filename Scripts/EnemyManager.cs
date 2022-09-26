using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /* Customizable */
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private HealthBar healthBar;

    /* Variables */
    private float currentHealth;

    private void Start()
    {
        SetEnemyHealth();
    }

    private void Update()
    {
        healthBar.SetActive(currentHealth, maxHealth);
    }

    private void SetEnemyHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
