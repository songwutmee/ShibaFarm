using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    [Header("Magnet Settings")]
    public ItemSO itemData;         
    public int amount = 1;         
    public float delayBeforeSuck = 0.5f;
    public float magnetRadius = 4f;
    public float magnetSpeed = 12f;

    private Transform playerTransform;
    private float spawnTime;
    private bool isBeingCollected;

    private void Start()
    {
        spawnTime = Time.time;
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomForce = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f));
            rb.AddForce(randomForce * 5f, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        if (playerTransform == null || isBeingCollected) return;

        if (Time.time < spawnTime + delayBeforeSuck) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance <= magnetRadius)
        {
            StartCoroutine(SuckTowardsPlayer());
        }
    }

    private IEnumerator SuckTowardsPlayer()
    {
        isBeingCollected = true;

        if (TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
        
        if (TryGetComponent(out Collider col))
        {
            col.enabled = false;
        }

        while (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position + Vector3.up) > 0.1f)
        {
            Vector3 targetPosition = playerTransform.position + Vector3.up;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, magnetSpeed * Time.deltaTime);
            yield return null;
        }

        Collect();
    }

    private void Collect()
    {
        if (InventoryUI.Instance != null)
        {
            bool added = InventoryUI.Instance.AddItemToInventory(itemData, amount);
            
            if (added)
            {
                // GameEvents.TriggerItemCollected();
                Destroy(gameObject);
            }
            else
            {
                isBeingCollected = false;
                ResetPhysics();
            }
        }
    }

    private void ResetPhysics()
    {
        if (TryGetComponent(out Rigidbody rb)) rb.isKinematic = false;
        if (TryGetComponent(out Collider col)) col.enabled = true;
    }
}