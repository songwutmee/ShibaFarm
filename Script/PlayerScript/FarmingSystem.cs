using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingSystem : MonoBehaviour
{
    public Camera mainCam;
    public LayerMask soilLayer;
    public LayerMask treeLayer;
    public float interactRange = 4.0f;

    public bool TryGetTargetSoil(out SoilTile tile)
    {
        tile = null;
        Ray ray = mainCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, soilLayer))
        {
            tile = hit.collider.GetComponent<SoilTile>();
        }
        return tile != null;
    }

    public bool TryGetTargetTree(out ChoppableCut_Tree tree)
    {
        tree = null;
        Ray ray = mainCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, treeLayer))
        {
            tree = hit.collider.GetComponent<ChoppableCut_Tree>();
        }
        return tree != null;
    }

    public void ApplyItemOnTile(ItemSO item, SoilTile tile)
    {
        if (item.category == ItemCategory.Tool)
        {
            if (item.toolAction == ToolAction.Hoe) tile.Till();
            else if (item.toolAction == ToolAction.Water) tile.Water();
        }
        else if (item.category == ItemCategory.Seed)
        {
            if (item.cropData != null) tile.Plant(item.cropData);
        }
    }

    public void TryHarvestExternal(SoilTile tile)
    {
        if (tile != null && tile.IsReadyToHarvest)
        {
            ItemSO product = tile.Harvest();
            if (product != null)
            {
                InventoryUI.Instance.AddItemToInventory(product, 1);
                GameEvents.TriggerCropHarvested();
            }
        }
    }

    public void ChopTree(ItemSO axe, ChoppableCut_Tree tree)
    {
        if (axe.toolAction == ToolAction.Axe)
        {
            tree.GetHit(1);
        }
    }
}