using Sounds;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Glitch : EnemyBase, IDamageable
{
    [SerializeField] private Transform[] _points;
    public int CurrentPoint;
    public Vector3 LastPoint;
    [SerializeField] private Vector3 _path;
    [SerializeField] private float _afterImageCount;
    [SerializeField] private float _lifeTime;
    [SerializeField] private AudioClip _sound;
    public Transform LookTarget;
    public GameObject AfterImage;

    public float DashDelay;
    public float DashTimer;

    public float Speed;

    public GameObject HealthBar;
    public GameObject Finish;

    public bool IsAlive;
    public float HealthCurrent;
    public float HealthMax;
    public AudioClip DeathSound;
    public AudioMixerGroup AudioMixerGroup;
    public LevelController LevelController;
    public Animator Animator;
    public Image HPBar;
    public CharacterHealth CharacterHealth;
    public GameObject AudioSource;

    public float AttackRange;
    public float StopDistance;
    public float Damage;
    public float HealthBuff;
    public float DamageBuff;
    public float SpeedBuff;

    private void Start()
    {
        Buff();
        HealthCurrent = HealthMax;
        DashTimer = DashDelay;
    }
    private void Update()
    {
        if (IsAlive)
        {
            transform.LookAt(LookTarget);
            DashTimer -= Time.deltaTime;
            Vector3 direction = LookTarget.position - transform.position;
            if (direction.magnitude > StopDistance)
            {
                transform.Translate(direction.normalized * Speed * Time.deltaTime, Space.World);
            }
            if (DashTimer <= 0)
            {
                Dash();
                DashTimer = DashDelay;
            }
            Attack();
        } 
        
    }

    public void Dash()
    {
        if (IsAlive)
        {
            Move();
            CreateAfterImage();
        } 
    }


    private void Move()
    {
        LastPoint = transform.position;
        CurrentPoint = Random.Range(0, _points.Length);
        transform.position = _points[CurrentPoint].position;
        SoundManager.Instance.PlaySound(_sound, AudioMixerGroup);
    }

    private void CreateAfterImage()
    {
        _path = transform.position - LastPoint;
        for (int i = 0; i < _afterImageCount; i++)
        {
            InstantiateAfterImage(_path * (i / _afterImageCount), (i / _afterImageCount) * _lifeTime);
        }
    }

    private void InstantiateAfterImage(Vector3 position, float lifeTime)
    {
        var afterImage = Instantiate(AfterImage, LastPoint + position, transform.rotation);
        StartCoroutine(DestroyThing(afterImage, lifeTime));
    }

    private IEnumerator DestroyThing(GameObject thing, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(thing);
    }

    public void GetDamage(float damage)
    {
        if (IsAlive)
        {
            HealthCurrent -= damage;
            HPBar.fillAmount = HealthCurrent / HealthMax;
            if (HealthCurrent <= 0)
            {
                IsAlive = false;
                Die();
            }
        }
    }

    public void Die()
    {
        LevelController.OnEnemyKilled();
        Animator.SetTrigger("Death");
        SoundManager.Instance.PlaySound(DeathSound, AudioMixerGroup);
    }

    public void Attack()
    {
        if ((LookTarget.position - transform.position).magnitude < AttackRange)
        {
            CharacterHealth.GetDamage(Damage * Time.deltaTime);
            Animator.SetBool("IsAttacking", true);
            AudioSource.SetActive(true);
        }
        else
        {
            Animator.SetBool("IsAttacking", false);
            AudioSource.SetActive(false);
        }
    }

    public void OnDeathAnimationEnd()
    {
        HealthBar.SetActive(false);
        Finish.SetActive(true);
        Destroy(gameObject);
    }

    public void Buff()
    {
        int difficulty = SaveManager.Instance.CurrentLevelDifficulty;
        HealthMax *= 1 + difficulty * HealthBuff;
        Damage *= 1 + difficulty * DamageBuff;
        Speed *= 1 + difficulty * SpeedBuff;
    }
}
