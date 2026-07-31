using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BallThrowInput : MonoBehaviour
{
    [SerializeField] private BallThrower ballThrower;
    [SerializeField] private float throwPower = 1f;

    private void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ballThrower.Throw(Vector3.forward + Vector3.up * 0.55f, throwPower);
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}