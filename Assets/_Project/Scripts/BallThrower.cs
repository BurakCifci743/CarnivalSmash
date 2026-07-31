using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallThrower : MonoBehaviour
{
    public event Action BallThrown;

    [Header("Throw Settings")]
    [SerializeField] private float forceMultiplier = 10f;

    private Rigidbody rb;
    private bool hasThrown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Throw(Vector3 direction, float power)
    {
        if (hasThrown) return;

        hasThrown = true;

        Vector3 force = direction.normalized * power * forceMultiplier;
        rb.AddForce(force, ForceMode.VelocityChange);

        BallThrown?.Invoke();
    }
}