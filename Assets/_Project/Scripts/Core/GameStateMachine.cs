using System;
using System.Collections;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public event Action<GameState> StateChanged;
    public event Action<int, int> AttemptChanged;
    public event Action<int, int> GameEnded;

    [Header("References")]
    [SerializeField] private BallThrower ballThrower;
    [SerializeField] private RoundController roundController;

    [Header("Game Settings")]
    [SerializeField] private int maxAttempts = 3;
    [SerializeField] private float nextAttemptDelay = 0.5f;

    public GameState CurrentState { get; private set; }
    public int CurrentAttempt { get; private set; }
    public int MaxAttempts => maxAttempts;
    public bool CanAcceptThrow => CurrentState == GameState.Playing;

    private void OnEnable()
    {
        ballThrower.BallThrown += HandleBallThrown;
        roundController.RoundCompleted += HandleRoundCompleted;
    }

    private void OnDisable()
    {
        ballThrower.BallThrown -= HandleBallThrown;
        roundController.RoundCompleted -= HandleRoundCompleted;
    }

    private void Start()
    {
        CurrentAttempt = 1;
        AttemptChanged?.Invoke(CurrentAttempt, maxAttempts);

        SetState(GameState.Playing);
    }

    public void SetMaxAttempts(int value)
    {
        maxAttempts = Mathf.Max(1, value);
    }
    private void HandleBallThrown()
    {
        SetState(GameState.Evaluating);
    }

    private void HandleRoundCompleted(int knockedCount, int totalCount)
    {
        bool allCansKnocked = knockedCount >= totalCount;
        bool noAttemptsLeft = CurrentAttempt >= maxAttempts;

        if (allCansKnocked || noAttemptsLeft)
        {
            SetState(GameState.Result);
            GameEnded?.Invoke(knockedCount, totalCount);
            return;
        }

        CurrentAttempt++;
        AttemptChanged?.Invoke(CurrentAttempt, maxAttempts);

        StartCoroutine(PrepareNextAttempt());
    }

    private IEnumerator PrepareNextAttempt()
    {
        yield return new WaitForSeconds(nextAttemptDelay);

        ballThrower.ResetBall();
        SetState(GameState.Playing);
    }
    private void SetState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        StateChanged?.Invoke(CurrentState);
    }
}