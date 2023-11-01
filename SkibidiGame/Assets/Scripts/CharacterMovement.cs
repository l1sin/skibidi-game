using Sounds;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float Speed;
    public float SpeedBonus;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private CharacterController _characterController;
    private Vector3 _movement;
    public Vector3 _velocity;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _whatIsGround;
    public bool IsGrounded;

    public AudioClip[] StepSounds;
    public AudioClip[] JumpSounds;
    public AudioClip[] LandSounds;
    public Animator Animator;

    public void Start()
    {
        SetSpeedLevel(SaveManager.Instance.CurrentProgress.UpgradeLevel[1]);
    }

    private void Update()
    {
        Move();
        ApplyGravity();
    }

    public void MakeStepSound()
    {
        SoundManager.Instance.PlaySoundRandom(StepSounds);
    }

    public void SetSpeedLevel(int SpeedLevel)
    {
        Speed *= 1 + SpeedLevel * SpeedBonus;
    }

    private void Move()
    {
        if (!IsGrounded)
        {
            IsGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _whatIsGround);
            if (IsGrounded) SoundManager.Instance.PlaySoundRandom(LandSounds);
        }
        else
        {
            IsGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _whatIsGround);
            if (!IsGrounded) SoundManager.Instance.PlaySoundRandom(JumpSounds);
        }

        float x = CharacterInput.MoveInputX;
        float z = CharacterInput.MoveInputY;
        if ((x != 0 || z != 0) && IsGrounded)
        {
            Animator.SetBool("IsRunning", true);
        }
        else
        {
            Animator.SetBool("IsRunning", false);
        }
        _movement = (transform.right * x + transform.forward * z).normalized;
        _characterController.Move(_movement * Speed * Time.deltaTime);
        

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
        }     
    }

    public void ApplyGravity()
    {
        if (!_characterController.isGrounded && !IsGrounded)
        {
            _velocity.y += Physics.gravity.y * Time.deltaTime;
        }
        if (_characterController.isGrounded && IsGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
        }
        _characterController.Move(_velocity * Time.deltaTime);
    }
}
