using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilGridSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject soilPrefab;
    public int rows = 5;
    public int cols = 5;
    public float spacing = 1.5f;

    [ContextMenu("Generate Grid")]
    public void GenerateGrid()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int z = 0; z < cols; z++)
            {
                Vector3 spawnPos = transform.position + new Vector3(x * spacing, 0, z * spacing);
                Instantiate(soilPrefab, spawnPos, Quaternion.identity, transform);
            }
        }
    }
}