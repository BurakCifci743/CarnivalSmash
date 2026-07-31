using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallThrower : MonoBehaviour
{
    public event Action BallThrown;

    [Header("Throw Settings")]
    [SerializeField] private float launchSpeed = 16f;

    public float LaunchSpeed => launchSpeed;

    private Rigidbody rb;
    private bool hasThrown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Throw(Vector3 launchVelocity)
    {
        if (hasThrown) return;

        hasThrown = true;

        rb.AddForce(launchVelocity, ForceMode.VelocityChange);

        BallThrown?.Invoke();
    }
}