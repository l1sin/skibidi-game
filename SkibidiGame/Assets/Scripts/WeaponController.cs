using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject[] Weapons;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (GameObject w in Weapons)
            {
                w.SetActive(false);
            }
            Weapons[0].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (GameObject w in Weapons)
            {
                w.SetActive(false);
            }
            Weapons[1].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (GameObject w in Weapons)
            {
                w.SetActive(false);
            }
            Weapons[2].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            foreach (GameObject w in Weapons)
            {
                w.SetActive(false);
            }
            Weapons[3].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            foreach (GameObject w in Weapons)
            {
                w.SetActive(false);
            }
            Weapons[4].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            foreach (GameObject w in Weapons)
            {
                w.SetActive(false);
            }
            Weapons[5].SetActive(true);
        }
    }
}
