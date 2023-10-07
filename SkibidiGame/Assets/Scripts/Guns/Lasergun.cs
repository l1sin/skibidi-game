using UnityEngine;

public class Lasergun : GunHold
{
    public GameObject LaserBeam;
    public LineRenderer Line;
    public GameObject ImpactVFX;
    public AudioSource LaserSFX;



    public override void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }
        else
        {
            EndShooting();
        }
        Walk();
    }

    public void Shoot()
    {
        if (!IsShooting) LaserSFX.Play();
        IsShooting = true;
        CanSwitch = false;
        LaserBeam.SetActive(true);
        ImpactVFX.SetActive(true);
        Animator.SetBool("IsShooting", IsShooting);
        Fire();
        //SoundManager.Instance.PlaySound(shotSound);
    }

    public void Fire()
    {
        RaycastHit HitInfo;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out HitInfo, 100.0f, Targets))
        {
            Transform objectHit = HitInfo.transform;
            Line.SetPosition(1, Line.transform.InverseTransformPoint(HitInfo.point));
            ImpactVFX.transform.position = HitInfo.point;
            Debug.Log(HitInfo.point);

            Enemy enemy = objectHit.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.GetDamage(Damage * Time.deltaTime);
            }
        }
    }

    public override void EndShooting()
    {
        base.EndShooting();
        LaserSFX.Stop();
        LaserBeam.SetActive(false);
        ImpactVFX.SetActive(false);
        Animator.SetBool("IsShooting", IsShooting);
    }
}
