using UnityEngine;

public class CanKnockdownDetector : MonoBehaviour
{
    [Header("Knockdown Settings")]
    [SerializeField] private float knockdownAngle = 45f;

    public bool IsKnockedDown { get; private set; }

    private void Update()
    {
        if (IsKnockedDown) return;

        float angleFromUp = Vector3.Angle(transform.up, Vector3.up);

        if (angleFromUp >= knockdownAngle)
        {
            IsKnockedDown = true;
        }
    }
}