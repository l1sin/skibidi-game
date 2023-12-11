using Sounds;
using UnityEngine;

public class AdButton : MonoBehaviour
{
    public void ShowAdDouble()
    {
        SoundManager.Instance.AudioMixerSetFloat("VolumeMaster", -80);
#if UNITY_EDITOR
        Debug.Log("Rewarded ad double");
#elif UNITY_WEBGL
        Yandex.WatchAdDouble();
#endif
        gameObject.SetActive(false);
    }

    public void ShowAdAdd()
    {
        SoundManager.Instance.AudioMixerSetFloat("VolumeMaster", -80);
#if UNITY_EDITOR
        Debug.Log("Rewarded ad add");
#elif UNITY_WEBGL
        Yandex.WatchAdAdd();
#endif
        gameObject.SetActive(false);
    }
}
