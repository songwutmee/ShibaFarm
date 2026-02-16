using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBox : MonoBehaviour
{
    [SerializeField] private float sellMultiplier = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickupable"))
        {
            SellItem(other.gameObject);
        }
    }

    private void SellItem(GameObject itemObject)
    {
        WorldItem worldItem = itemObject.GetComponent<WorldItem>();
        
        if (worldItem != null && worldItem.itemData != null)
        {
            SellHeldItem(worldItem.itemData);
            Destroy(itemObject); 
        }
    }

    public void SellHeldItem(ItemSO item)
    {
        if (item == null) return;
        
        int price = Mathf.RoundToInt(item.sellPrice * sellMultiplier);
        
        if (PlayerWallet.Instance != null)
        {
            PlayerWallet.Instance.AddMoney(price);
            Debug.Log("Shiba sold " + item.itemName + " for $" + price);
        }
    }
}