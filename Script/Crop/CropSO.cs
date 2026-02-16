using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCrop", menuName = "ShibaFarm/Crop")]
public class CropSO : ScriptableObject
{
    public string cropName;
    public GameObject[] growthPrefabs;
    public float[] stageDurations;
    
    [Header("Output")]
    public ItemSO harvestItem; 
}