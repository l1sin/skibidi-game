using Sounds;
using UnityEngine;
using UnityEngine.Audio;

public class Collectible : BaseCollectible
{
    public AudioClip PickupSound;
    public AudioMixerGroup AudioMixerGroup;
    public override void OnCollect()
    {
        LevelController.OnPickUp();
        SoundManager.Instance.PlaySound(PickupSound, AudioMixerGroup);
        base.OnCollect();
    }
}
