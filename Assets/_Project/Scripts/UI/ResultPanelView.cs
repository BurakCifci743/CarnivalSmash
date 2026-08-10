using TMPro;
using UnityEngine;

public class ResultPanelView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameStateMachine gameStateMachine;
    [SerializeField] private BallThrower ballThrower;
    [SerializeField] private LocalLeaderboardController localLeaderboardController;
    [SerializeField] private GameObject gameplayHud;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TMP_Text instructionText;
    [SerializeField] private TMP_Text attemptText;
    [SerializeField] private TMP_Text resultTitleText;
    [SerializeField] private TMP_Text resultDetailText;

    [Header("Text")]
    [SerializeField] private string startInstruction = "Tap to shoot";

    private void OnEnable()
    {
        gameStateMachine.AttemptChanged += UpdateAttemptText;
        ballThrower.BallThrown += HideInstruction;
        localLeaderboardController.LeaderboardUpdated += HandleLeaderboardUpdated;
    }

    private void OnDisable()
    {
        gameStateMachine.AttemptChanged -= UpdateAttemptText;
        ballThrower.BallThrown -= HideInstruction;
        localLeaderboardController.LeaderboardUpdated -= HandleLeaderboardUpdated;
    }

    private void Start()
    {
        gameplayHud.SetActive(true);
        resultPanel.SetActive(false);

        instructionText.text = startInstruction;
        instructionText.gameObject.SetActive(true);

        UpdateAttemptText(gameStateMachine.CurrentAttempt, gameStateMachine.MaxAttempts);
    }

    private void HideInstruction()
    {
        instructionText.gameObject.SetActive(false);
    }

    private void UpdateAttemptText(int currentAttempt, int maxAttempts)
    {
        attemptText.text = $"Attempt: {currentAttempt} / {maxAttempts}";
    }

    private void HandleLeaderboardUpdated(GameResult result, int bestScore, bool isNewBest)
    {
        gameplayHud.SetActive(false);

        resultTitleText.text = result.IsPerfect ? "Perfect Smash!" : "Game Over";

        resultDetailText.text =
            $"Score: {result.FinalScore}\n" +
            $"Best: {bestScore}" + (isNewBest ? "  NEW!" : "") + "\n" +
            $"Cans: {result.KnockedCount} / {result.TotalCount}\n" +
            $"Attempts: {result.AttemptsUsed} / {result.MaxAttempts}";

        resultPanel.SetActive(true);
    }
    public void ResetForNewGame()
    {
        gameplayHud.SetActive(true);
        resultPanel.SetActive(false);

        instructionText.text = startInstruction;
        instructionText.gameObject.SetActive(true);

        UpdateAttemptText(gameStateMachine.CurrentAttempt, gameStateMachine.MaxAttempts);
    }
}