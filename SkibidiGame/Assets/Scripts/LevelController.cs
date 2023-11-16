using Sounds;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class LevelController : MonoBehaviour
{
    public float TimeInSeconds;
    public TextMeshProUGUI Timer;
    public TextMeshProUGUI[] ObjectivesTexts;
    public Objective[] Objectives;

    public List<EnemyBase> Enemies;
    public List<BaseCollectible> Collectibles;

    public float FastTime;
    public bool NoDamage = true;
    public bool InTime = true;

    public int TotalCollectibles;
    public int CollectedCollectibles;
    public int TotalEnemies;
    public int KilledEnemies;

    public bool LevelFinished;

    public GameObject[] TurnOffOnLevelFinished;
    public GameObject LevelFinishedMenu;
    public GameObject DeadMenu;

    public CharacterInput CharacterInput;

    public int RewardPerObjective = 100;

    public PauseManager PauseManager;

    public AudioClip WinSound;
    public AudioClip FailSound;
    public AudioMixerGroup AudioMixerGroup;
    public GameObject ObjecivesObject;

    public void Start()
    {
        TotalCollectibles = Collectibles.Count;
        TotalEnemies = Enemies.Count;

        Timer.text = GetTimerText(TimeInSeconds);
        SetObjectives();
    }

    public void Update()
    {
        TimeInSeconds += Time.deltaTime;
        if (TimeInSeconds > FastTime && InTime) FailInTime(); 
        Timer.text = GetTimerText(TimeInSeconds);
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleObjectives();
        }
    }

    public void ToggleObjectives()
    {
        if (ObjecivesObject.activeSelf) ObjecivesObject.SetActive(false);
        else ObjecivesObject.SetActive(true);
    }

    public string GetTimerText(float time)
    {
        int seconds = Mathf.FloorToInt(time);
        int minutes = seconds / 60;
        seconds %= 60;
        string text;
        if (seconds >= 10)
        {
            text = $"{minutes}:{seconds}";
        }
        else
        {
            text = $"{minutes}:0{seconds}";
        }
        return text;
    }

    public void SetObjectives()
    {
        Objectives = new Objective[4];
        for (int i = 0; i < 4; i++)
        {
            Objectives[i] = new Objective();
            Objectives[i].Text = ObjectivesTexts[i];
        }

        Objectives[0].Text.font = SaveManager.Instance.CurrentFont;
        Objectives[0].Text.text = $"{SaveManager.Instance.Localization[19]} {GetTimerText(FastTime)}";
        Objectives[0].CompleteObjective();

        Objectives[1].Text.font = SaveManager.Instance.CurrentFont;
        Objectives[1].Text.text = $"{SaveManager.Instance.Localization[20]}";
        Objectives[1].CompleteObjective();

        Objectives[2].Text.font = SaveManager.Instance.CurrentFont;
        Objectives[2].Text.text = $"{SaveManager.Instance.Localization[21]} {CollectedCollectibles}/{TotalCollectibles}";
        Objectives[2].FailObjective();

        Objectives[3].Text.font = SaveManager.Instance.CurrentFont;
        Objectives[3].Text.text = $"{SaveManager.Instance.Localization[22]} {KilledEnemies}/{TotalEnemies}";
        Objectives[3].FailObjective();
    }

    public void FailInTime()
    {
        InTime = false;
        Objectives[0].FailObjective();
    }

    public void FailNoDamage()
    {
        NoDamage = false;
        Objectives[1].FailObjective();
    }

    public void OnPickUp()
    {
        CollectedCollectibles++;
        Objectives[2].Text.text = $"{SaveManager.Instance.Localization[21]} {CollectedCollectibles}/{TotalCollectibles}";
        if (CollectedCollectibles >= TotalCollectibles)
        {
            Objectives[2].CompleteObjective();
        }
    }

    public void OnEnemyKilled()
    {
        KilledEnemies++;
        Objectives[3].Text.text = $"{SaveManager.Instance.Localization[22]} {KilledEnemies}/{TotalEnemies}";
        if (KilledEnemies >= TotalEnemies)
        {
            Objectives[3].CompleteObjective();
        }
    }

    public void FinishLevel()
    {
        PauseManager.Pause();
        foreach (GameObject go in TurnOffOnLevelFinished)
        {
            go.SetActive(false);
        }
        LevelFinishedMenu.SetActive(true);
        LevelFinished = true;
    }

    public void FailLevel()
    {
        PauseManager.Pause();
        SoundManager.Instance.PlaySound(FailSound, AudioMixerGroup);
        foreach (GameObject go in TurnOffOnLevelFinished)
        {
            go.SetActive(false);
        }
        DeadMenu.SetActive(true);
    }


    public class Objective
    {
        public bool IsCompleted;
        public string ObjectiveText;
        public TextMeshProUGUI Text;

        public void CompleteObjective()
        {
            IsCompleted = true;
            Text.color = Color.green;
        }

        public void FailObjective()
        {
            IsCompleted = false;
            Text.color = Color.red;
        }
    }
}
