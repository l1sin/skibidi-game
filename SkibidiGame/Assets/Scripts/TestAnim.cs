using UnityEngine;
using UnityEngine.VFX;

public class TestAnim : MonoBehaviour
{
    public CharacterMovement _CharacterMovement;
    public Animator Animator;
    public Transform Gun;
    public Camera Camera;
    public VisualEffect VFX;

    public Transform DefaultPos;
    public Transform AimingPos;
    public Transform CurrentPos;
    

    public float DefaultFOV;
    public float AimingFOV;
    public float CurrentFOV;

    public float LerpValue;

    public bool CanAim;

    public float TimeScale;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !Animator.GetCurrentAnimatorStateInfo(0).IsName("PistolShot"))
        {
            Animator.SetTrigger("Shoot");
            VFX.Play();
            Time.timeScale = TimeScale;
        }
        else if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("PistolShot"))
        {
            Time.timeScale = 1;
        }

        if (CanAim) Aim();

        if (CharacterInput.IsMoving && _CharacterMovement.IsGrounded)
        {
            Animator.SetBool("Walking", true);
        }
        else
        {
            Animator.SetBool("Walking", false);
        }
    }

    public void Aim()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            CurrentPos = AimingPos;
            CurrentFOV = AimingFOV;
        }
        else
        {
            CurrentPos = DefaultPos;
            CurrentFOV = DefaultFOV;

        }
        Gun.position = Vector3.Lerp(Gun.position, CurrentPos.position, LerpValue);
        Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, CurrentFOV, LerpValue);
    }
}
