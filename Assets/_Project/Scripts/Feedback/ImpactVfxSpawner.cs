using UnityEngine;

public class ImpactVfxSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BallImpactReporter ballImpactReporter;
    [SerializeField] private GameObject impactVfxPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnOffset = 0.02f;

    private void OnEnable()
    {
        ballImpactReporter.BallImpacted += SpawnImpactVfx;
    }

    private void OnDisable()
    {
        ballImpactReporter.BallImpacted -= SpawnImpactVfx;
    }

    private void SpawnImpactVfx(Vector3 impactPoint, float impactSpeed)
    {
        if (impactVfxPrefab == null) return;

        Vector3 spawnPosition = impactPoint + Vector3.up * spawnOffset;

        Instantiate(
            impactVfxPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}