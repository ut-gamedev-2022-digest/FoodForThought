using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void Awake()
    {
        Events.OnReachFinish += OnReachFinish;
    }

    private void OnDestroy()
    {
        Events.OnReachFinish -= OnReachFinish;
    }

    private void OnReachFinish()
    {
        UnlockNextLevel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Events.ReachFinish();
        Events.EndGame();
    }

    private static void UnlockNextLevel()
    {
        var nextLevel = Game.UnlockNextLevelForCurrentUser();
        Debug.Log("Level unlocked: " + nextLevel);
    }
}