using UnityEngine;

public enum LoseReason
{
    TimeRunOut,
    HealthZero
}

public class Game : MonoBehaviour
{
    public Level Level;
    public static Game Instance;
    private void Awake()
    {
        Instance = this;
    }
}
