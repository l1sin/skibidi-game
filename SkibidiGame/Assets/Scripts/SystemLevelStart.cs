using UnityEngine;

public class SystemLevelStart : MonoBehaviour
{
    private void Awake()
    {
        transform.DetachChildren();
    }
}
