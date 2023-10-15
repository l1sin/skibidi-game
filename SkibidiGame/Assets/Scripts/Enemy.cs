using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public Transform LookPoint;
    public Transform LookTarget;
    public CharacterHealth CharacterHealth;
    public LevelController LevelController;
    public Transform Body;
    public Transform Head;

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
        if (detected)
        {
            LookAtPlayer();
            NavMeshFollow();
            Attack();
        }
    }

    public IEnumerator LookForPlayer()
    {
        yield return new WaitForSeconds(1);
        if (Physics.Raycast(LookPoint.position, Destination.position - LookPoint.position, out RaycastHit hitInfo,  100, ViewMask))
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

    public void LookAtPlayer()
    {
        Head.LookAt(LookTarget);
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
        Destroy(gameObject);
    }

    public void Attack()
    {
        if ((Destination.position - LookPoint.transform.position).magnitude < AttackRange)
        {
            CharacterHealth.GetDamage(Damage * Time.deltaTime);
            Animator.SetBool("IsAttacking", true);
        }
        else
        {
            Animator.SetBool("IsAttacking", false);
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
