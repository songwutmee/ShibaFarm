using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarSystem : MonoBehaviour
{
    public static CalendarSystem Instance { get; private set; }

    [Header("Settings")]
    public float secondsPerDay = 120f;
    public int currentDay = 1;

    private float dayTimer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        dayTimer += Time.deltaTime;

        if (dayTimer >= secondsPerDay)
        {
            NextDay();
        }
    }

    public void NextDay()
    {
        dayTimer = 0;
        currentDay++;

        GameEvents.TriggerNewDay();
        
        Debug.Log("Morning of Day: " + currentDay);
    }

    public float GetTimeNormalized()
    {
        return dayTimer / secondsPerDay;
    }
}