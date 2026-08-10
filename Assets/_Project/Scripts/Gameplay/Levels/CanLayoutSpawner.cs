using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class CanLayoutSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject canPrefab;
    [SerializeField] private Transform canParent;

    [Header("Spawn Settings")]
    [SerializeField] private bool spawnOnAwake = true;
    [SerializeField] private bool clearExistingCans = true;
    [SerializeField] private string canNamePrefix = "Can_";

    private readonly List<Rigidbody> spawnedRigidbodies = new();

    private void Awake()
    {
        if (spawnOnAwake)
        {
            SpawnCurrentLevel();
        }
    }

    public void SetLevel(LevelData newLevelData, bool spawnImmediately = true)
    {
        levelData = newLevelData;

        if (spawnImmediately)
        {
            SpawnCurrentLevel();
        }
    }

    [ContextMenu("Spawn Current Level")]
    public void SpawnCurrentLevel()
    {
        if (levelData == null)
        {
            Debug.LogError("CanLayoutSpawner: LevelData is not assigned.", this);
            return;
        }

        if (canPrefab == null)
        {
            Debug.LogError("CanLayoutSpawner: Can prefab is not assigned.", this);
            return;
        }

        if (canParent == null)
        {
            canParent = transform;
        }

        if (clearExistingCans)
        {
            ClearExistingCans();
        }

        SpawnLayout();
    }

    private void SpawnLayout()
    {
        spawnedRigidbodies.Clear();

        int canIndex = 1;
        int[] rowCounts = levelData.GetRowCounts();

        for (int rowIndex = 0; rowIndex < rowCounts.Length; rowIndex++)
        {
            int cansInRow = rowCounts[rowIndex];

            float rowY = levelData.BottomRowY + rowIndex * levelData.VerticalSpacing;
            float startX = -((cansInRow - 1) * levelData.HorizontalSpacing) * 0.5f;

            for (int columnIndex = 0; columnIndex < cansInRow; columnIndex++)
            {
                float x = startX + columnIndex * levelData.HorizontalSpacing;

                Vector3 localPosition = new Vector3(
                    x,
                    rowY,
                    levelData.SpawnZ
                );

                GameObject canInstance = Instantiate(canPrefab, canParent, false);

                canInstance.SetActive(false);

                canInstance.name = $"{canNamePrefix}{canIndex:00}";
                canInstance.transform.SetLocalPositionAndRotation(
                    localPosition,
                    Quaternion.identity
                );

                canInstance.transform.localScale = canPrefab.transform.localScale;

                Rigidbody rb = canInstance.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.detectCollisions = false;
                    rb.isKinematic = true;

                    spawnedRigidbodies.Add(rb);
                }

                canInstance.SetActive(true);

                canIndex++;
            }
        }

        Physics.SyncTransforms();

        for (int i = 0; i < spawnedRigidbodies.Count; i++)
        {
            Rigidbody rb = spawnedRigidbodies[i];

            rb.isKinematic = false;
            rb.detectCollisions = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
        }
    }

    private void ClearExistingCans()
    {
        if (canParent == null) return;

        for (int i = canParent.childCount - 1; i >= 0; i--)
        {
            Transform child = canParent.GetChild(i);

            if (Application.isPlaying)
            {
                Destroy(child.gameObject);
            }
            else
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }
}