using UnityEngine;
using UnityEngine.UI;

public class ToiletBoss : Enemy
{
    public Image HPBarImage;
    public GameObject HPBarObject;
    public GameObject Finish;
    public override void GetDamage(float damage)
    {
        if (IsAlive)
        {
            HealthCurrent -= damage;
            HPBarImage.fillAmount = HealthCurrent / HealthMax;
            if (HealthCurrent <= 0)
            {
                IsAlive = false;
                Die();
            }
        }
    }

    public override void OnDeathAnimationEnd()
    {
        HPBarObject.SetActive(false);
        Finish.SetActive(true);
        base.OnDeathAnimationEnd();
    }
}
