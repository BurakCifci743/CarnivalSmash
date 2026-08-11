using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonLock : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text labelText;

    [Header("Level")]
    [SerializeField] private int levelIndex;

    [Header("Text")]
    [SerializeField] private string unlockedTextFormat = "Level {0}";
    [SerializeField] private string lockedTextFormat = "Level {0} Locked";

    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (labelText == null)
        {
            labelText = GetComponentInChildren<TMP_Text>(true);
        }
    }

    private void OnEnable()
    {
        Refresh();
    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        bool isUnlocked = LevelProgressStore.IsLevelUnlocked(levelIndex);

        if (button != null)
        {
            button.interactable = isUnlocked;
        }

        if (labelText != null)
        {
            int displayLevelNumber = levelIndex + 1;

            labelText.text = isUnlocked
                ? string.Format(unlockedTextFormat, displayLevelNumber)
                : string.Format(lockedTextFormat, displayLevelNumber);
        }
    }

    [ContextMenu("Clear All Progress")]
    private void ClearAllProgress()
    {
        LevelProgressStore.ClearAllProgress();
        Refresh();
    }
}