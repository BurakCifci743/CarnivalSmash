using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadController : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private string levelSelectSceneName = "LevelSelect";
    [SerializeField] private string gameSceneName = "Game_Booth";

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene(levelSelectSceneName);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void LoadSelectedLevel(int levelIndex)
    {
        SelectedLevelStore.SelectLevel(levelIndex);
        LoadGame();
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}