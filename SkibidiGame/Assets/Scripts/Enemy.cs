using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform LookPoint;
    public Transform LookTarget;
    public Transform Body;
    public Transform Head;
    public float Speed;

    public float HealthMax;
    public float HealthCurrent;

    public NavMeshAgent Agent;
    public Transform Destination;

    public LayerMask ViewMask;
    public bool detected;


    public void Awake()
    {
        HealthCurrent = HealthMax;
        detected = false;
        StartCoroutine(LookForPlayer());
    }

    public void Update()
    {
        if (detected)
        {
            LookAtPlayer();
            NavMeshFollow();
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
