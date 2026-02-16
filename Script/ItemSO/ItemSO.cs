using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory { Tool, Seed, Product, Material }
public enum ToolAction { None, Hoe, Water, Axe }

[CreateAssetMenu(fileName = "NewItem", menuName = "ShibaFarm/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemCategory category;
    public int sellPrice;
    public bool isStackable;
    public int maxStack = 99;

    [Header("Tool Settings")]
    public ToolAction toolAction;
    public GameObject equipmentPrefab; 
    public int energyCost = 5;

    [Header("Seed Settings")]
    public CropSO cropData;
}