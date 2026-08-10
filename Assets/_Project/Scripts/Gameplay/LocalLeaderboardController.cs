using System;
using UnityEngine;

public class LocalLeaderboardController : MonoBehaviour
{
    public event Action<GameResult, int, bool> LeaderboardUpdated;

    private const string BestScoreKeyPrefix = "BestScore_Level_";
    private const string CompletedKeyPrefix = "Completed_Level_";
    private const string HighestUnlockedLevelKey = "HighestUnlockedLevelIndex";

    [Header("References")]
    [SerializeField] private GameResultController gameResultController;
    [SerializeField] private LevelController levelController;

    public int CurrentLevelBestScore => GetBestScore(levelController.CurrentLevelIndex);

    private void OnEnable()
    {
        gameResultController.GameResultReady += HandleGameResultReady;
    }

    private void OnDisable()
    {
        gameResultController.GameResultReady -= HandleGameResultReady;
    }

    private void HandleGameResultReady(GameResult result)
    {
        int levelIndex = levelController.CurrentLevelIndex;

        int previousBestScore = GetBestScore(levelIndex);
        bool isNewBest = result.FinalScore > previousBestScore;

        if (isNewBest)
        {
            PlayerPrefs.SetInt(GetBestScoreKey(levelIndex), result.FinalScore);
        }

        if (result.IsPerfect)
        {
            MarkLevelCompleted(levelIndex);
            UnlockNextLevel(levelIndex);
        }

        PlayerPrefs.Save();

        int updatedBestScore = GetBestScore(levelIndex);
        LeaderboardUpdated?.Invoke(result, updatedBestScore, isNewBest);
    }

    public int GetBestScore(int levelIndex)
    {
        return PlayerPrefs.GetInt(GetBestScoreKey(levelIndex), 0);
    }

    public bool IsLevelCompleted(int levelIndex)
    {
        return PlayerPrefs.GetInt(GetCompletedKey(levelIndex), 0) == 1;
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        if (levelIndex <= 0) return true;

        int highestUnlockedLevelIndex = PlayerPrefs.GetInt(HighestUnlockedLevelKey, 0);
        return levelIndex <= highestUnlockedLevelIndex;
    }

    public void ClearBestScore()
    {
        PlayerPrefs.DeleteKey(GetBestScoreKey(levelController.CurrentLevelIndex));
        PlayerPrefs.Save();
    }

    public void ClearAllProgress()
    {
        PlayerPrefs.DeleteKey(HighestUnlockedLevelKey);

        for (int i = 0; i < 100; i++)
        {
            PlayerPrefs.DeleteKey(GetBestScoreKey(i));
            PlayerPrefs.DeleteKey(GetCompletedKey(i));
        }

        PlayerPrefs.Save();
    }

    private void MarkLevelCompleted(int levelIndex)
    {
        PlayerPrefs.SetInt(GetCompletedKey(levelIndex), 1);
    }

    private void UnlockNextLevel(int currentLevelIndex)
    {
        int nextLevelIndex = currentLevelIndex + 1;
        int highestUnlockedLevelIndex = PlayerPrefs.GetInt(HighestUnlockedLevelKey, 0);

        if (nextLevelIndex > highestUnlockedLevelIndex)
        {
            PlayerPrefs.SetInt(HighestUnlockedLevelKey, nextLevelIndex);
        }
    }

    private string GetBestScoreKey(int levelIndex)
    {
        return $"{BestScoreKeyPrefix}{levelIndex}";
    }

    private string GetCompletedKey(int levelIndex)
    {
        return $"{CompletedKeyPrefix}{levelIndex}";
    }
}