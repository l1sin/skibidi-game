using UnityEngine;

public class BaseCollectible : MonoBehaviour
{
    public LevelController LevelController;
    public float RotationSpeed;

    public virtual void OnCollect()
    {
        Destroy(gameObject);
    }

    public void Update()
    {
        transform.Rotate(new Vector3(0, RotationSpeed, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            OnCollect();
        }
    }

}
