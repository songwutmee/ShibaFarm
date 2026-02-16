using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public static TimeSystem Instance { get; private set; }

    [Header("Settings")]
    public float dayDurationInSeconds = 300f;
    [Range(0, 1)] public float timeOfDay;

    private void Awake() => Instance = this;

    private void Update()
    {
        timeOfDay += Time.deltaTime / dayDurationInSeconds;

        if (timeOfDay >= 1f)
        {
            timeOfDay = 0;
            GameEvents.TriggerNewDay();
        }

        GameEvents.TriggerTimeChanged(GetHour(), GetMinute());
    }

    public int GetHour() => Mathf.FloorToInt(timeOfDay * 24);
    public int GetMinute() => Mathf.FloorToInt((timeOfDay * 24 % 1) * 60);
}