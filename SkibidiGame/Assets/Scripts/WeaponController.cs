using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject[] Weapons;
    public Gun[] AllGuns;
    public Gun CurrentGun;
    public void Update()
    {
        if (CurrentGun.CanSwitch)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && CurrentGun != AllGuns[0])
            {
                foreach (GameObject w in Weapons)
                {
                    w.SetActive(false);
                }
                Weapons[0].SetActive(true);
                CurrentGun = Weapons[0].GetComponent<Gun>();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && CurrentGun != AllGuns[1])
            {
                foreach (GameObject w in Weapons)
                {
                    w.SetActive(false);
                }
                Weapons[1].SetActive(true);
                CurrentGun = Weapons[1].GetComponent<Gun>();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && CurrentGun != AllGuns[2])
            {
                foreach (GameObject w in Weapons)
                {
                    w.SetActive(false);
                }
                Weapons[2].SetActive(true);
                CurrentGun = Weapons[2].GetComponent<Gun>();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && CurrentGun != AllGuns[3])
            {
                foreach (GameObject w in Weapons)
                {
                    w.SetActive(false);
                }
                Weapons[3].SetActive(true);
                CurrentGun = Weapons[3].GetComponent<Gun>();
            }

            if (Input.GetKeyDown(KeyCode.Alpha5) && CurrentGun != AllGuns[4])
            {
                foreach (GameObject w in Weapons)
                {
                    w.SetActive(false);
                }
                Weapons[4].SetActive(true);
                CurrentGun = Weapons[4].GetComponent<Gun>();
            }

            if (Input.GetKeyDown(KeyCode.Alpha6) && CurrentGun != AllGuns[5])
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
