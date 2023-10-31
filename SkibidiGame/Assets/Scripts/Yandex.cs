using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Yandex : MonoBehaviour
{
    public static Yandex Instance;
    public const string Path = "idbfs/SkibidiGameSaveDirectory";

    [DllImport("__Internal")]
    public static extern string GetLanguage();

    [DllImport("__Internal")]
    public static extern void Rate();

    [DllImport("__Internal")]
    public static extern void WatchAd();

    [DllImport("__Internal")]
    public static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    public static extern void LoadExtern();

    [DllImport("__Internal")]
    public static extern void FullScreenAd();

    [DllImport("__Internal")]
    public static extern void CallRate();

    [DllImport("__Internal")]
    public static extern void CallPurchaseMenu(string id, string name);

    [DllImport("__Internal")]
    public static extern string GetPrice(int index);

    [DllImport("__Internal")]
    public static extern void GetYanIcon();


    private void Awake()
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
#if UNITY_EDITOR
        EditorInit();
        Debug.Log("UnityEditor");
#endif
    }

    public void StartInit()
    {
        Debug.Log("WebInit");
        string lang = GetLanguage();
        Debug.Log(lang);
        SaveManager.Instance.LoadLanguage(lang);
        LoadExtern();
    }

    public void EditorInit()
    {
        SaveManager.Instance.LoadLanguage("en");
        SaveManager.Instance.LoadDataLocal();
    }
}
