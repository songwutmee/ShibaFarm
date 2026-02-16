using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [Header("Setup")]
    public Transform toolSocket;

    private ItemSO currentItem;
    private GameObject currentModel;

    private void Update()
    {
        if (HotbarUI.Instance == null) return;

        ItemSO selectedItem = HotbarUI.Instance.GetSelectedItem();

        if (selectedItem != currentItem)
        {
            EquipItem(selectedItem);
        }
    }

    public void EquipItem(ItemSO newItem)
    {
        currentItem = newItem;

        if (currentModel != null)
        {
            Destroy(currentModel);
            currentModel = null;
        }

        if (newItem == null || newItem.equipmentPrefab == null)
        {
            return;
        }

        currentModel = Instantiate(newItem.equipmentPrefab, toolSocket);

        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.identity;
        currentModel.transform.localScale = Vector3.one;

    }
}
