using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events
{
    public static event Action OnTimeRunOut;
    public static void TimeRunOut() => OnTimeRunOut?.Invoke();

    public static event Action OnRestartGame;
    public static void RestartGame() => OnRestartGame?.Invoke();

    public static event Action OnReachFinish;

    public static void ReachFinish() => OnReachFinish?.Invoke();

    public static event Action<float> OnShowTime;

    public static void ShowTime(float time) => OnShowTime?.Invoke(time);
}
