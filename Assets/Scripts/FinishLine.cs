using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        Events.ReachFinish();
        Events.EndGame();

        UnlockNextLevel();
    }

    private static void UnlockNextLevel()
    {
        var nextLevel = Game.UnlockNextLevelForCurrentUser();
        Debug.Log("Level unlocked: " + nextLevel);
    }
}
