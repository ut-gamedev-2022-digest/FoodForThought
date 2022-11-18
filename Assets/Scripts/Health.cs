using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public float health = 75f;
    public HealthBar healthBar;

    private void Awake()
    {
        Events.OnCollisionWithEnemy += OnCollisionWithEnemy;
    }

    public float maxHealth = 100f;
    public Slider slider;

    void Start()
    {
        health = 75f;
        healthBar.SetHealth(health);
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