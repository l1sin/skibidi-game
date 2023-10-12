using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMethods : MonoBehaviour
{
    public int LevelDifficulty;
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
