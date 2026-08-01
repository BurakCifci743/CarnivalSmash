using System;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public event Action<GameState> StateChanged;

    [Header("References")]
    [SerializeField] private BallThrower ballThrower;
    [SerializeField] private RoundController roundController;

    public GameState CurrentState { get; private set; }
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
        SetState(GameState.Playing);
    }

    private void HandleBallThrown()
    {
        SetState(GameState.Evaluating);
    }

    private void HandleRoundCompleted(int knockedCount, int totalCount)
    {
        SetState(GameState.Result);
    }

    private void SetState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        StateChanged?.Invoke(CurrentState);
    }
}