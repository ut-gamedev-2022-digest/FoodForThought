using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : MonoBehaviour
{
    public bool Attached;
    public float Damage;
    public float TimeBetweenDamage;

    private Rigidbody rigidbody;
    private float nextDamageTime;
    private RandomMovement randomMovement;

    private void Awake()
    {
        Attached = false;
        nextDamageTime = 0.0f;
        rigidbody = GetComponent<Rigidbody>();
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
        if (LoadLevel.Instance.BacteriaAudioSource != null && !LoadLevel.Instance.BacteriaAudioSource.isPlaying)
        {
            LoadLevel.Instance.BacteriaAudioSource.Play();
        }
        Events.CollisionWithEnemy(Damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !Attached)
        {
            Attached = true;
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
