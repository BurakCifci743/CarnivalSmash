using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallImpactReporter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BallThrower ballThrower;

    [Header("Debug")]
    [SerializeField] private bool logImpactDebug;
    [SerializeField] private bool logOnlyFirstImpact = true;
    private Rigidbody rb;
    private bool hasLoggedImpact;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ballThrower == null) return;
        if (!ballThrower.HasThrown) return;

        if (logOnlyFirstImpact && hasLoggedImpact) return;

       hasLoggedImpact = true;

if (!logImpactDebug) return;

ContactPoint contact = collision.GetContact(0);

Debug.Log(
    $"Ball Impact: {contact.point:F3} | Hit Object: {collision.collider.name} | Velocity: {rb.linearVelocity:F3}"
);
    }
}