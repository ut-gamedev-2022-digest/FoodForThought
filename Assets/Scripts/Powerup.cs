using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupEffect powerupEffect;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (audioSource != null) audioSource.Play();
        Destroy(gameObject);
        powerupEffect.Apply(other.gameObject);
    }
}