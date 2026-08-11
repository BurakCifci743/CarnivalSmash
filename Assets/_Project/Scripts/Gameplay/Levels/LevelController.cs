using System;
using UnityEngine;

[DefaultExecutionOrder(-200)]
public class LevelController : MonoBehaviour
{
    public event Action<LevelData, int> LevelLoaded;

    [Header("References")]
    [SerializeField] private CanLayoutSpawner canLayoutSpawner;
    [SerializeField] private GameStateMachine gameStateMachine;

    [Header("Levels")]
    [SerializeField] private LevelData[] levels;
    [SerializeField, Min(1)] private int startingLevelNumber = 1;

    public LevelData CurrentLevel { get; private set; }
    public int CurrentLevelIndex { get; private set; }
    public int CurrentLevelNumber => CurrentLevelIndex + 1;

    private void Awake()
    {
        int levelIndex = SelectedLevelStore.HasSelectedLevel
            ? SelectedLevelStore.SelectedLevelIndex
            : startingLevelNumber - 1;

        LoadLevel(levelIndex);
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

        LevelLoaded?.Invoke(CurrentLevel, CurrentLevelIndex);
    }

    public void ReloadCurrentLevel()
    {
        LoadLevel(CurrentLevelIndex);
    }

    public bool HasNextLevel()
    {
        return levels != null && CurrentLevelIndex + 1 < levels.Length;
    }

    public bool TryLoadNextLevel()
    {
        if (!HasNextLevel())
        {
            return false;
        }

        LoadLevel(CurrentLevelIndex + 1);
        return true;
    }

    private void OnValidate()
    {
        startingLevelNumber = Mathf.Max(1, startingLevelNumber);
    }
}