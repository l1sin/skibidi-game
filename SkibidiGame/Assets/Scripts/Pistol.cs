using Sounds;
using UnityEngine;
using UnityEngine.VFX;

public class Pistol : MonoBehaviour
{
    public CharacterMovement _CharacterMovement;
    public Animator Animator;
    public Transform Gun;
    public Camera Camera;
    public VisualEffect VFX;

    public Transform DefaultPos;
    public Transform AimingPos;
    public Transform CurrentPos;

    public bool IsShooting;
   
    public AudioClip shotSound;
    public GameObject Particles;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !IsShooting)
        {
            Shoot();
        }

        if (CharacterInput.IsMoving && _CharacterMovement.IsGrounded)
        {
            Animator.SetBool("Walking", true);
        }
        else
        {
            Animator.SetBool("Walking", false);
        }
    }

    public void EndShooting()
    {
        IsShooting = false;
    }

    public void Shoot()
    {
        IsShooting = true;
        Animator.SetTrigger("Shoot");
        VFX.Play();
        SoundManager.Instance.PlaySound(shotSound);

        RaycastHit HitInfo;
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out HitInfo, 100.0f))
        {
            Transform objectHit = HitInfo.transform;
            GameObject particles = Instantiate(Particles, HitInfo.point, Quaternion.LookRotation(HitInfo.normal));
            Destroy(particles, 2);
            Debug.Log(objectHit.name);
        }
    }
}
