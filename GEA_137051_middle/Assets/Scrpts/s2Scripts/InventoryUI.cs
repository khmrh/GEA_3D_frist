using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public List<Transform> slot = new List<Transform>();
    public GameObject SLotItem;
    List<GameObject> items = new List<GameObject>();

    public void UpdateInventory(Inventory myInven)
    {
        foreach (var slotItem in items)
        {
            Destroy(slotItem);
        }
        items.Clear();

        int idx = 0;
        foreach (var item in myInven.items)
        {
            if (idx >= slot.Count) break;

            var go = Instantiate(SLotItem, slot[idx]);
            go.transform.localPosition = Vector3.zero;

            SlotitemPreFab sItem = go.GetComponent<SlotitemPreFab>();
            items.Add(go);

            switch (item.Key)
            {
                case BlockType.Dirt:
                    sItem.itemSetting(Resources.Load<Sprite>("Sprites/Items/Dirt"), item.Value.ToString());
                    break;
                case BlockType.Grass:
                    sItem.itemSetting(Resources.Load<Sprite>("Sprites/Items/Grass"), item.Value.ToString());
                    break;
                case BlockType.Water:
                    sItem.itemSetting(Resources.Load<Sprite>("Sprites/Items/Water"), item.Value.ToString());
                    break;
                default:
                    break;
            }
            idx++;
        }
    }
}
