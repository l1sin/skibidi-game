using Sounds;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public Transform LookPoint;
    public CharacterHealth CharacterHealth;
    public LevelController LevelController;
    public Transform Body;

    public float HealthMax;
    public float HealthCurrent;

    public NavMeshAgent Agent;
    public Transform Destination;

    public LayerMask ViewMask;
    public bool detected;

    public float AttackRange;
    public float Damage;

    public Animator Animator;
    public float HealthBuff = 1;
    public float SpeedBuff = 1;
    public float DamageBuff = 1;

    public bool IsAlive = true;

    public GameObject AudioSource;
    public AudioClip DeathSound;

    public float ViewDistance;

    public void Start()
    {
        Buff();
        HealthCurrent = HealthMax;
        detected = false;
        StartCoroutine(LookForPlayer());
        Animator.SetBool("IsAttacking", false);
    }

    public void Update()
    {
        if (detected && IsAlive)
        {
            NavMeshFollow();
            Attack();
        }
    }

    public IEnumerator LookForPlayer()
    {
        yield return new WaitForSeconds(1);
        if (Physics.Raycast(LookPoint.position, Destination.position - LookPoint.position, out RaycastHit hitInfo, ViewDistance, ViewMask))
        {
            if (hitInfo.transform.gameObject.layer == 7)
            {
                detected = true;
            }
            else
            {
                StartCoroutine(LookForPlayer());
            }
        }
        else
        {
            StartCoroutine(LookForPlayer());
        }
    }

    public void NavMeshFollow()
    {
        Agent.destination = Destination.position;
    }

    public void GetDamage(float damage)
    {
        if (IsAlive)
        {
            HealthCurrent -= damage;
            if (HealthCurrent <= 0)
            {
                IsAlive = false;
                Die();
            }
        }
    }
    public void Die()
    {
        LevelController.OnEnemyKilled();
        Animator.SetTrigger("Death");
        Destroy(Agent);
        SoundManager.Instance.PlaySound(DeathSound);
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }

    public void Attack()
    {
        if ((Destination.position - LookPoint.transform.position).magnitude < AttackRange)
        {
            CharacterHealth.GetDamage(Damage * Time.deltaTime);
            Animator.SetBool("IsAttacking", true);
            AudioSource.SetActive(true);
        }
        else
        {
            Animator.SetBool("IsAttacking", false);
            AudioSource.SetActive(false);
        }
    }

    public void Buff()
    {
        int difficulty = SaveManager.Instance.CurrentLevelDifficulty;
        HealthMax *= 1 + difficulty * HealthBuff;
        Damage *= 1 + difficulty * DamageBuff;
        Agent.speed *= 1 + difficulty * SpeedBuff;
    }
}
