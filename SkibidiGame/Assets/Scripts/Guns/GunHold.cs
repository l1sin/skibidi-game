using UnityEngine;

public class GunHold : Gun
{
    public void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            IsShooting = true;
            CanSwitch = false;
            Animator.SetBool("IsShooting", IsShooting);
        }
        else
        {
            Animator.SetBool("IsShooting", IsShooting);
        }
        Walk();
    }
}
