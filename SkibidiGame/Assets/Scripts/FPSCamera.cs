using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private float _mouseSensitivityX;
    [SerializeField] private float _mouseSensitivityY;

    private float _verticalRotation = 0f;
    private float _horizontalRotation;

    private void Start()
    {
        LockAndHideCursor();
    }
    private void Update()
    {
        RotateVertical();
        RotateHorizontal();
    }

    private void LockAndHideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void RotateVertical()
    {
        _verticalRotation -= PlayerInput.MouseInputY * _mouseSensitivityY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(_verticalRotation, 0f,0f);
    }

    private void RotateHorizontal()
    {
        _horizontalRotation = PlayerInput.MouseInputX * _mouseSensitivityX;
        _player.Rotate(Vector3.up * _horizontalRotation);
    }
}