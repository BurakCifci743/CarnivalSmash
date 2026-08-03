using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BallImpactReporter ballImpactReporter;
    [SerializeField] private Transform cameraTransform;

    [Header("Shake Settings")]
    [SerializeField] private float duration = 0.12f;
    [SerializeField] private float strength = 0.04f;
    [SerializeField] private float maxImpactSpeed = 18f;

    private Vector3 originalLocalPosition;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        originalLocalPosition = cameraTransform.localPosition;
    }

    private void OnEnable()
    {
        ballImpactReporter.BallImpacted += HandleBallImpact;
    }

    private void OnDisable()
    {
        ballImpactReporter.BallImpacted -= HandleBallImpact;
    }

    private void HandleBallImpact(Vector3 impactPoint, float impactSpeed)
{
    float impactRatio = Mathf.Clamp01(impactSpeed / maxImpactSpeed);
    float shakeStrength = strength * impactRatio;

    if (shakeRoutine != null)
    {
        StopCoroutine(shakeRoutine);
    }

    shakeRoutine = StartCoroutine(Shake(shakeStrength));
    }
    private IEnumerator Shake(float shakeStrength)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeStrength;
            randomOffset.z = 0f;

            cameraTransform.localPosition = originalLocalPosition + randomOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalLocalPosition;
        shakeRoutine = null;
    }
}