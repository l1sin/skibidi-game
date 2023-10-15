public class Collectible : BaseCollectible
{
    public override void OnCollect()
    {
        LevelController.OnPickUp();
        base.OnCollect();
    }
}
