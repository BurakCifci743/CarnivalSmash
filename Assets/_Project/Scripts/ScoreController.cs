using System;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public event Action<int> ScoreChanged;

    [Header("References")]
    [SerializeField] private RoundController roundController;

    [Header("Score Settings")]
    [SerializeField] private int scorePerCan = 100;

    public int CurrentScore { get; private set; }

    private void OnEnable()
    {
        roundController.RoundCompleted += HandleRoundCompleted;
    }

    private void OnDisable()
    {
        roundController.RoundCompleted -= HandleRoundCompleted;
    }

    public int CalculateScore(int knockedCount)
    {
        return knockedCount * scorePerCan;
    }

    private void HandleRoundCompleted(int knockedCount, int totalCount)
    {
        CurrentScore = CalculateScore(knockedCount);

        ScoreChanged?.Invoke(CurrentScore);
    }
}