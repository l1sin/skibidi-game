using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour, IDamageable
{
    public float HealthMax;
    public float HealthCurrent;
    public float HealthBonus = 0.25f;
    public int HealthLevel = 0;

    public Image HPBar;
    public LevelController LevelController;
    public bool NoDamage = true;

    public void Start()
    {
        SetHealth();
        SetHPBar();
    }

    public void SetHealth()
    {
        HealthMax *= 1 + HealthLevel * HealthBonus;
        HealthCurrent = HealthMax;
    }

    public void SetHPBar()
    {
        HPBar.fillAmount = HealthCurrent / HealthMax;
    }

    public void GetDamage(float damage)
    {
        HealthCurrent -= damage;
        SetHPBar();
        if (NoDamage)
        {
            NoDamage = false;
            LevelController.FailNoDamage();
        }
        if (HealthCurrent <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("Dead");
    }
}
