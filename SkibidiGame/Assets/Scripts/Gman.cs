using Sounds;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Gman : EnemyBase, IDamageable
{
    public bool IsAlive;
    public float HealthMax;
    public float HealthCurrent;
    public float Damage;
    public Image HPBarImage;
    public GameObject HPBarObject;
    public LevelController LevelController;
    public Animator Animator;
    public AudioClip Explosion;
    public AudioMixerGroup AudioMixerGroup;
    public Transform LookTarget;
    public GameObject Finish;
    public GameObject LaserMain;
    public GameObject[] LaserPoints;
    public float AttackPeriod;
    public int AnimationState;
    public LayerMask AttackMask;
    public float LaserRadius;


    private void Start()
    {
        HealthCurrent = HealthMax;
        StartCoroutine(Attack());
    }

    private void Update()
    {
        if (IsAlive)
        {
            Laser();
        }
    }

    public void GetDamage(float damage)
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

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(AttackPeriod);
        if (IsAlive)
        {
            AnimationState = Random.Range(1, 8);
            Animator.SetInteger("AnimationState", AnimationState);
        }
    }

    public void Laser()
    {
        if (LaserMain.activeInHierarchy)
        {
            foreach (GameObject laser in LaserPoints)
            {
                Physics.SphereCast(laser.transform.position, LaserRadius, laser.transform.forward, out RaycastHit hitInfo, 200, AttackMask);

                if (hitInfo.collider != null)
                {
                    hitInfo.collider.GetComponent<IDamageable>().GetDamage(Damage * Time.deltaTime);
                }
            }
        }
    }

    public void Die()
    {
        LevelController.OnEnemyKilled();
        AnimationState = -1;
        Animator.SetInteger("AnimationState", AnimationState);
    }

    public void OnDeathAnimationEnd()
    {
        HPBarObject.SetActive(false);
        Finish.SetActive(true);
        SoundManager.Instance.PlaySound(Explosion, AudioMixerGroup);
        Destroy(gameObject);
    }

    public void AttackAnimationEnd()
    {
        AnimationState = 0;
        Animator.SetInteger("AnimationState", AnimationState);
        StartCoroutine(Attack());
    }
}
