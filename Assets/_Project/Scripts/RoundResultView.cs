using TMPro;
using UnityEngine;

public class RoundResultView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoundController roundController;
    [SerializeField] private TMP_Text resultText;

    private void OnEnable()
    {
        roundController.RoundCompleted += UpdateResultText;
    }

    private void OnDisable()
    {
        roundController.RoundCompleted -= UpdateResultText;
    }

    private void Start()
{
    resultText.text = "Tap a can to throw";
}

    private void UpdateResultText(int knockedCount, int totalCount)
    {
        resultText.text = $"Cans: {knockedCount} / {totalCount}";
    }
}