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
        for (int i = 0; i <= Level; i++)
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

    public void UnlockUpgrades()
    {
        for (int i = 0; i < UpgradeBlocks.Length; i++)
        {
            UpgradeBlocks[i].UnlockLockers(UpgradeLevel[i]);
        }
    }

    public void UnlockGuns()
    {
        for (int i = 0; i < GunBlocks.Length; i++)
        {
            GunBlocks[i].UnlockLockers(GunLevel[i]);
        }
    }
}
