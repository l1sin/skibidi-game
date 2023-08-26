using UnityEngine;

public class TestAnim : MonoBehaviour
{
    public Animator Animator;
    public Transform Gun;
    public Camera Camera;

    public Transform DefaultPos;
    public Transform AimingPos;
    public Transform CurrentPos;
    

    public float DefaultFOV;
    public float AimingFOV;
    public float CurrentFOV;

    public float LerpValue;

    public bool CanAim;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !Animator.GetCurrentAnimatorStateInfo(0).IsName("PistolShot"))
        {
            Animator.SetTrigger("Shoot");
        }
        if (CanAim) Aim();
        if (Input.GetKey(KeyCode.W))
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
