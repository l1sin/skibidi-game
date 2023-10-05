using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject[] Weapons;
    public Gun CurrentGun;
    public void Update()
    {
        if (CurrentGun.CanSwitch)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                foreach (GameObject w in Weapons)
                {
                    w.SetActive(false);
                }
                Weapons[0].SetActive(true);
                CurrentGun = Weapons[0].GetComponent<Gun>();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                foreach (GameObject w in Weapons)
                {
                    w.SetActive(false);
                }
                Weapons[1].SetActive(true);
                CurrentGun = Weapons[1].GetComponent<Gun>();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                foreach (GameObject w in Weapons)
                {
                    w.SetActive(false);
                }
                Weapons[2].SetActive(true);
                CurrentGun = Weapons[2].GetComponent<Gun>();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                foreach (GameObject w in Weapons)
                {
                    w.SetActive(false);
                }
                Weapons[3].SetActive(true);
                CurrentGun = Weapons[3].GetComponent<Gun>();
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                foreach (GameObject w in Weapons)
                {
                    w.SetActive(false);
                }
                Weapons[4].SetActive(true);
                CurrentGun = Weapons[4].GetComponent<Gun>();
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                foreach (GameObject w in Weapons)
                {
                    w.SetActive(false);
                }
                Weapons[5].SetActive(true);
                CurrentGun = Weapons[5].GetComponent<Gun>();
            }
        } 
    }
}
