using System;

public static class Events
{

    public static event Action<LoseReason> OnLost;

    public static void Lost(LoseReason loseReason) => OnLost?.Invoke(loseReason);

    public static event Action OnRestartGame;
    public static void RestartGame() => OnRestartGame?.Invoke();

    public static event Action OnReachFinish;

    public static void ReachFinish() => OnReachFinish?.Invoke();

    public static event Action<float> OnShowTime;

    public static void ShowTime(float time) => OnShowTime?.Invoke(time);

    public static event Action<float> OnCollisionWithEnemy;
    public static void CollisionWithEnemy(float damage) => OnCollisionWithEnemy?.Invoke(damage);
}