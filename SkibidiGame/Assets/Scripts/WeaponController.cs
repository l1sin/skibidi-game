using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    public GameObject[] Weapons;
    public Gun[] AllGuns;
    public Image[] GunIcons;
    public Color DefaultColor;
    public Color SelectedColor;
    public Gun CurrentGun;
    public TextMeshProUGUI AmmoText;

    public void Awake()
    {
        ChangeGun(0);
        ChangeIcon(0);
    }
    public void Update()
    {
        if (CurrentGun.CanSwitch)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && CurrentGun != AllGuns[0])
            {
                ChangeGun(0);
                ChangeIcon(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && CurrentGun != AllGuns[1])
            {
                ChangeGun(1);
                ChangeIcon(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && CurrentGun != AllGuns[2])
            {
                ChangeGun(2);
                ChangeIcon(2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && CurrentGun != AllGuns[3])
            {
                ChangeGun(3);
                ChangeIcon(3);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5) && CurrentGun != AllGuns[4])
            {
                ChangeGun(4);
                ChangeIcon(4);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6) && CurrentGun != AllGuns[5])
            {
                ChangeGun(5);
                ChangeIcon(5);
            }
        } 
    }

    public void ChangeGun(int index)
    {
        foreach (GameObject w in Weapons)
        {
            w.SetActive(false);
        }
        Weapons[index].SetActive(true);
        CurrentGun = Weapons[index].GetComponent<Gun>();
        UpdateAmmoText(CurrentGun.Ammo);
    }

    public void ChangeIcon(int index)
    {
        foreach (Image i in GunIcons)
        {
            i.color = DefaultColor;
        }
        GunIcons[index].color = SelectedColor;
    }
    public void UpdateAmmoText(float ammoAmount)
    {
        AmmoText.text = string.Format("{0:f0}", ammoAmount);
    }
}
