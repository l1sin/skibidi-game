using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI MoneyText;
    public Button[] LevelButtons;
    public Button LastLevelButton;
    public TextMeshProUGUI[] Ranks;
    public Color[] RankColors;

    public UpgradeBlock[] UpgradeBlocks;
    public UpgradeBlock[] GunBlocks;

    [Header("SaveFile")]
    public int Money;
    public int Level;
    public int[] UpgradeLevel;
    public int[] GunLevel;
    public string[] LevelRank;

    public bool CanLoadData;

    [Header("Data")]
    public int[,] UpgradePrices;
    public int[,] GunPrices;

    public void Start()
    {
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
        UpgradePrices = ReadCSV("UpgradePrices");
        GunPrices = ReadCSV("GunPrices");
        UpdateBlocks();
    }

    public void LoadData()
    {
        Progress progress = new Progress();
        Money = progress.Money;
        Level = progress.Level;
        UpgradeLevel = progress.UpgradeLevel;
        GunLevel = progress.GunLevel;
        LevelRank = progress.LevelRank;
    }

    public void UnlockLevels()
    {
        int count;
        if (Level > LevelButtons.Length) count = LevelButtons.Length;
        else count = Level;
        for (int i = 0; i < count; i++)
        {
            LevelButtons[i].interactable = true;
        }
    }

    public void SetRanks()
    {
        for (int i = 0; i < Ranks.Length; i++)
        {
            string r = LevelRank[i];
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
            if (LevelRank[i] != "S") return;
        }
        LastLevelButton.interactable = true;
    }

    public void SetMoney()
    {
        MoneyText.text = Money.ToString();
    }

    public void SpendMoney(int money)
    {
        Money -= money;
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
            UpgradeBlocks[i].UnlockLockers(UpgradeLevel[i]);
        }
    }

    public void UnlockUpgrades(int index)
    {
        UpgradeBlocks[index].UnlockLockers(UpgradeLevel[index]);
    }

    public void UnlockGuns()
    {
        for (int i = 0; i < GunBlocks.Length; i++)
        {
            GunBlocks[i].UnlockLockers(GunLevel[i]);
        }
    }

    public void UnlockGuns(int index)
    {
        GunBlocks[index].UnlockLockers(GunLevel[index]);
    }

    public int[,] ReadCSV(string path)
    {
        int[,] priceList;
        TextAsset ta = Resources.Load<TextAsset>($"Prices/{path}");
        string[] lines = ta.text.Split('\n', System.StringSplitOptions.None);
        priceList = new int[lines.Length - 1, lines[0].Split(';', System.StringSplitOptions.None).Length - 1];
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(';', System.StringSplitOptions.None);

            for (int j = 1; j < values.Length; j++)
            {
                priceList[i - 1 , j - 1] = int.Parse(values[j]);
            }
        }
        return priceList;
    }

    public DataPass PassData()
    {
        Progress progress = new Progress();
        progress.Money = Money;
        progress.Level = Level;
        progress.UpgradeLevel = UpgradeLevel;
        progress.GunLevel = GunLevel;
        progress.LevelRank = LevelRank;
        GameObject dataPass = new GameObject();
        DataPass ProgressData = dataPass.AddComponent<DataPass>();
        ProgressData.Progress = progress;
        DontDestroyOnLoad(dataPass);
        return ProgressData;
    }
}

