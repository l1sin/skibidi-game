using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI MoneyText;
    public Button[] LevelButtons;
    public Button LastLevelButton;
    public TextMeshProUGUI[] Ranks;
    public Color[] RankColors;

    public GameObject Hint;

    public UpgradeBlock[] UpgradeBlocks;
    public UpgradeBlock[] GunBlocks;

    [Header("SaveFile")]
    public Progress Progress;

    public bool CanLoadData;

    [Header("Data")]
    public int[,] UpgradePrices;
    public int[,] GunPrices;

    public int AdBonus;
    public Texture YanTexure;

    public void Start()
    {
#if UNITY_EDITOR
        Debug.Log("FullScreenAd");
#elif UNITY_WEBGL
        Yandex.FullScreenAd();
#endif
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (CanLoadData)
        {
            LoadData();
        }
        UnlockLevels();
        SetRanks();
        UnlockLastLevel();
        SetMoney();
        UnlockUpgrades();
        UnlockGuns();
        UpgradePrices = Utility.Utility.ReadCSVInt("Prices/UpgradePrices");
        GunPrices = Utility.Utility.ReadCSVInt("Prices/GunPrices");
#if UNITY_EDITOR
        Debug.Log("LoadYanIcon");
        Debug.Log("LoadYanPrices");
        SetYanTexture("https://yastatic.net/s3/games-static/static-data/images/payments/sdk/currency-icon-m.png");
#elif UNITY_WEBGL
        Yandex.GetYanIcon();
        GetYanPrices();
#endif
        UpdateBlocks();
    }

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.KeypadMinus))
    //    {
    //        Progress = new Progress();
    //        SaveManager.Instance.SaveData(Progress);
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    }
    //    if (Input.GetKeyDown(KeyCode.KeypadPlus))
    //    {
    //        Progress = new Progress();
    //        Progress.Level = 31;
    //        for (int i = 0; i < Progress.UpgradeLevel.Length; i++)
    //        {
    //            Progress.UpgradeLevel[i] = 5;
    //        }
    //        for (int i = 0; i < Progress.GunLevel.Length; i++)
    //        {
    //            Progress.GunLevel[i] = 5;
    //        }
    //        for (int i = 0; i< Progress.LevelRank.Length; i++)
    //        {
    //            Progress.LevelRank[i] = "S";
    //        }
    //        SaveManager.Instance.SaveData(Progress);
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    }
    //}

    public void GetYanPrices()
    {
        foreach (UpgradeBlock ub in UpgradeBlocks)
        {
            ub.BuyAllButtonText.text = Yandex.GetPrice(ub.ProductIndex);
        }
        foreach (UpgradeBlock ub in GunBlocks)
        {
            ub.BuyAllButtonText.text = Yandex.GetPrice(ub.ProductIndex);
        }
        Debug.Log("Prices set");
    }

    public void SetYanTexture(string url)
    {
        StartCoroutine(DownloadYanImage(url));
        Debug.Log("Icons set");
    }

    public IEnumerator DownloadYanImage(string mediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            YanTexure = ((DownloadHandlerTexture)request.downloadHandler).texture;
            foreach (var b in UpgradeBlocks)
            {
                b.YanIcon.texture = YanTexure;
            }
            foreach (var b in GunBlocks)
            {
                b.YanIcon.texture = YanTexure;
            }
        }
    }

    public void GetAdReward()
    {
        GetMoney(AdBonus);
    }

    public void GetMoney(int money)
    {
        Progress.Money += money;
        SetMoney();
        UpdateBlocks();
        SaveData();
    }

    public void LoadData()
    {
        Progress = SaveManager.Instance.CurrentProgress;
    }
    public void SaveData()
    {
        SaveManager.Instance.SaveData(Progress);
    }

    public void UnlockLevels()
    {
        int count = Progress.Level;
        if (count > LevelButtons.Length - 1) count = LevelButtons.Length - 1;
        for (int i = 0; i <= count; i++)
        {
            LevelButtons[i].interactable = true;
        }
    }

    public void SetRanks()
    {
        for (int i = 0; i < Ranks.Length; i++)
        {
            string r = Progress.LevelRank[i];
            Ranks[i].text = r;
            Ranks[i].color = SetRankColor(r);
        }
    }

    public Color SetRankColor(string r)
    {
        switch (r)
        {
            case "S": return RankColors[0];
            case "A": return RankColors[1];
            case "B": return RankColors[2];
            case "C": return RankColors[3];
            case "D": return RankColors[4];
            default: return RankColors[5];
        }
    }

    public void UnlockLastLevel()
    {
        for (int i = 0; i < 30; i++)
        {
            if (Progress.LevelRank[i] != "S") return;
        }
        LastLevelButton.interactable = true;
        Hint.SetActive(false);
    }

    public void SetMoney()
    {
        MoneyText.text = Progress.Money.ToString();
    }

    public void SpendMoney(int money)
    {
        Progress.Money -= money;
        SetMoney();
        UpdateBlocks();
    }
    public void UpdateBlocks()
    {
        foreach (var b in UpgradeBlocks)
        {
            b.UpdatePrice();
        }
        foreach (var b in GunBlocks)
        {
            b.UpdatePrice();
        }
    }

    public void UnlockUpgrades()
    {
        for (int i = 0; i < UpgradeBlocks.Length; i++)
        {
            UpgradeBlocks[i].UnlockLockers(Progress.UpgradeLevel[i]);
        }
    }

    public void UnlockUpgrades(int index)
    {
        UpgradeBlocks[index].UnlockLockers(Progress.UpgradeLevel[index]);
    }

    public void UnlockGuns()
    {
        for (int i = 0; i < GunBlocks.Length; i++)
        {
            GunBlocks[i].UnlockLockers(Progress.GunLevel[i]);
        }
    }

    public void UnlockGuns(int index)
    {
        GunBlocks[index].UnlockLockers(Progress.GunLevel[index]);
    }
}

