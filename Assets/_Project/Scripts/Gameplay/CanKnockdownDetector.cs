using UnityEngine;

public class CanKnockdownDetector : MonoBehaviour
{
    [Header("Knockdown Settings")]
    [SerializeField] private float knockdownAngle = 45f;
    [SerializeField] private float heightDropThreshold = 0.12f;

    public bool IsKnockedDown { get; private set; }
    public float CurrentAngleFromUp => Vector3.Angle(transform.up, Vector3.up);

    private float startHeight;

    private void Awake()
    {
        startHeight = transform.position.y;
    }

    private void Update()
    {
        if (IsKnockedDown) return;

        if (IsTiltedEnough() || HasDroppedEnough())
        {
            IsKnockedDown = true;
        }
    }

    private bool IsTiltedEnough()
    {
        return CurrentAngleFromUp >= knockdownAngle;
    }

    private bool HasDroppedEnough()
    {
        return transform.position.y <= startHeight - heightDropThreshold;
    }
}