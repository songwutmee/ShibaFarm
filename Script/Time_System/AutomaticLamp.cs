using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticLamp : MonoBehaviour
{
    [Header("Settings")]
    public Light lampLight;
    public MeshRenderer lanternRenderer;
    public int materialIndex = 0;
    
    [Header("Visuals")]
    public Material onMaterial;
    public Material offMaterial;

    [Header("Schedule")]
    public int turnOnHour = 18;  // 6 PM
    public int turnOffHour = 6;  // 6 AM

    private void OnEnable() => GameEvents.OnTimeChanged += CheckTime;
    private void OnDisable() => GameEvents.OnTimeChanged -= CheckTime;

    private void CheckTime(int hour, int minute)
    {
        bool shouldBeOn = (hour >= turnOnHour || hour < turnOffHour);
        
        if (lampLight.enabled != shouldBeOn)
        {
            SetLampState(shouldBeOn);
        }
    }

    private void SetLampState(bool isOn)
    {
        if (lampLight != null) lampLight.enabled = isOn;

        if (lanternRenderer != null && onMaterial != null && offMaterial != null)
        {
            Material[] materials = lanternRenderer.materials;
            materials[materialIndex] = isOn ? onMaterial : offMaterial;
            lanternRenderer.materials = materials;
        }
    }
}