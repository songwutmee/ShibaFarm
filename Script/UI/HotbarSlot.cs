using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotbarSlot : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI amountText;

    public ItemSO item;
    public int amount;

    public bool HasStack => item != null && amount > 0;

    public void SetItem(ItemSO newItem, int newAmount)
    {
        item = newItem;
        amount = newAmount;
        UpdateUI();
    }

    public void SetStack(ItemSO newItem, int newAmount)
    {
        SetItem(newItem, newAmount);
    }

    public void Clear()
    {
        item = null;
        amount = 0;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (iconImage == null) return;

        if (item != null)
        {
            iconImage.sprite = item.icon;
            iconImage.enabled = true;
            if (amountText) amountText.text = amount > 1 ? amount.ToString() : "";
        }
        else
        {
            iconImage.enabled = false;
            if (amountText) amountText.text = "";
        }
    }
}