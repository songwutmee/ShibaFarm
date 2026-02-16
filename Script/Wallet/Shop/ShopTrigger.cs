using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public ShopDefinition catalog;
    public KeyCode interactKey = KeyCode.E;
    private bool playerInRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            if (ShopUI.Instance != null)
            {
                ShopUI.Instance.OpenShop(catalog);
            }
        }
    }
}