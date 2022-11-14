using System;
using UnityEngine;

public class Villi : MonoBehaviour
{
    public float damage = 10f;
    public float cooldown = 2f;
    
    private float _lastAttackTime;

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();
        if (health == null) return;
        if (Time.time - _lastAttackTime < cooldown) return;
        _lastAttackTime = Time.time;
        Events.CollisionWithEnemy(damage);
    }
}
    