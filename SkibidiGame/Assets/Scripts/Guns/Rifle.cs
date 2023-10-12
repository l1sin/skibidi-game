using Sounds;
using UnityEngine;

public class Rifle : GunTap
{
    public override void Shoot()
    {
        if (Ammo > 0)
        {
            UpdateAmmo(1);
            IsShooting = true;
            CanSwitch = false;
            Animator.SetTrigger("Shoot");
            ShotVFX.Play();
            SoundManager.Instance.PlaySound(shotSound);

            RaycastHit HitInfo;
            if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out HitInfo, 100.0f, Targets))
            {
                Transform objectHit = HitInfo.transform;
                GameObject particles = Instantiate(ImpactVFX, HitInfo.point, Quaternion.LookRotation(HitInfo.normal));
                particles.transform.SetParent(objectHit);
                Destroy(particles, 5);

                Enemy enemy = objectHit.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    enemy.GetDamage(Damage);
                }
            }
        }
        else
        {
            EndShooting();
        }
    }
}