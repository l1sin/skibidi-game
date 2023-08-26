using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private void LateUpdate()
    {
        transform.position = _target.position;
    }
}
