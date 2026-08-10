using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallThrower : MonoBehaviour
{
    public event Action BallThrown;

    [Header("Throw Settings")]
    [SerializeField] private float launchSpeed = 17f;
    

    private Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool hasThrown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        startPosition = transform.position;
        startRotation = transform.rotation;
        ResetBall();
    }

    public bool ThrowAtTarget(Vector3 targetPoint, float minFlightTime)
    {
        if (hasThrown) return false;

        Vector3 launchVelocity = CalculateLaunchVelocity(transform.position, targetPoint, minFlightTime);

        if (launchVelocity.sqrMagnitude <= 0.001f) return false;

        hasThrown = true;
        rb.WakeUp();

        rb.AddForce(launchVelocity, ForceMode.VelocityChange);

        BallThrown?.Invoke();

        return true;
    }

    public void ResetBall()
{
    hasThrown = false;

    rb.linearVelocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;

    rb.position = startPosition;
    rb.rotation = startRotation;

    transform.SetPositionAndRotation(startPosition, startRotation);

    rb.Sleep();
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