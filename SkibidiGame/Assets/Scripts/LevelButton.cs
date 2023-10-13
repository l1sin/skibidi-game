using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int LevelDifficulty;
    public MainMenuController MainMenuController;
    public void LoadLevel(int index)
    {
        DataPass data = MainMenuController.PassData();
        data.Difficulty = LevelDifficulty;
        SceneManager.LoadScene(index);
    }
}
