using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public int Price;
    public string PurchaseID;
    public int ProductIndex;
    public RawImage YanIcon;
    public enum ItemType
    {
        Upgrade,
        Gun
    }

    public void OnEnable()
    {
        BuyOneButton.onClick.AddListener(BuyOneOnClick);
        BuyAllButton.onClick.AddListener(CallPurchaseMenu);
    }

    public void OnDisable()
    {
        BuyOneButton.onClick.RemoveAllListeners();
        BuyAllButton.onClick.RemoveAllListeners();
    }

    public void Awake()
    {
        BuyOneButtonText.font = SaveManager.Instance.CurrentFont;
        BuyAllButtonText.font = SaveManager.Instance.CurrentFont;
        UpdatePrice();
    }

    public void CheckPrice()
    {
        if (_ItemType == ItemType.Upgrade)
        {
            Price = MainMenuController.UpgradePrices[ItemIndex, MainMenuController.Progress.UpgradeLevel[ItemIndex]];
        }
        else if (_ItemType == ItemType.Gun)
        {
            Price = MainMenuController.GunPrices[ItemIndex, MainMenuController.Progress.GunLevel[ItemIndex]];
        }
        BuyOneButtonText.text = Price.ToString();
    }

    public void CheckMoney()
    {
        if (Price > MainMenuController.Progress.Money)
        {
            BuyOneButton.interactable = false;
        }
        else
        {
            BuyOneButton.interactable = true;
        }
    }

    public bool CheckIfMaxLevel()
    {
        if (_ItemType == ItemType.Upgrade && MainMenuController.Progress.UpgradeLevel[ItemIndex] >= 5)
        {
            BuyOneButton.interactable = false;
            BuyAllButton.interactable = false;
            BuyOneButtonText.text = SaveManager.Instance.Localization[17];
            BuyAllButtonText.text = SaveManager.Instance.Localization[17];
            return true;
        }
        else if (_ItemType == ItemType.Gun && MainMenuController.Progress.GunLevel[ItemIndex] >= 5)
        {
            BuyOneButton.interactable = false;
            BuyAllButton.interactable = false;
            BuyOneButtonText.text = SaveManager.Instance.Localization[17];
            BuyAllButtonText.text = SaveManager.Instance.Localization[17];
            
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CallPurchaseMenu()
    {
#if UNITY_EDITOR
        Debug.Log("CallPurchaseMenu");
        Debug.Log(gameObject.name);
        BuyAllOnClick("1234567890-1234567890");
#elif UNITY_WEBGL
        Yandex.CallPurchaseMenu(PurchaseID, gameObject.name);
#endif
    }

    public void BuyOneOnClick()
    {
        if (_ItemType == ItemType.Upgrade)
        {
            UnlockLockers(++MainMenuController.Progress.UpgradeLevel[ItemIndex]);
        }
        else if (_ItemType == ItemType.Gun)
        {
            UnlockLockers(++MainMenuController.Progress.GunLevel[ItemIndex]);
        }
        MainMenuController.SpendMoney(Price);
        MainMenuController.SaveData();
    }

    public void UpdatePrice()
    {
        if (!CheckIfMaxLevel())
        {
            CheckPrice();
            CheckMoney();
        }
    }

    public void BuyAllOnClick(string token)
    {
        if (_ItemType == ItemType.Upgrade)
        {
            MainMenuController.Progress.UpgradeLevel[ItemIndex] = 5;
        }
        else if (_ItemType == ItemType.Gun)
        {
            MainMenuController.Progress.GunLevel[ItemIndex] = 5;
        }
        UnlockLockers(5);
        CheckIfMaxLevel();
        MainMenuController.SaveData();
#if UNITY_EDITOR
        Debug.Log($"Token {token} consumed");
#elif UNITY_WEBGL
        Debug.Log(token);
        Yandex.ConsumePurchase(token);
#endif
    }


    public void UnlockLockers(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Lockers[i].SetActive(false);
        }
    }
}
