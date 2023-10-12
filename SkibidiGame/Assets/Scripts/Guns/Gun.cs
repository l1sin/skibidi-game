using UnityEngine;

public class Gun : MonoBehaviour
{
    public WeaponController WeaponController;
    public CharacterMovement _CharacterMovement;
    public Animator Animator;
    public Transform GunObject;
    public Camera Camera;
    public LayerMask Targets;
    public bool CanSwitch = true;
    public float Damage;
    public bool IsShooting;
    public float StartAmmo;
    public float Ammo;
    public float GunLevel;
    public float AnimationSpeedModifyer;

    public void Awake()
    {
        Ammo = StartAmmo;
        Damage *= 1 + (GunLevel - 1) * 0.1f;
        AnimationSpeedModifyer *= 1 + (GunLevel - 1) * 0.1f;
        Animator.SetFloat("AnimationSpeedModifyer", AnimationSpeedModifyer);
    }

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

    public virtual void EndShooting()
    {
        IsShooting = false;
        CanSwitch = true;
    }

    public virtual void UpdateAmmo(float ammoConsumption)
    {
        Ammo -= ammoConsumption;
        WeaponController.UpdateAmmoText(Ammo);
    }
}
