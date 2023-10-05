using UnityEngine;
using UnityEngine.VFX;

public class GunTap : Gun
{
    public VisualEffect ShotVFX;
    public AudioClip shotSound;
    public GameObject ImpactVFX;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !IsShooting)
        {
            Shoot();
            CanSwitch = false;
        }

        Walk();
    }

    public virtual void Shoot() { }
}
