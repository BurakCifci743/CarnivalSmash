using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallThrower : MonoBehaviour
{
    public event Action BallThrown;

    [Header("Throw Settings")]
    [SerializeField] private float launchSpeed = 17f;

    public bool HasThrown => hasThrown;

    private Rigidbody rb;
    private bool hasThrown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool ThrowAtTarget(Vector3 targetPoint, float minFlightTime)
    {
        if (hasThrown) return false;

        Vector3 launchVelocity = CalculateLaunchVelocity(transform.position, targetPoint, minFlightTime);

        if (launchVelocity.sqrMagnitude <= 0.001f) return false;

        hasThrown = true;

        rb.AddForce(launchVelocity, ForceMode.VelocityChange);

        BallThrown?.Invoke();

        return true;
    }

    private Vector3 CalculateLaunchVelocity(Vector3 startPoint, Vector3 targetPoint, float minFlightTime)
    {
        Vector3 displacement = targetPoint - startPoint;

        Vector3 horizontalDisplacement = new Vector3(
            displacement.x,
            0f,
            displacement.z
        );

        if (horizontalDisplacement.sqrMagnitude <= 0.001f)
        {
            return Vector3.zero;
        }

        float flightTime = horizontalDisplacement.magnitude / launchSpeed;
        flightTime = Mathf.Max(flightTime, minFlightTime);

        Vector3 horizontalVelocity = horizontalDisplacement.normalized * launchSpeed;

        float verticalVelocity =
            (displacement.y - 0.5f * Physics.gravity.y * flightTime * flightTime) / flightTime;

        return horizontalVelocity + Vector3.up * verticalVelocity;
    }
}