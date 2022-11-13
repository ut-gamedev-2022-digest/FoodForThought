using UnityEngine;

public class PlayerSlowDown : MonoBehaviour
{
    private const float SlowDownFactor = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        var keyboardControl = other.GetComponent<PlayerKeyboardControl>();
        if (keyboardControl != null)
        {
            keyboardControl.speed *= SlowDownFactor;
            keyboardControl.DisableGravity();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        var keyboardControl = other.GetComponent<PlayerKeyboardControl>();
        if (keyboardControl != null)
        {
            keyboardControl.speed /= SlowDownFactor;
            keyboardControl.EnableGravity();
        }
    }
}
