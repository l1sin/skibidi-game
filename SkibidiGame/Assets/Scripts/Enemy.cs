using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform LookTarget;
    public Transform Body;
    public Transform Head;
    public float Speed;

    public float HealthMax;
    public float HealthCurrent;

    public NavMeshAgent Agent;
    public Transform Destination;


    public void Awake()
    {
        HealthCurrent = HealthMax;
    }

    public void Update()
    {
        LookAtPlayer();
        NavMeshFollow();
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
        HealthCurrent -= damage;
        if (HealthCurrent <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
