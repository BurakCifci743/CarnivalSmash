using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BallThrowInput : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BallThrower ballThrower;
    [SerializeField] private RoundController roundController;
    [SerializeField] private Camera mainCamera;

    [Header("Tap Aim Settings")]
    [SerializeField] private float maxRayDistance = 100f;
    [SerializeField] private float minFlightTime = 0.25f;

    private void Update()
    {
        HandleTapToThrow();
        HandleDebugReset();
    }

    private void HandleTapToThrow()
    {
        if (!roundController.CanThrow) return;
        if (Pointer.current == null) return;

        if (!Pointer.current.press.wasPressedThisFrame) return;

        Vector2 screenPosition = Pointer.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, maxRayDistance)) return;

        Vector3 targetPoint = hit.point;
        Vector3 ballPosition = ballThrower.transform.position;

        Vector3 launchVelocity = CalculateLaunchVelocity(ballPosition, targetPoint);

        ballThrower.Throw(launchVelocity);
    }

    private Vector3 CalculateLaunchVelocity(Vector3 startPoint, Vector3 targetPoint)
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

        float horizontalSpeed = ballThrower.LaunchSpeed;
        float flightTime = horizontalDisplacement.magnitude / horizontalSpeed;
        flightTime = Mathf.Max(flightTime, minFlightTime);

        Vector3 horizontalVelocity = horizontalDisplacement.normalized * horizontalSpeed;

        float verticalVelocity =
            (displacement.y - 0.5f * Physics.gravity.y * flightTime * flightTime) / flightTime;

        return horizontalVelocity + Vector3.up * verticalVelocity;
    }

    private void HandleDebugReset()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}