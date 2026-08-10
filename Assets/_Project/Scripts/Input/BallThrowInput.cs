using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class BallThrowInput : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BallThrower ballThrower;
    [SerializeField] private GameStateMachine gameStateMachine;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask aimLayerMask;
    [SerializeField] private GameResetController gameResetController;

    [Header("Tap Aim Settings")]
    [SerializeField] private float maxRayDistance = 100f;
    [SerializeField] private float minFlightTime = 0.30f;

    [Header("Aim Correction")]
    [SerializeField] private float verticalAimOffset = 0.10f;

    [Header("Debug")]
    [SerializeField] private Transform debugTargetMarker;
    [SerializeField] private bool logAimDebug;

    private void Update()
    {
        HandleTapToThrow();
        HandleDebugReset();
    }

    private void HandleTapToThrow()
    {
        if (!gameStateMachine.CanAcceptThrow) return;
        if (Pointer.current == null) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!Pointer.current.press.wasPressedThisFrame) return;

        Vector2 screenPosition = Pointer.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, aimLayerMask, QueryTriggerInteraction.Collide)) return;

        Vector3 rawTargetPoint = hit.point;
        Vector3 correctedTargetPoint = rawTargetPoint + Vector3.up * verticalAimOffset;

        if (logAimDebug)
        {
            Debug.Log(
                $"Aim Target: {rawTargetPoint:F3} | Corrected Target: {correctedTargetPoint:F3} | Hit Object: {hit.collider.name}"
            );
        }

        if (debugTargetMarker != null)
        {
            debugTargetMarker.position = rawTargetPoint;
            debugTargetMarker.gameObject.SetActive(true);
        }

        ballThrower.ThrowAtTarget(correctedTargetPoint, minFlightTime);
    }

    private void HandleDebugReset()
{
    if (Keyboard.current == null) return;

    if (Keyboard.current.rKey.wasPressedThisFrame)
    {
        gameResetController.ResetScene();
    }
}
}