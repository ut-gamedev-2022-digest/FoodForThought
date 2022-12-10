using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : MonoBehaviour
{
    public AudioSource audioSource;
    public bool Attached;
    public float Damage;
    public float TimeBetweenDamage;

    private float nextDamageTime;

    private void Awake()
    {
        Attached = false;
        nextDamageTime = 0;
        CheckLaunchMode();
    }

    private void CheckLaunchMode()
    {
        int mode = PlayerPrefs.GetInt("LaunchMode", 1);
        if (mode == 0)
        {
            gameObject.SetActive(false);
        }
        else if (mode == 1)
        {
            gameObject.SetActive(true);
        }
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
        var timer = other.gameObject.GetComponent<Timer>();
        if (timer != null && !Attached)
        {
            Attached = true;
            audioSource.Play();
            var fj = other.gameObject.AddComponent<FixedJoint>();
            fj.connectedBody = gameObject.GetComponent<Rigidbody>();

        }
    }
}
