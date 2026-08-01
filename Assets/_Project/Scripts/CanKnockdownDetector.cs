using UnityEngine;

public class CanKnockdownDetector : MonoBehaviour
{
    [Header("Knockdown Settings")]
    [SerializeField] private float knockdownAngle = 45f;

    public bool IsKnockedDown { get; private set; }
    public float CurrentAngleFromUp => Vector3.Angle(transform.up, Vector3.up);

    private void Update()
    {
        if (IsKnockedDown) return;

        if (CurrentAngleFromUp >= knockdownAngle)
        {
            IsKnockedDown = true;
        }
    }
}