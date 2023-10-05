using Sounds;
using UnityEngine;
using UnityEngine.VFX;

public class Minigun : GunHold
{
    public VisualEffect ShotVFX;
    public AudioClip shotSound;
    public GameObject ImpactVFX;

    public void Fire()
    {
        ShotVFX.Play();
        SoundManager.Instance.PlaySound(shotSound);

        RaycastHit HitInfo;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out HitInfo, 100.0f, Targets))
        {
            Transform objectHit = HitInfo.transform;
            GameObject particles = Instantiate(ImpactVFX, HitInfo.point, Quaternion.LookRotation(HitInfo.normal));
            particles.transform.SetParent(objectHit);
            Destroy(particles, 5);
            Debug.Log(objectHit.name);

            Enemy enemy = objectHit.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.GetDamage(Damage);
            }
        }
    }
}
