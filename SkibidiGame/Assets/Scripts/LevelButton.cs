using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int LevelDifficulty;
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
