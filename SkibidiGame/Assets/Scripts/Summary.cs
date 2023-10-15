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
        MakeResult();
    }

    public void MakeResult()
    {
        FPSCamera.ShowCursor();

        foreach (Gun g in WeaponController.AllGuns)
        {
            g.enabled = false;
        }

        for (int i = 0; i < 4; i++)
        {
            ObjectiveTexts[i].text = LevelController.ObjectivesTexts[i].text;
            if (LevelController.Objectives[i].IsCompleted)
            {
                ObjectiveResultTexts[i].text = "Completed";
                ObjectiveResultTexts[i].color = Color.green;
            }
            else
            {
                ObjectiveResultTexts[i].text = "Failed";
                ObjectiveResultTexts[i].color = Color.red;
            }
        }
        ObjectiveTexts[4].text = "Complete level";
        ObjectiveResultTexts[4].text = "Completed";
        ObjectiveResultTexts[4].color = Color.green;

        Rank = GetRank();
        RankText.text = Rank;
        Money = (Completed + 1) * LevelController.RewardPerObjective;
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
