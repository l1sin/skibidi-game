using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private CharacterController _characterController;
    private Vector3 _movement;
    private Vector3 _velocity;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private bool _isGrounded;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _whatIsGround);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
        }

        float x = PlayerInput.MoveInputX;
        float z = PlayerInput.MoveInputY;
        _movement = transform.right * x + transform.forward * z;
        _characterController.Move(_movement * _speed * Time.unscaledDeltaTime);
        

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
        }

        _velocity.y += Physics.gravity.y * Time.unscaledDeltaTime;
        _characterController.Move(_velocity * Time.unscaledDeltaTime);
    }
}
