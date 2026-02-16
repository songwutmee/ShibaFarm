using UnityEngine;
using UnityEngine.EventSystems;

public class HotbarSlotClick : MonoBehaviour, IPointerClickHandler
{
    public HotbarSlot slot;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slot == null) return;

        var drag = InventoryDragHandler.Instance;
        if (drag == null) return;

        if (drag.IsDragging && drag.draggedFromSlot != null)
        {
            var from = drag.draggedFromSlot;

            if (from.item == null)
            {
                drag.EndDrag();
                return;
            }

            if (slot.item == null)
            {
                slot.SetStack(from.item, from.amount);
                from.Clear();
                drag.EndDrag();
                return;
            }
            if (slot.item == from.item && slot.item.isStackable)
            {
                int max = Mathf.Max(1, slot.item.maxStack);
                int canAdd = Mathf.Min(from.amount, max - slot.amount);

                if (canAdd > 0)
                {
                    slot.amount += canAdd;
                    slot.UpdateUI();

                    from.amount -= canAdd;
                    if (from.amount <= 0) from.Clear();
                    else from.UpdateUI();
                }

                drag.EndDrag();
                return;
            }

            ItemSO tempItem = slot.item;
            int tempAmt = slot.amount;

            slot.SetStack(from.item, from.amount);

            if (tempItem != null)
                from.SetItem(tempItem, tempAmt);
            else
                from.Clear();

            drag.EndDrag();
            return;
        }

        if (slot.item != null && !drag.IsDragging)
        {
            drag.BeginDragFromHotbar(slot);
        }
    }
}
