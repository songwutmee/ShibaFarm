using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }
    public static bool IsOpen { get; private set; }

    public GameObject inventoryPanel;
    public InventorySlot[] slots;

    private void Awake() => Instance = this;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) Toggle();
    }

    public void Toggle()
    {
        IsOpen = !IsOpen;
        inventoryPanel.SetActive(IsOpen);
        
        Cursor.visible = IsOpen;
        Cursor.lockState = IsOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public bool AddItemToInventory(ItemSO item, int amount)
    {
        if (item.isStackable)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == item)
                {
                    slot.SetItem(item, slot.amount + amount);
                    return true;
                }
            }
        }

        foreach (InventorySlot slot in slots)
        {
            if (slot.item == null)
            {
                slot.SetItem(item, amount);
                return true;
            }
        }

        return false;
    }
}