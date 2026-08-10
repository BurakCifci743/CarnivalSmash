using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelController levelController;
    [SerializeField] private TMP_Text levelText;

    private void OnEnable()
    {
        levelController.LevelLoaded += HandleLevelLoaded;
    }

    private void OnDisable()
    {
        levelController.LevelLoaded -= HandleLevelLoaded;
    }

    private void Start()
    {
        UpdateLevelText(levelController.CurrentLevel);
    }

    private void HandleLevelLoaded(LevelData levelData, int levelIndex)
    {
        UpdateLevelText(levelData);
    }

    private void UpdateLevelText(LevelData levelData)
    {
        if (levelData == null) return;

        levelText.text = levelData.LevelName;
    }
}