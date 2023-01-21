using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public float CooldownTime = 1f;
    public float Damage = 10f;
    public AudioSource AudioSource;

    private float nextDamageTime = 0f;
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time > nextDamageTime)
            {
                Events.CollisionWithEnemy(Damage);
                if (AudioSource != null && !AudioSource.isPlaying)
                {
                    AudioSource.Play();
                }
                nextDamageTime = Time.time + CooldownTime;
            }
        }
        else if (collision.gameObject.CompareTag("Bacteria"))
        {
            Physics.IgnoreCollision(collider, collision.collider);
        }
    }


}
