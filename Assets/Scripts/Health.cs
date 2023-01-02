using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 100f;
    public HealthBar healthBar;
    
    private bool _isDead = false;
    public bool _isShielded = false;

    private float shieldCounter;

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

    private void Update()
    {
        if (_isShielded)
        {
            shieldCounter += Time.deltaTime;
        }

        if (shieldCounter > 5f)
        {
            shieldCounter = 0;
            _isShielded = false;
        }
    }

    private void OnCollisionWithEnemy(float damage)
    {
        if (!_isShielded)
        {
            health -= damage;
            healthBar.SetHealth(health);
        }

        if (!(health <= 0) || _isDead) return;
        Debug.Log($"Deadly collision with enemy, damage: {damage}, health: {health}");
        
        _isDead = true;
        Events.Lost(LoseReason.HealthZero);
        Events.EndGame();
    }
}