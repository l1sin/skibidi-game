using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public Progress CurrentProgress;
    public int CurrentLevel;
    public int CurrentLevelDifficulty;
    public string path = "save.json";
    private void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public Progress LoadData()
    {
        Progress progress;
        if (PlayerPrefs.HasKey("save"))
        {
            string json = PlayerPrefs.GetString("save");
            progress = JsonUtility.FromJson<Progress>(json);
            CurrentProgress = progress;
            Debug.Log($"Loaded from PlayerPrefs:\n{json}");
        }
        else
        {
            progress = new Progress();
            SaveData(progress);
            Debug.Log("File do not exists. Creating save file");
        }

        
        //if (File.Exists($"{Application.dataPath}/{path}"))
        //{
        //    string json = File.ReadAllText($"{Application.dataPath}/{path}");
        //    progress = JsonUtility.FromJson<Progress>(json);
        //    CurrentProgress = progress;
        //    Debug.Log("File exists. Loading");
        //}
        //else
        //{
        //    progress = new Progress();
        //    Debug.Log("File do not exists. Creating save file");
        //    SaveData(progress);
        //}
        return progress;
    }

    public void SaveData(Progress progress)
    {
        CurrentProgress = progress;
        string json = JsonUtility.ToJson(progress);
        PlayerPrefs.SetString("save", json);
        PlayerPrefs.Save();
        Debug.Log($"Local save to PlayerPrefs");
    }
}
