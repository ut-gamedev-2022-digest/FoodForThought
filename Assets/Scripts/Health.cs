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

    private void Start()
    {
        Debug.Log("Initial health: " + health);
        healthBar.SetHealth(health);
    }

    private void OnDestroy()
    {
        Events.OnCollisionWithEnemy -= OnCollisionWithEnemy;
    }

    private void OnCollisionWithEnemy(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        Debug.Log($"Collision with enemy, damage: {damage}, health: {health}");
        if (health <= 0)
        {
            // TODO: Events.OnPlayerDeath();
        }
    }
}