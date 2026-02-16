using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShop", menuName = "ShibaFarm/Shop Definition")]
public class ShopDefinition : ScriptableObject
{
    public string shopName; 
    
    [Serializable]
    public class ShopEntry
    {
        public ItemSO item;
        public int price;
    }

    public List<ShopEntry> items = new List<ShopEntry>();
}