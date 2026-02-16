using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemView : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public Button buyButton;

    private ItemSO currentItem;
    private int currentPrice;

    public void Setup(ItemSO item, int price)
    {
        currentItem = item;
        currentPrice = price;

        iconImage.sprite = item.icon;
        nameText.text = item.itemName;
        priceText.text = "$" + price.ToString();

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => ShopUI.Instance.BuyItem(currentItem, currentPrice));
    }
}