using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BallThrowInput : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BallThrower ballThrower;
    [SerializeField] private RoundController roundController;

    [Header("Throw Test Settings")]
    [SerializeField] private float throwPower = 1f;

    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (!roundController.CanThrow) return;

            ballThrower.Throw(Vector3.forward + Vector3.up * 0.55f, throwPower);
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}