using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance { get; private set; }
    [SerializeField] private int money;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public int Balance => money;

    public void AddMoney(int amount)
    {
        money += amount;
        GameEvents.TriggerMoneyChanged(money);
    }

    public bool TrySpend(int amount)
    {
        if (money < amount) return false;
        money -= amount;
        GameEvents.TriggerMoneyChanged(money);
        return true;
    }
}