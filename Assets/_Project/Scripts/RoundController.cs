using System;
using System.Collections;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public event Action<int, int> RoundCompleted;

    [Header("References")]
    [SerializeField] private BallThrower ballThrower;
    [SerializeField] private CanGroup canGroup;

    [Header("Timing")]
    [SerializeField] private float resultDelay = 3f;

    public RoundState CurrentState { get; private set; }
    public bool CanThrow => CurrentState == RoundState.Ready;

    private void Awake()
    {
        CurrentState = RoundState.Ready;
    }

    private void OnEnable()
    {
        ballThrower.BallThrown += HandleBallThrown;
    }

    private void OnDisable()
    {
        ballThrower.BallThrown -= HandleBallThrown;
    }

    private void HandleBallThrown()
    {
        CurrentState = RoundState.BallThrown;
        StartCoroutine(EvaluateRoundAfterDelay());
    }

    private IEnumerator EvaluateRoundAfterDelay()
    {
        CurrentState = RoundState.Evaluating;

        yield return new WaitForSeconds(resultDelay);

        int knockedCount = canGroup.GetKnockedDownCount();
        int totalCount = canGroup.GetTotalCanCount();

        CurrentState = RoundState.Completed;

        Debug.Log($"Round completed: {knockedCount} / {totalCount} cans knocked down.");

        RoundCompleted?.Invoke(knockedCount, totalCount);
    }
}