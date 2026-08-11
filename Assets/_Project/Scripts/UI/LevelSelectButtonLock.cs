using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelectButtonLock : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text progressText;

    [Header("Level")]
    [SerializeField] private int levelIndex;

    [Header("Text")]
    [SerializeField] private string unlockedProgressText = "PLAY";
    [SerializeField] private string lockedProgressText = "LOCKED";
    [SerializeField] private string bestScoreTextFormat = "BEST {0}";

    [Header("Visual Feedback")]
    [SerializeField] private GameObject lockedVisual;
    [SerializeField] private Image lockedImage;

    [Header("Locked Click Feedback")]
    [SerializeField] private Color lockedColor = new Color(0f, 0f, 0f, 0.75f);
    [SerializeField] private Color warningColor = new Color(1f, 0f, 0f, 0.75f);
    [SerializeField] private int blinkCount = 3;
    [SerializeField] private float blinkInterval = 0.08f;

    private bool isUnlocked;
    private Coroutine blinkCoroutine;

    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (lockedImage == null && lockedVisual != null)
        {
            lockedImage = lockedVisual.GetComponent<Image>();
        }
    }

    private void OnEnable()
    {
        Refresh();
    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        isUnlocked = LevelProgressStore.IsLevelUnlocked(levelIndex);

        if (button != null)
        {
            button.interactable = isUnlocked;
        }

        if (lockedVisual != null)
        {
            lockedVisual.SetActive(!isUnlocked);
        }

        if (lockedImage != null)
        {
            lockedImage.color = lockedColor;
        }

        UpdateProgressText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isUnlocked) return;
        if (lockedImage == null) return;

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }

        blinkCoroutine = StartCoroutine(BlinkLockedVisual());
    }

    private void UpdateProgressText()
    {
        if (progressText == null) return;

        if (!isUnlocked)
        {
            progressText.text = lockedProgressText;
            return;
        }

        int bestScore = LevelProgressStore.GetBestScore(levelIndex);

        progressText.text = bestScore > 0
            ? string.Format(bestScoreTextFormat, bestScore)
            : unlockedProgressText;
    }

    private IEnumerator BlinkLockedVisual()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            lockedImage.color = warningColor;
            yield return new WaitForSeconds(blinkInterval);

            lockedImage.color = lockedColor;
            yield return new WaitForSeconds(blinkInterval);
        }

        lockedImage.color = lockedColor;
        blinkCoroutine = null;
    }

    [ContextMenu("Clear All Progress")]
    private void ClearAllProgress()
    {
        LevelProgressStore.ClearAllProgress();
        Refresh();
    }
}