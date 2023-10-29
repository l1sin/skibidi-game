using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int LevelDifficulty;
    public MainMenuController MainMenuController;
    public void LoadLevel(int index)
    {
        SaveManager.Instance.CurrentLevelDifficulty = LevelDifficulty;
        if (LevelDifficulty < 3)
        {
            SaveManager.Instance.CurrentLevel = index + (10 * LevelDifficulty);
        }
        else
        {
            SaveManager.Instance.CurrentLevel = 31;
        }
        SceneManager.LoadScene(index);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(12);
    }

    public void LoadThisLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
