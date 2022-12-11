using System;

public static class Events
{
    public static event Action<LoseReason> OnLost;
    public static void Lost(LoseReason loseReason) => OnLost?.Invoke(loseReason);

    public static event Action OnRestartGame;
    public static void RestartGame() => OnRestartGame?.Invoke();

    public static event Action OnReachFinish;
    public static void ReachFinish() => OnReachFinish?.Invoke();

    public static event Action OnStartGame;
    public static void StartGame() => OnStartGame?.Invoke();

    public static event Action OnPauseGame;
    public static void PauseGame() => OnPauseGame?.Invoke();

    public static event Action OnResumeGame;
    public static void ResumeGame() => OnResumeGame?.Invoke();

    public static event Action OnEndGame;
    public static void EndGame() => OnEndGame?.Invoke();

    public static event Action<float> OnShowTime;
    public static void ShowTime(float time) => OnShowTime?.Invoke(time);

    public static event Action<float> OnCollisionWithEnemy;
    public static void CollisionWithEnemy(float damage) => OnCollisionWithEnemy?.Invoke(damage);

    public static event Func<float> OnGetTime;
    public static float GetTime() => (float)(OnGetTime?.Invoke());
    
    public static event Action OnEducationalWindowOpen;
    public static void EducationalWindowOpen() => OnEducationalWindowOpen?.Invoke();
    
    public static event Action OnEducationalWindowClose;
    public static void EducationalWindowClose() => OnEducationalWindowClose?.Invoke();
}