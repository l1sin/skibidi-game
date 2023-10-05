using UnityEngine;

public class Gun : MonoBehaviour
{
    public CharacterMovement _CharacterMovement;
    public Animator Animator;
    public Transform GunObject;
    public Camera Camera;
    public LayerMask Targets;
    public bool CanSwitch = true;
    public float Damage;
    public bool IsShooting;

    public void Walk()
    {
        if (CharacterInput.IsMoving && _CharacterMovement.IsGrounded)
        {
            Animator.SetBool("Walking", true);
        }
        else
        {
            Animator.SetBool("Walking", false);
        }
    }

    public void EndShooting()
    {
        IsShooting = false;
        CanSwitch = true;
    }
}
