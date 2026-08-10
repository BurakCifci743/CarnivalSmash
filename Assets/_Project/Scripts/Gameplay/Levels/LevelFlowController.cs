using UnityEngine;

public class LevelFlowController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LevelController levelController;
    [SerializeField] private GameStateMachine gameStateMachine;
    [SerializeField] private BallThrower ballThrower;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private ResultPanelView resultPanelView;

    public void RetryCurrentLevel()
    {
        levelController.ReloadCurrentLevel();
        ResetGameplayState();
    }

    public void LoadNextLevel()
    {
        bool loadedNextLevel = levelController.TryLoadNextLevel();

        if (!loadedNextLevel)
        {
            Debug.Log("LevelFlowController: No next level available.");
            return;
        }

        ResetGameplayState();
    }

    private void ResetGameplayState()
    {
        ballThrower.ResetBall();
        scoreController.ResetScore();
        gameStateMachine.StartNewGame();
        resultPanelView.ResetForNewGame();
    }
}