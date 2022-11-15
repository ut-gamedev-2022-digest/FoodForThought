using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : MonoBehaviour
{
    public AudioSource audioSource;
    public bool Attached;
    public int Damage;
    public float TimeBetweenDamage;

    private float nextDamageTime;

    private void Awake()
    {
        Attached = false;
        nextDamageTime = 0;
    }

    private void Update()
    {
        if (Time.time > nextDamageTime)
        {
            DoDamage();
            nextDamageTime = Time.time + TimeBetweenDamage;
        }
    }

    private void DoDamage()
    {
        Events.CollisionWithEnemy(Damage);
    }
}
