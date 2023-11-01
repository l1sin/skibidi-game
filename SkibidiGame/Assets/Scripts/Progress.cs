using System;

[Serializable]
public class Progress
{
    public int Money;
    public int Level;
    public int[] UpgradeLevel;
    public int[] GunLevel;
    public string[] LevelRank;

    public Progress()
    {
        Money = 0;
        Level = 0;
        UpgradeLevel = new int[3] { 0, 0, 0 };
        GunLevel = new int[6] { 1, 0, 0, 0, 0, 0 };
        LevelRank = new string[31];
        for (int i = 0; i < 31; i++)
        {
            LevelRank[i] = "";
        }
    }
}
