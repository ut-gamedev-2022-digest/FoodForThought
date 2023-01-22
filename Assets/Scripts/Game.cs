using UnityEngine;

public enum LoseReason
{
    TimeRunOut,
    HealthZero
}

public static class Game
{
    public static bool IsPaused { get; set; }
    
    public static string UserDependentPlayerPrefsKey(string key)
    {
        var name = PlayerPrefs.GetString(PlayerPrefsConstants.CurrentUserName);
        return $"{key}_{name}";
    }

    public static int GetLevelsUnlockedForCurrentUser()
    {
        var key = UserDependentPlayerPrefsKey(PlayerPrefsConstants.LevelsUnlocked);
        return PlayerPrefs.GetInt(key, 1);
    }
    
    public static int UnlockNextLevelForCurrentUser()
    {
        var key = UserDependentPlayerPrefsKey(PlayerPrefsConstants.LevelsUnlocked);
        var levelsUnlocked = PlayerPrefs.GetInt(key, 1);
        var value = levelsUnlocked + 1;
        PlayerPrefs.SetInt(key, value);
        return value;
    }
}