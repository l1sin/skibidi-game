using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private CharacterController _characterController;
    private Vector3 _movement;
    private Vector3 _velocity;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _whatIsGround;
    public bool IsGrounded;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        IsGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _whatIsGround);

        if (IsGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
        }

        float x = CharacterInput.MoveInputX;
        float z = CharacterInput.MoveInputY;
        _movement = transform.right * x + transform.forward * z;
        _characterController.Move(_movement * _speed * Time.unscaledDeltaTime);
        

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
        }

        _velocity.y += Physics.gravity.y * Time.unscaledDeltaTime;
        _characterController.Move(_velocity * Time.unscaledDeltaTime);
    }
}
