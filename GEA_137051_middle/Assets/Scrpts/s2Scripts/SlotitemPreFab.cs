using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlotitemPreFab : MonoBehaviour
{
    public Image itemimage;
    public TextMeshProUGUI itemText;
    public BlockType BlockType;

    public void itemSetting(Sprite itemSprite, string txt, BlockType type)
    {
        itemimage.sprite = itemSprite;
        itemText.text = txt;
        BlockType = type;
    }
}
