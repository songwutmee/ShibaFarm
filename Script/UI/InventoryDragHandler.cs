using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDragHandler : MonoBehaviour
{
    public static InventoryDragHandler Instance { get; private set; }

    [Header("UI Visuals")]
    [SerializeField] private Image dragIcon;

    public bool IsDragging { get; private set; }

    public ItemSO dragItem { get; private set; }
    public int dragAmount { get; private set; }
    public InventorySlot draggedFromSlot { get; private set; }
    public HotbarSlot draggedFromHotbar { get; private set; }

    private RectTransform iconRect;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        
        if (dragIcon != null)
        {
            iconRect = dragIcon.GetComponent<RectTransform>();
            dragIcon.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (IsDragging && iconRect != null)
        {
            iconRect.position = Input.mousePosition;
        }
    }

    public void BeginDrag(InventorySlot slot)
    {
        if (slot == null || slot.item == null) return;

        IsDragging = true;
        draggedFromSlot = slot;
        draggedFromHotbar = null;
        
        dragItem = slot.item;
        dragAmount = slot.amount;

        UpdateIcon(dragItem.icon);
    }

    public void BeginDragFromHotbar(HotbarSlot slot)
    {
        if (slot == null || slot.item == null) return;

        IsDragging = true;
        draggedFromHotbar = slot;
        draggedFromSlot = null;

        dragItem = slot.item;
        dragAmount = slot.amount;

        UpdateIcon(dragItem.icon);
    }

    public void EndDrag()
    {
        IsDragging = false;
        draggedFromSlot = null;
        draggedFromHotbar = null;
        dragItem = null;
        dragAmount = 0;

        if (dragIcon != null) dragIcon.gameObject.SetActive(false);
    }

    private void UpdateIcon(Sprite sprite)
    {
        if (dragIcon == null) return;
        dragIcon.sprite = sprite;
        dragIcon.gameObject.SetActive(true);
    }
}