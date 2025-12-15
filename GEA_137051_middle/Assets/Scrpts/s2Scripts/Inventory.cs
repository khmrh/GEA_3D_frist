using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<BlockType, int> items = new();

    public InventoryUI inventoryUI;

    private void Start()
    {
        if (inventoryUI == null)
        {
            inventoryUI = FindObjectOfType<InventoryUI>();
        }
        // 시작할 때 UI 한번 초기화
        if (inventoryUI != null)
        {
            inventoryUI.UpdateInventory(this);
        }
    }

    public void add(BlockType type, int count = 1)
    {
        if (!items.ContainsKey(type)) items[type] = 0;
        items[type] += count;
        Debug.Log($"[Inventory] +{count} {type} (총 {items[type]}");

        if (inventoryUI != null)
        {
            inventoryUI.UpdateInventory(this);
        }
    }

    public bool cosume(BlockType type, int count = 1)
    {
        if (!items.TryGetValue(type, out var have) || have < count) return false;
        items[type] = have - count;
        Debug.Log($"[Inventory] -{count} {type} (총 {items[type]}");
        if (items[type] == 0)
        {
            items.Remove(type);
            inventoryUI.selectedIndex = -1;
            inventoryUI.RssetSelection();
        }

        inventoryUI.UpdateInventory(this);
        return true;
    }

}
