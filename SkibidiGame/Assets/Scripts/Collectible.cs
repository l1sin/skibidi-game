using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public LevelController LevelController;
    public float RotationSpeed;
    public void Update()
    {
        transform.Rotate(new Vector3(0, RotationSpeed, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            LevelController.OnPickUp();
            Destroy(gameObject);
        }
    }

}
