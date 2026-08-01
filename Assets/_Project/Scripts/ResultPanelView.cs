using TMPro;
using UnityEngine;

public class ResultPanelView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoundController roundController;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private GameObject gameplayHud;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TMP_Text resultTitleText;
    [SerializeField] private TMP_Text resultDetailText;

    private void OnEnable()
    {
        roundController.RoundCompleted += HandleRoundCompleted;
    }

    private void OnDisable()
    {
        roundController.RoundCompleted -= HandleRoundCompleted;
    }

    private void Start()
    {
        gameplayHud.SetActive(true);
        resultPanel.SetActive(false);
    }

    private void HandleRoundCompleted(int knockedCount, int totalCount)
    {
        gameplayHud.SetActive(false);

        int score = scoreController.CalculateScore(knockedCount);

        resultTitleText.text = "Round Complete";
        resultDetailText.text = $"Score: {score}\nCans: {knockedCount} / {totalCount}";

        resultPanel.SetActive(true);
    }
}