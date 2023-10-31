using TMPro;
using UnityEngine;

public class Summary : MonoBehaviour
{
    public LevelController LevelController;
    public FPSCamera FPSCamera;
    public WeaponController WeaponController;
    public TextMeshProUGUI[] ObjectiveTexts;
    public TextMeshProUGUI[] ObjectiveResultTexts;
    public TextMeshProUGUI RankText;
    public TextMeshProUGUI MoneyAmountText;

    public Color[] RankColors;
    int Completed;
    string Rank;
    int Money;

    public void Awake()
    {
        ApplyFonts();
        MakeResult();
    }

    public void ApplyFonts()
    {
        foreach (TextMeshProUGUI tmp in ObjectiveTexts)
        {
            tmp.font = SaveManager.Instance.CurrentFont;
        }
        foreach (TextMeshProUGUI tmp in ObjectiveResultTexts)
        {
            tmp.font = SaveManager.Instance.CurrentFont;
        }
        RankText.font = SaveManager.Instance.CurrentFont;
        MoneyAmountText.font = SaveManager.Instance.CurrentFont;
    }

    public void MakeResult()
    {
        for (int i = 0; i < 4; i++)
        {
            ObjectiveTexts[i].text = LevelController.ObjectivesTexts[i].text;
            if (LevelController.Objectives[i].IsCompleted)
            {
                ObjectiveResultTexts[i].text = SaveManager.Instance.Localization[30];
                ObjectiveResultTexts[i].color = Color.green;
            }
            else
            {
                ObjectiveResultTexts[i].text = SaveManager.Instance.Localization[31];
                ObjectiveResultTexts[i].color = Color.red;
            }
        }
        ObjectiveTexts[4].text = SaveManager.Instance.Localization[33];
        ObjectiveResultTexts[4].text = SaveManager.Instance.Localization[30];
        ObjectiveResultTexts[4].color = Color.green;

        Rank = GetRank();
        RankText.text = Rank;
        Money = (Completed + 1) * LevelController.RewardPerObjective * (SaveManager.Instance.CurrentLevelDifficulty + 1);
        MoneyAmountText.text = Money.ToString();

        SaveGame();
    }

    public string GetRank()
    {
        Completed = 0;
        foreach (LevelController.Objective o in LevelController.Objectives)
        {
            if (o.IsCompleted) Completed++;
        }
        switch (Completed)
        {
            case 0:
                RankText.color = RankColors[4];
                return "D";
            case 1:
                RankText.color = RankColors[3];
                return "C";
            case 2:
                RankText.color = RankColors[2];
                return "B";
            case 3:
                RankText.color = RankColors[1];
                return "A";
            case 4:
                RankText.color = RankColors[0];
                return "S";
            default:
                return "";
        }
    }

    public void SaveGame()
    {
        Progress progress = SaveManager.Instance.CurrentProgress;
        int lastRankValue = 0;
        switch (progress.LevelRank[SaveManager.Instance.CurrentLevel - 1])
        {
            case "D":
                lastRankValue = 1;
                break;
            case "C":
                lastRankValue = 2;
                break;
            case "B":
                lastRankValue = 3;
                break;
            case "A":
                lastRankValue = 4;
                break;
            case "S":
                lastRankValue = 5;
                break;
            default:
                lastRankValue = 0;
                break;
        }

        if (Completed >= lastRankValue)
        progress.LevelRank[SaveManager.Instance.CurrentLevel - 1] = Rank;

        progress.Money += Money;
        if (SaveManager.Instance.CurrentLevel > progress.Level)
        {
            progress.Level = SaveManager.Instance.CurrentLevel;
        }
        SaveManager.Instance.CurrentProgress = progress;
        SaveManager.Instance.SaveData(SaveManager.Instance.CurrentProgress);
    }
}
