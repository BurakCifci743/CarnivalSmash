using System;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public event Action<int> ScoreChanged;

    [Header("References")]
    [SerializeField] private RoundController roundController;

    [Header("Score Settings")]
    [SerializeField] private int scorePerCan = 100;
    [SerializeField] private int remainingAttemptBonus = 250;

    public int CurrentScore { get; private set; }

    private void OnEnable()
    {
        roundController.RoundCompleted += HandleRoundCompleted;
    }

    private void OnDisable()
    {
        roundController.RoundCompleted -= HandleRoundCompleted;
    }

    public int CalculateBaseScore(int knockedCount)
    {
        return knockedCount * scorePerCan;
    }

    public int CalculateFinalScore(int knockedCount, int attemptsUsed, int maxAttempts)
    {
        int baseScore = CalculateBaseScore(knockedCount);
        int remainingAttempts = Mathf.Max(0, maxAttempts - attemptsUsed);

        return baseScore + remainingAttempts * remainingAttemptBonus;
    }

    private void HandleRoundCompleted(int knockedCount, int totalCount)
    {
        CurrentScore = CalculateBaseScore(knockedCount);
        ScoreChanged?.Invoke(CurrentScore);
    }
}