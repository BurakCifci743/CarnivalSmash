using TMPro;
using UnityEngine;

public class ResultPanelView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameStateMachine gameStateMachine;
    [SerializeField] private BallThrower ballThrower;
    [SerializeField] private ScoreController scoreController;
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
        gameStateMachine.GameEnded += HandleGameEnded;
        gameStateMachine.AttemptChanged += UpdateAttemptText;
        ballThrower.BallThrown += HideInstruction;
    }

    private void OnDisable()
    {
        gameStateMachine.GameEnded -= HandleGameEnded;
        gameStateMachine.AttemptChanged -= UpdateAttemptText;
        ballThrower.BallThrown -= HideInstruction;
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

   private void HandleGameEnded(int knockedCount, int totalCount)
{
    gameplayHud.SetActive(false);

    int attemptsUsed = gameStateMachine.CurrentAttempt;
    int finalScore = scoreController.CalculateFinalScore(
        knockedCount,
        attemptsUsed,
        gameStateMachine.MaxAttempts
    );

    bool allCansKnocked = knockedCount >= totalCount;

    resultTitleText.text = allCansKnocked ? "Perfect Smash!" : "Game Over";

    resultDetailText.text =
        $"Score: {finalScore}\n" +
        $"Cans: {knockedCount} / {totalCount}\n" +
        $"Attempts: {attemptsUsed} / {gameStateMachine.MaxAttempts}";

    resultPanel.SetActive(true);
}
}