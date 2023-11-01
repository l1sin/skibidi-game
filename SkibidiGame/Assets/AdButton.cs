using UnityEngine;

public class AdButton : MonoBehaviour
{
    public void ShowAdOnClick()
    {
#if UNITY_EDITOR
        Debug.Log("Rewarded ad");
#elif UNITY_WEBGL
        Yandex.WatchAd();
#endif
        gameObject.SetActive(false);
    }
}
