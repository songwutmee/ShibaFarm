using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    // Money & Economy
    public static Action<int> OnMoneyChanged;
    public static void TriggerMoneyChanged(int balance) => OnMoneyChanged?.Invoke(balance);

    // Time & Calendar
    public static Action OnNewDay;
    public static void TriggerNewDay() => OnNewDay?.Invoke();

    public static Action<int, int> OnTimeChanged; // Hour, Minute
    public static void TriggerTimeChanged(int h, int m) => OnTimeChanged?.Invoke(h, m);

    // Inventory
    public static Action OnInventoryUpdated;
    public static void TriggerInventoryUpdated() => OnInventoryUpdated?.Invoke();
    public static void TriggerCropHarvested() => OnInventoryUpdated?.Invoke(); 


    public static Action<ItemSO> OnItemSelected;
    public static void TriggerItemSelected(ItemSO item)
    {
        if (OnItemSelected != null) OnItemSelected.Invoke(item);
    }
}