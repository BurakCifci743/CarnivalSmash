using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        scoreController.ScoreChanged += UpdateScoreText;
    }

    private void OnDisable()
    {
        scoreController.ScoreChanged -= UpdateScoreText;
    }

    private void Start()
    {
        UpdateScoreText(scoreController.CurrentScore);
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = $"Score: {score}";
    }
}