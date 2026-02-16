using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance { get; private set; }

    public GameObject shopPanel;
    public TextMeshProUGUI shopTitle;
    public Transform contentParent;
    public GameObject shopItemPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenShop(ShopDefinition shopData)
    {
        if (shopData == null) return;
        
        shopPanel.SetActive(true);
        shopTitle.text = shopData.shopName;
        
        // Clear list
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Build list
        foreach (var entry in shopData.items)
        {
            GameObject go = Instantiate(shopItemPrefab, contentParent);
            if (go.TryGetComponent(out ShopItemView view))
            {
                view.Setup(entry.item, entry.price);
            }
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void BuyItem(ItemSO item, int price)
    {
        if (PlayerWallet.Instance.TrySpend(price))
        {
            bool success = InventoryUI.Instance.AddItemToInventory(item, 1);
            if (!success)
            {
                PlayerWallet.Instance.AddMoney(price); // Refund
            }
        }
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}