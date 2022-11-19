using UnityEngine;

public class PlayerHealthDegrading : MonoBehaviour
{
    public float damagePerSecond = 1f;

    private bool _isPlayerInZone = false;

    private void Update()
    {
        if (!_isPlayerInZone) return;

        Events.CollisionWithEnemy(damagePerSecond * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var keyboardControl = other.GetComponent<PlayerKeyboardControl>();
        if (keyboardControl != null)
        {
            _isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var keyboardControl = other.GetComponent<PlayerKeyboardControl>();
        if (keyboardControl != null)
        {
            _isPlayerInZone = false;
        }
    }
}