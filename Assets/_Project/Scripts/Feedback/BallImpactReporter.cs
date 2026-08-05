using System;
using UnityEngine;

[RequireComponent(typeof(BallThrower))]
public class BallImpactReporter : MonoBehaviour
{
    public event Action<Vector3, float> BallImpacted;

    [Header("References")]
    [SerializeField] private BallThrower ballThrower;

    [Header("Impact Settings")]
    [SerializeField] private LayerMask impactLayerMask;
    [SerializeField] private float minImpactSpeed = 1.5f;
    [SerializeField] private bool reportOnlyFirstImpact = true;

    [Header("Debug")]
    [SerializeField] private bool logImpactDebug;

    private bool impactReported;

    private void Awake()
    {
        if (ballThrower == null)
        {
            ballThrower = GetComponent<BallThrower>();
        }
    }

    private void OnEnable()
    {
        ballThrower.BallThrown += ResetImpactReport;
    }

    private void OnDisable()
    {
        ballThrower.BallThrown -= ResetImpactReport;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (reportOnlyFirstImpact && impactReported) return;
        if (!IsInImpactLayer(collision.gameObject.layer)) return;

        float impactSpeed = collision.relativeVelocity.magnitude;

        if (impactSpeed < minImpactSpeed) return;

        Vector3 impactPoint = transform.position;

        if (collision.contactCount > 0)
        {
            impactPoint = collision.GetContact(0).point;
        }

        impactReported = true;

        if (logImpactDebug)
        {
            Debug.Log(
                $"BALL IMPACT | Object: {collision.collider.name} | Point: {impactPoint:F3} | Speed: {impactSpeed:F2}"
            );
        }

        BallImpacted?.Invoke(impactPoint, impactSpeed);
    }

    private void ResetImpactReport()
    {
        impactReported = false;
    }

    private bool IsInImpactLayer(int layer)
    {
        return (impactLayerMask.value & (1 << layer)) != 0;
    }
}