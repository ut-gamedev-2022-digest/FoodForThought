using System;
using UnityEngine;

public class Health : MonoBehaviour
{
<<<<<<< Updated upstream
    public float health = 75f;

    private void Awake()
    {
        Events.OnCollisionWithEnemy += OnCollisionWithEnemy;
=======

    public float maxHealth = 100f;
    public float currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = 75f;
        healthBar.SetHealth(currentHealth);
>>>>>>> Stashed changes
    }

    private void OnDestroy()
    {
        Events.OnCollisionWithEnemy -= OnCollisionWithEnemy;
    }

    private void OnCollisionWithEnemy(float damage)
    {
        Debug.Log("Collision with enemy");
        health -= damage;
        Debug.Log("Health: " + health);
        if (health <= 0)
        {
            // TODO: Events.OnPlayerDeath();
        }
    }
}