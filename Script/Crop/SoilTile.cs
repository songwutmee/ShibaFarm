using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilTile : MonoBehaviour
{
    public enum SoilState { Empty, Tilled, Watered }
    
    public SoilState currentState = SoilState.Empty;
    public CropSO crop; 
    public int stageIndex;
    public float growthTimer;

    [SerializeField] private MeshRenderer groundRenderer;
    [SerializeField] private Material dryMat;
    [SerializeField] private Material wetMat;
    private GameObject cropInstance;

    public bool IsReadyToHarvest => crop != null && stageIndex >= crop.growthPrefabs.Length - 1;

    private void OnEnable() => GameEvents.OnNewDay += ProcessGrowth;
    private void OnDisable() => GameEvents.OnNewDay -= ProcessGrowth;

    public void Till()
    {
        if (currentState != SoilState.Empty) return;
        currentState = SoilState.Tilled;
        UpdateVisuals();
    }

    public void Water()
    {
        if (currentState == SoilState.Empty) return;
        currentState = SoilState.Watered;
        UpdateVisuals();
    }

    public void Plant(CropSO newCrop)
    {
        if (currentState == SoilState.Empty || crop != null) return;
        crop = newCrop;
        stageIndex = 0;
        growthTimer = 0;
        RefreshCropVisual();
    }

    public ItemSO Harvest()
    {
        if (!IsReadyToHarvest) return null;

        ItemSO product = crop.harvestItem;
        
        // Reset tile state
        crop = null;
        if (cropInstance != null) Destroy(cropInstance);
        
        currentState = SoilState.Tilled; 
        UpdateVisuals();

        return product;
    }

    public void ClearCrop() 
    {
        crop = null;
        if (cropInstance != null) Destroy(cropInstance);
        UpdateVisuals();
    }

    private void ProcessGrowth()
    {
        if (crop != null && currentState == SoilState.Watered)
        {
            growthTimer++;
            if (growthTimer >= crop.stageDurations[stageIndex])
            {
                if (stageIndex < crop.growthPrefabs.Length - 1)
                {
                    stageIndex++;
                    growthTimer = 0;
                    RefreshCropVisual();
                }
            }
        }
        
        if (currentState == SoilState.Watered)
        {
            currentState = SoilState.Tilled;
            UpdateVisuals();
        }
    }

    private void UpdateVisuals()
    {
        if (groundRenderer == null) return;
        groundRenderer.enabled = currentState != SoilState.Empty;
        if (groundRenderer.enabled)
            groundRenderer.material = (currentState == SoilState.Watered) ? wetMat : dryMat;
    }

    private void RefreshCropVisual()
    {
        if (cropInstance != null) Destroy(cropInstance);
        if (crop == null) return;

        cropInstance = Instantiate(crop.growthPrefabs[stageIndex], transform.position, Quaternion.identity, transform);
    }
}