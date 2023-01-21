using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : MonoBehaviour
{
    public AudioSource audioSource;
    public bool Attached;
    public float Damage;
    public float TimeBetweenDamage;

    private Rigidbody rigidbody;
    private Collider collider;
    private float nextDamageTime;
    private RandomMovement randomMovement;

    private void Awake()
    {
        Attached = false;
        nextDamageTime = 0.0f;
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        randomMovement = GetComponent<RandomMovement>();
    }

    private void Update()
    {
        if (Attached && Time.time > nextDamageTime)
        {
            DoDamage();
            nextDamageTime = Time.time + TimeBetweenDamage;
        }
    }

    private void DoDamage()
    {
        Events.CollisionWithEnemy(Damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !Attached)
        {
            Attached = true;
            audioSource.Play();
            DoDamage();
            transform.localScale = transform.localScale * 0.4f;
            transform.SetParent(other.gameObject.transform);
            rigidbody.detectCollisions = false;
            if (randomMovement != null)
            {
                randomMovement.Active = false;
            }
        }
    }
}
