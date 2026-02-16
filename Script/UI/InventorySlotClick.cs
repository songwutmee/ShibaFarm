using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotClick : MonoBehaviour, IPointerClickHandler
{
    public InventorySlot slot;

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryDragHandler drag = InventoryDragHandler.Instance;

        if (!drag.IsDragging)
        {
            drag.BeginDrag(slot);
        }
        else
        {
            ItemSO tempItem = slot.item;
            int tempAmount = slot.amount;

            slot.SetItem(drag.dragItem, drag.dragAmount);

            if (drag.draggedFromSlot != null)
            {
                if (tempItem == null) drag.draggedFromSlot.Clear();
                else drag.draggedFromSlot.SetItem(tempItem, tempAmount);
            }
            else if (drag.draggedFromHotbar != null)
            {
                if (tempItem == null) drag.draggedFromHotbar.Clear();
                else drag.draggedFromHotbar.SetItem(tempItem, tempAmount);
            }

            drag.EndDrag();
        }
    }
}