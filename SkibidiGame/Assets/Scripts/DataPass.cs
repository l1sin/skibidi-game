using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPass : MonoBehaviour
{
    public Progress Progress;
    public int Difficulty;

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        WeaponController wc = FindObjectOfType<WeaponController>();
        wc.SetGunProperties(Progress.UpgradeLevel, Progress.GunLevel);
        CharacterMovement cm = FindObjectOfType<CharacterMovement>();
        cm.SetSpeedLevel(Progress.UpgradeLevel[1]);
        CharacterHealth ch = FindObjectOfType<CharacterHealth>();
        ch.HealthLevel = Progress.UpgradeLevel[0];
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
