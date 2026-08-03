using System;
using UnityEngine;

public class GameResultController : MonoBehaviour
{
    public event Action<GameResult> GameResultReady;

    [Header("References")]
    [SerializeField] private GameStateMachine gameStateMachine;
    [SerializeField] private ScoreController scoreController;

    private void OnEnable()
    {
        gameStateMachine.GameEnded += HandleGameEnded;
    }

    private void OnDisable()
    {
        gameStateMachine.GameEnded -= HandleGameEnded;
    }

    private void HandleGameEnded(int knockedCount, int totalCount)
    {
        int attemptsUsed = gameStateMachine.CurrentAttempt;
        int maxAttempts = gameStateMachine.MaxAttempts;

        int finalScore = scoreController.CalculateFinalScore(
            knockedCount,
            attemptsUsed,
            maxAttempts
        );

        GameResult result = new GameResult(
            knockedCount,
            totalCount,
            attemptsUsed,
            maxAttempts,
            finalScore
        );

        GameResultReady?.Invoke(result);
    }
}