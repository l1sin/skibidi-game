using System;
using TMPro;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public Progress CurrentProgress;
    public int CurrentLevel;
    public int CurrentLevelDifficulty;
    public string path = "save.json";
    public string[,] Dictionary;
    public string[] Localization;
    public TMP_FontAsset[] Fonts;
    public TMP_FontAsset CurrentFont;
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

    public void LoadLanguage(string language)
    {
        Dictionary = Utility.Utility.ReadCSVString("Localization");

        int id = GetLanguageId(language);
        CurrentFont = Fonts[id - 1];
        Localization = new string[Dictionary.GetLength(0) - 1];

        for (int i = 1; i < Dictionary.GetLength(0); i++)
        {
            Localization[i - 1] = Dictionary[i, id];
        }
    }

    public int GetLanguageId(string language)
    {
        for (int j = 0; j < Dictionary.GetLength(1); j++)
        {
            if (Dictionary[0, j] == language)
            {
                return j;
            }
        }
        Debug.Log("Unknown language - switch to en");
        return GetLanguageId("en");
    }
}
