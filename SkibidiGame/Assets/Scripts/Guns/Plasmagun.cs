using Sounds;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Plasmagun : GunTap
{
    public float Radius;
    public GameObject PlasmaBeamVFX;
    public Transform ShootingPoint;
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
                GameObject beamObj = Instantiate(PlasmaBeamVFX, ShootingPoint.position, Quaternion.identity);
                beamObj.transform.LookAt(HitInfo.point);
                beamObj.transform.localScale = new Vector3(1, 1, HitInfo.distance);
                Destroy(beamObj, 5);

                GameObject particles = Instantiate(ImpactVFX, HitInfo.point, Quaternion.LookRotation(HitInfo.normal));
                VisualEffect visualEffect = particles.GetComponent<VisualEffect>();
                visualEffect.SetFloat("Size", Radius);
                Destroy(particles, 5);

                Collider[] targets = Physics.OverlapSphere(HitInfo.point, Radius, Targets);
                HashSet <Enemy> enemies = new HashSet<Enemy>();

                foreach (Collider target in targets)
                {
                    enemies.Add(target.GetComponentInParent<Enemy>());
                }
                foreach (Enemy enemy in enemies)
                {
                    if (enemy != null)
                    {
                        enemy.GetDamage(Damage);
                    }
                }
            }
            else
            {
                GameObject beamObj = Instantiate(PlasmaBeamVFX, ShootingPoint.position, Quaternion.identity);
                beamObj.transform.LookAt(Camera.transform.position + Camera.transform.forward * 20f);
                beamObj.transform.localScale = new Vector3(1, 1, 20);
                Destroy(beamObj, 5);
            }
        }
        else
        {
            EndShooting();
        }
    }
}
