using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform FollowTarget;
    public Transform LookTarget;
    public CharacterController Controller;
    public Transform Body;
    public Transform Head;
    public float Speed;

    public void Update()
    {
        Follow();
        LookAtPlayer();
    }

    public void Follow()
    {
        Vector3 move = (FollowTarget.position - transform.position).normalized;
        move.y = 0;
        Controller.Move(move * Speed * Time.deltaTime);
        float RotationY = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
        Body.localRotation = Quaternion.Euler(0, RotationY, 0);
    }

    public void LookAtPlayer()
    {
        Head.LookAt(LookTarget);
    }
}
