using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlotitemPreFab : MonoBehaviour
{
    public Image itemimage;
    public TextMeshProUGUI itemText;

    public void itemSetting(Sprite itemSprite, string txt)
    {
        itemimage.sprite = itemSprite;
        itemText.text = txt;
    }
}
