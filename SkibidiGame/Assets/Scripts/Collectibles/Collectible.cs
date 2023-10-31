using Sounds;
using UnityEngine;

public class Collectible : BaseCollectible
{
    public AudioClip PickupSound;
    public override void OnCollect()
    {
        LevelController.OnPickUp();
        SoundManager.Instance.PlaySound(PickupSound);
        base.OnCollect();
    }
}
