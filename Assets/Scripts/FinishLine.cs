using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Timer>() != null)
        {
            Events.ReachFinish();
            Events.EndGame();
        }
    }
}
