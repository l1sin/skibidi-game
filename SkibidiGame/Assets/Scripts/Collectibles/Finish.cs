public class Finish : BaseCollectible
{
    public override void OnCollect()
    {
        LevelController.FinishLevel();
        base.OnCollect();
    }
}
