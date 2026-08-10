using UnityEngine;

[CreateAssetMenu(fileName = "LevelData_", menuName = "Carnival Smash/Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Level Info")]
    [SerializeField] private int levelNumber = 1;
    [SerializeField] private string levelName = "Level 1";

    [Header("Gameplay")]
    [SerializeField] private CanLayoutType layoutType = CanLayoutType.Pyramid15;
    [SerializeField] private int maxAttempts = 3;

    [Header("Can Layout")]
    [SerializeField] private float horizontalSpacing = 0.30f;
    [SerializeField] private float verticalSpacing = 0.45f;
    [SerializeField] private float bottomRowY = 0.90f;
    [SerializeField] private float spawnZ = 3.50f;

    public int LevelNumber => levelNumber;
    public string LevelName => levelName;
    public CanLayoutType LayoutType => layoutType;
    public int MaxAttempts => maxAttempts;

    public float HorizontalSpacing => horizontalSpacing;
    public float VerticalSpacing => verticalSpacing;
    public float BottomRowY => bottomRowY;
    public float SpawnZ => spawnZ;

    public int[] GetRowCounts()
    {
        return layoutType switch
        {
            CanLayoutType.Pyramid3 => new[] { 2, 1 },
            CanLayoutType.Pyramid5 => new[] { 3, 2 },
            CanLayoutType.Pyramid10 => new[] { 4, 3, 2, 1 },
            CanLayoutType.Pyramid15 => new[] { 5, 4, 3, 2, 1 },
            _ => new[] { 5, 4, 3, 2, 1 }
        };
    }

    public int GetTotalCanCount()
    {
        int total = 0;
        int[] rows = GetRowCounts();

        for (int i = 0; i < rows.Length; i++)
        {
            total += rows[i];
        }

        return total;
    }

    private void OnValidate()
    {
        levelNumber = Mathf.Max(1, levelNumber);
        maxAttempts = Mathf.Max(1, maxAttempts);

        horizontalSpacing = Mathf.Max(0.01f, horizontalSpacing);
        verticalSpacing = Mathf.Max(0.01f, verticalSpacing);
    }
}