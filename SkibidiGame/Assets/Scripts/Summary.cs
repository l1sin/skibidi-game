using TMPro;
using UnityEngine;
using static LevelController;

public class Summary : MonoBehaviour
{
    public LevelController LevelController;
    public TextMeshProUGUI[] ObjectiveTexts;
    public TextMeshProUGUI[] ObjectiveResultTexts;
    public TextMeshProUGUI RankText;
    public TextMeshProUGUI MoneyAmountText;

    public Color[] RankColors;
    int Completed;
    int Money;

    public void Awake()
    {
        MakeResult();
    }

    public void MakeResult()
    {
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

        RankText.text = GetRank();
        Money = (Completed + 1) * LevelController.RewardPerObjective;
        MoneyAmountText.text = Money.ToString();
    }

    public string GetRank()
    {
        Completed = 0;
        foreach (Objective o in LevelController.Objectives)
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
}
