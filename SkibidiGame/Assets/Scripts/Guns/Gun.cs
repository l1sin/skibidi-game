using UnityEngine;
using UnityEngine.VFX;

public class Gun : MonoBehaviour
{
    public CharacterMovement _CharacterMovement;
    public Animator Animator;
    public Transform GunObject;
    public Camera Camera;
    public VisualEffect ShotVFX;

    public bool IsShooting;
   
    public AudioClip shotSound;
    public GameObject ImpactVFX;

    public float Damage;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !IsShooting)
        {
            Shoot();
        }

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
    }

    public virtual void Shoot() { }
}
