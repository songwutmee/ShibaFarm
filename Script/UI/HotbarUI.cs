using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    public static HotbarUI Instance { get; private set; }

    [Header("Configuration")]
    public HotbarSlot[] slots;
    public RectTransform selectionHighlight;
    public int selectedIndex = 0;
    public bool IsInputLocked { get; set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        UpdateSelectionUI();
    }

    private void Update()
    {
        if (IsInputLocked || InventoryUI.IsOpen) return;

        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedIndex = i;
                UpdateSelectionUI();
                GameEvents.TriggerItemSelected(slots[selectedIndex].item);
            }
        }
    }

    private void UpdateSelectionUI()
    {
        if (selectionHighlight != null && slots.Length > selectedIndex)
        {
            selectionHighlight.position = slots[selectedIndex].transform.position;
        }
    }

    public ItemSO GetSelectedItem()
    {
        if (slots == null || slots.Length <= selectedIndex) return null;
        return slots[selectedIndex].item;
    }

    public ItemSO GetHeldItem()
    {
        if (slots == null || slots.Length == 0 || selectedIndex >= slots.Length) return null;
        return slots[selectedIndex].item;
    }

}