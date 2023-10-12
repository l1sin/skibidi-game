using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBlock : MonoBehaviour
{
    public GameObject[] Lockers;

    public void UnlockLockers(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Lockers[i].SetActive(false);
        }
    }
}
