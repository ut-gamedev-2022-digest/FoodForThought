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
}
