using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 75f;

    private void Awake()
    {
        Events.OnCollisionWithEnemy += OnCollisionWithEnemy;
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