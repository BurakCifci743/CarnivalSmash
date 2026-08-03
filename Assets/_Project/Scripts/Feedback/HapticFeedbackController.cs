using UnityEngine;

public class HapticFeedbackController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BallImpactReporter ballImpactReporter;

    [Header("Haptic Settings")]
    [SerializeField] private bool enableHaptics = true;
    [SerializeField] private float minImpactSpeed = 3f;

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
        if (!enableHaptics) return;
        if (impactSpeed < minImpactSpeed) return;

#if UNITY_IOS || UNITY_ANDROID
        Handheld.Vibrate();
#endif
    }
}