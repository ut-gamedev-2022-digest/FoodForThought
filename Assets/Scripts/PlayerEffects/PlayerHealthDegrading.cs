using UnityEngine;

public class PlayerHealthDegrading : MonoBehaviour
{
    public float damagePerSecond = 1f;

    private bool _isPlayerInZone;

    private void Update()
    {
        if (!_isPlayerInZone) return;

        Events.CollisionWithEnemy(damagePerSecond * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        _isPlayerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        _isPlayerInZone = false;
    }
}