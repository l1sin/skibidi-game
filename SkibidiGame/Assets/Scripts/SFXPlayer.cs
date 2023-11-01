using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private void Update()
    {
        if (_audioSource.isPlaying == false)
        {
            Destroy(gameObject);
        }
    }
}
