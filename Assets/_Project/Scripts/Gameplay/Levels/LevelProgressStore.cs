using UnityEngine;

public static class LevelProgressStore
{
    private const string BestScoreKeyPrefix = "BestScore_Level_";
    private const string CompletedKeyPrefix = "Completed_Level_";
    private const string HighestUnlockedLevelKey = "HighestUnlockedLevelIndex";

    public static bool IsLevelUnlocked(int levelIndex)
    {
        if (levelIndex <= 0) return true;

        int highestUnlockedLevelIndex = PlayerPrefs.GetInt(HighestUnlockedLevelKey, 0);
        return levelIndex <= highestUnlockedLevelIndex;
    }

    public static int GetBestScore(int levelIndex)
    {
        return PlayerPrefs.GetInt(GetBestScoreKey(levelIndex), 0);
    }

    public static bool IsLevelCompleted(int levelIndex)
    {
        return PlayerPrefs.GetInt(GetCompletedKey(levelIndex), 0) == 1;
    }

    public static void ClearAllProgress()
    {
        PlayerPrefs.DeleteKey(HighestUnlockedLevelKey);

        for (int i = 0; i < 100; i++)
        {
            PlayerPrefs.DeleteKey(GetBestScoreKey(i));
            PlayerPrefs.DeleteKey(GetCompletedKey(i));
        }

        PlayerPrefs.Save();
    }

    private static string GetBestScoreKey(int levelIndex)
    {
        return $"{BestScoreKeyPrefix}{levelIndex}";
    }

    private static string GetCompletedKey(int levelIndex)
    {
        return $"{CompletedKeyPrefix}{levelIndex}";
    }
}