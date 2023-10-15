using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int LevelDifficulty;
    public MainMenuController MainMenuController;
    public void LoadLevel(int index)
    {
        SaveManager.Instance.CurrentLevelDifficulty = LevelDifficulty;
        SaveManager.Instance.CurrentLevel = index + (10 * LevelDifficulty);
        SceneManager.LoadScene(index);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
