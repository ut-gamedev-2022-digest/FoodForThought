using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup : MonoBehaviour
{
    public PowerupEffect powerupEffect;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "Banana")
        {
            audioSource.Play();
            Destroy(gameObject);
            powerupEffect.Apply(collision.gameObject);
        }
    }
}
