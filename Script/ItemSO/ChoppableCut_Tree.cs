using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppableCut_Tree : MonoBehaviour
{
    [Header("Tree Stats")]
    public float maxHealth = 3f;
    private float currentHealth;

    [Header("Drops")]
    public GameObject logPrefab;
    public int dropCount = 3;
    public GameObject seedDropPrefab;
    [Range(0f, 1f)] public float seedDropChance = 0.3f;

    [Header("Effects")]
    public ParticleSystem hitVFX;
    public AudioClip hitSound;
    public AudioClip fallSound;

    [HideInInspector] public SoilTile parentTile; 
    private bool isFalling;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void GetHit(float damage)
    {
        if (isFalling) return;

        currentHealth -= damage;
        if (hitVFX) hitVFX.Play();

        PlaySound(hitSound);

        if (currentHealth <= 0)
        {
            StartCoroutine(TreeFallRoutine());
        }
    }

    private IEnumerator TreeFallRoutine()
    {
        isFalling = true;
        PlaySound(fallSound);

        if (parentTile != null)
        {
            parentTile.ClearCrop();
        }

        float elapsed = 0;
        Quaternion startRot = transform.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(80, 0, 0);

        while (elapsed < 1.5f)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / 1.5f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        SpawnDrops();
        Destroy(gameObject, 1f);
    }

    private void SpawnDrops()
    {
        for (int i = 0; i < dropCount; i++)
        {
            Instantiate(logPrefab, transform.position + Vector3.up, Quaternion.identity);
        }

        if (seedDropPrefab != null && Random.value < seedDropChance)
        {
            Instantiate(seedDropPrefab, transform.position + Vector3.up, Quaternion.identity);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}