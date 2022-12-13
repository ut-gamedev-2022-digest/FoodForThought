using UnityEngine;

public class PlayerHealthDamage : MonoBehaviour
{
    public float damage = 10f;
    public float cooldown = 2f;
    
    private float _lastAttackTime;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time - _lastAttackTime < cooldown) return;
        
        _lastAttackTime = Time.time;
        
        Events.CollisionWithEnemy(damage);
    }
}
    