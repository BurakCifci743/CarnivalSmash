using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallImpactReporter : MonoBehaviour
{
    public event Action<Vector3, float> BallImpacted;

    [Header("References")]
    [SerializeField] private BallThrower ballThrower;

    [Header("Impact Settings")]
    [SerializeField] private float minImpactSpeed = 2f;
    [SerializeField] private bool reportOnlyFirstImpact = true;

    [Header("Debug")]
    [SerializeField] private bool logImpactDebug;

    private Rigidbody rb;
    private bool hasReportedImpact;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        ballThrower.BallThrown += ResetImpactReport;
    }

    private void OnDisable()
    {
        ballThrower.BallThrown -= ResetImpactReport;
    }

    private void ResetImpactReport()
    {
        hasReportedImpact = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ballThrower == null) return;
        if (!ballThrower.HasThrown) return;

        float impactSpeed = rb.linearVelocity.magnitude;

        if (impactSpeed < minImpactSpeed) return;
        if (reportOnlyFirstImpact && hasReportedImpact) return;

        hasReportedImpact = true;

        ContactPoint contact = collision.GetContact(0);

        BallImpacted?.Invoke(contact.point, impactSpeed);

        if (!logImpactDebug) return;

        Debug.Log(
            $"Ball Impact: {contact.point:F3} | Hit Object: {collision.collider.name} | Velocity: {rb.linearVelocity:F3} | Speed: {impactSpeed:F2}"
        );
    }
}