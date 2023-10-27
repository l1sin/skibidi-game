using TMPro;
using UnityEngine;

public class LocalizeTMP : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public int LineID;

    public void Start()
    {
        Text.font = SaveManager.Instance.CurrentFont;
        Text.text = SaveManager.Instance.Localization[LineID];
    }
}
