using Sounds;
using UnityEngine;

public class Rifle : Gun
{
    public override void Shoot()
    {
        IsShooting = true;
        Animator.SetTrigger("Shoot");
        ShotVFX.Play();
        SoundManager.Instance.PlaySound(shotSound);

        RaycastHit HitInfo;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out HitInfo, 100.0f))
        {
            Transform objectHit = HitInfo.transform;
            GameObject particles = Instantiate(ImpactVFX, HitInfo.point, Quaternion.LookRotation(HitInfo.normal));
            particles.transform.SetParent(objectHit);
            Destroy(particles, 2);
            Debug.Log(objectHit.name);

            Enemy enemy = objectHit.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.GetDamage(Damage);
            }
        }
    }
}