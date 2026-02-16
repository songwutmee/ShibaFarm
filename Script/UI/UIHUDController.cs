using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHUDController : MonoBehaviour
{
    [Header("HUD Elements")]
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI debtText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI clockText;

    private void OnEnable()
    {
        GameEvents.OnMoneyChanged += RefreshFinanceUI;
        GameEvents.OnNewDay += RefreshDayUI;
        GameEvents.OnTimeChanged += RefreshClockUI;
    }

    private void OnDisable()
    {
        GameEvents.OnMoneyChanged -= RefreshFinanceUI;
        GameEvents.OnNewDay -= RefreshDayUI;
        GameEvents.OnTimeChanged -= RefreshClockUI;
    }

    private void RefreshFinanceUI(int balance)
    {
        cashText.text = $"Cash: ${balance:n0}";
        int currentDebt = DebtManager.Instance.remainingDebt;
        debtText.text = $"Debt: ${currentDebt:n0}";
        debtText.color = currentDebt > 20000 ? Color.red : Color.white;
    }

    private void RefreshDayUI()
    {
        dayText.text = $"Day: {CalendarSystem.Instance.currentDay}";
    }

    private void RefreshClockUI(int hour, int minute)
    {
        clockText.text = $"{hour:00}:{minute:00}";
    }
}