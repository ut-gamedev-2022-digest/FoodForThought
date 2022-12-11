using UnityEngine;

public class PlayerSlowDown : MonoBehaviour
{
    private const float SlowDownFactor = 0.5f;
    private const float NormalSpeedUp = 0.5f;
    private const float LowGravity = 1f;
    private float? _originalGravity;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log($"Player entered slow down zone. Setting gravity to {LowGravity}");
        
        _originalGravity = Events.GravityChange(LowGravity);
        Events.SpeedChange(SlowDownFactor);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        var newGravity = _originalGravity ?? -10f;
        Debug.Log($"Player exited slow down zone. Setting gravity to {newGravity}");
        
        Events.GravityChange(newGravity);
        Events.SpeedChange(NormalSpeedUp);
    }
}