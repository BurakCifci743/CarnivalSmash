using System;
using UnityEngine;

public class LocalLeaderboardController : MonoBehaviour
{
    public event Action<GameResult, int, bool> LeaderboardUpdated;

    private const string BestScoreKey = "BestScore";

    [Header("References")]
    [SerializeField] private GameResultController gameResultController;

    public int BestScore => PlayerPrefs.GetInt(BestScoreKey, 0);

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
        bool isNewBest = result.FinalScore > BestScore;

        if (isNewBest)
        {
            PlayerPrefs.SetInt(BestScoreKey, result.FinalScore);
            PlayerPrefs.Save();
        }

        LeaderboardUpdated?.Invoke(result, BestScore, isNewBest);
    }

    public void ClearBestScore()
    {
        PlayerPrefs.DeleteKey(BestScoreKey);
        PlayerPrefs.Save();
    }
}