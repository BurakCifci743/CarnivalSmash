using UnityEngine;

[DefaultExecutionOrder(-200)]
public class LevelController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanLayoutSpawner canLayoutSpawner;
    [SerializeField] private GameStateMachine gameStateMachine;

    [Header("Levels")]
    [SerializeField] private LevelData[] levels;
    [SerializeField] private int startingLevelIndex;

    public LevelData CurrentLevel { get; private set; }
    public int CurrentLevelIndex { get; private set; }

    private void Awake()
    {
        LoadLevel(startingLevelIndex);
    }

    public void LoadLevel(int levelIndex)
    {
        if (levels == null || levels.Length == 0)
        {
            Debug.LogError("LevelController: No levels assigned.", this);
            return;
        }

        int safeIndex = Mathf.Clamp(levelIndex, 0, levels.Length - 1);
        LevelData selectedLevel = levels[safeIndex];

        if (selectedLevel == null)
        {
            Debug.LogError($"LevelController: Level at index {safeIndex} is missing.", this);
            return;
        }

        CurrentLevelIndex = safeIndex;
        CurrentLevel = selectedLevel;

        gameStateMachine.SetMaxAttempts(CurrentLevel.MaxAttempts);
        canLayoutSpawner.SetLevel(CurrentLevel, true);
    }

    public bool TryLoadNextLevel()
    {
        int nextIndex = CurrentLevelIndex + 1;

        if (nextIndex >= levels.Length)
        {
            return false;
        }

        LoadLevel(nextIndex);
        return true;
    }
}