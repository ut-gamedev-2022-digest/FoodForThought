using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public float CooldownTime = 1f;
    public float Damage = 10f;
    private float nextDamageTime = 0f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Oj");
            Events.CollisionWithEnemy(Damage);
            nextDamageTime = Time.time + CooldownTime;
        }
    }


}
