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
    public float Speed;

    public float HealthMax;
    public float HealthCurrent;

    public NavMeshAgent Agent;
    public Transform Destination;

    public LayerMask ViewMask;
    public bool detected;

    public float AttackRange;
    public float Damage;

    public Animator Animator;

    public bool IsAlive = true;

    public void Start()
    {
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
        if (Agent.remainingDistance <= AttackRange && Agent.remainingDistance != 0)
        {
            CharacterHealth.GetDamage(Damage * Time.deltaTime);
            Animator.SetBool("IsAttacking", true);
        }
        else
        {
            Animator.SetBool("IsAttacking", false);
        }
    }
}
