using Sounds;
using UnityEngine;
using UnityEngine.VFX;

public class Plasmagun : GunTap
{
    public float Radius;
    public GameObject PlasmaBeamVFX;
    public Transform ShootingPoint;
    public override void Shoot()
    {
        IsShooting = true;
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

            Debug.Log(HitInfo.distance);

            

            Collider[] targets = Physics.OverlapSphere(HitInfo.point, Radius, Targets);
            foreach (Collider target in targets)
            {
                Enemy enemy = target.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    enemy.GetDamage(Damage);
                }
            } 
        }
    }
}
