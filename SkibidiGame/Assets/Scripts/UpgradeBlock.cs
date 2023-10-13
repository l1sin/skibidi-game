using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Icons;

public class UpgradeBlock : MonoBehaviour
{
    public GameObject[] Lockers;

    public MainMenuController MainMenuController;
    public Button BuyOneButton;
    public TextMeshProUGUI BuyOneButtonText;
    public int[,] Prices;
    public Button BuyAllButton;
    public TextMeshProUGUI BuyAllButtonText;
    public int ItemIndex;
    public ItemType _ItemType;
    public enum ItemType
    {
        Upgrade,
        Gun
    }

    public void OnEnable()
    {
        BuyOneButton.onClick.AddListener(BuyOneOnClick);
        BuyAllButton.onClick.AddListener(BuyAllOnClick);
    }

    public void OnDisable()
    {
        BuyOneButton.onClick.RemoveAllListeners();
        BuyAllButton.onClick.RemoveAllListeners();
    }

    public void CheckPrice()
    {

    }

    public void CheckMoney()
    {

    }

    public void CheckIfMaxLevel()
    {
        if (_ItemType == ItemType.Upgrade && MainMenuController.UpgradeLevel[ItemIndex] >= 5)
        {
            BuyOneButton.interactable = false;
            BuyAllButton.interactable = false;
        }
        else if (_ItemType == ItemType.Gun && MainMenuController.GunLevel[ItemIndex] >= 5)
        {
            BuyOneButton.interactable = false;
            BuyAllButton.interactable = false;
        }
    }

    public void BuyOneOnClick()
    {
        if (_ItemType == ItemType.Upgrade)
        {
            UnlockLockers(++MainMenuController.UpgradeLevel[ItemIndex]);
        }
        else if (_ItemType == ItemType.Gun)
        {
            UnlockLockers(++MainMenuController.GunLevel[ItemIndex]);
        }
        CheckIfMaxLevel();
    }

    public void BuyAllOnClick()
    {
        if (_ItemType == ItemType.Upgrade)
        {
            MainMenuController.UpgradeLevel[ItemIndex] = 5;
        }
        else if (_ItemType == ItemType.Gun)
        {
            MainMenuController.GunLevel[ItemIndex] = 5;
        }
        UnlockLockers(5);
        CheckIfMaxLevel();
    }


    public void UnlockLockers(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Lockers[i].SetActive(false);
        }
    }
}
