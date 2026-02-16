using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebtManager : MonoBehaviour
{
    public static DebtManager Instance { get; private set; }

    [Header("The Burden")]
    public int remainingDebt = 50000;
    public int dailyInterest = 200;
    public int interestPenalty = 1000;

    private void Awake() => Instance = this;

    private void OnEnable() => GameEvents.OnNewDay += ApplyDailyStress;
    private void OnDisable() => GameEvents.OnNewDay -= ApplyDailyStress;

    private void ApplyDailyStress()
    {
        remainingDebt += dailyInterest;
        
        // Every week, Shiba must pay a debt
        if (CalendarSystem.Instance.currentDay % 7 == 0)
        {
            int autoPay = 1500;
            if (PlayerWallet.Instance.TrySpend(autoPay))
            {
                remainingDebt -= autoPay;
                DialogueManager.Instance.ShowMessage("Bank collected weekly payment: $" + autoPay);
            }
            else
            {
                remainingDebt += interestPenalty;
                DialogueManager.Instance.ShowMessage("Insufficient funds! Debt penalty applied.", 5f);
            }
        }
        
        // Refresh UI
        GameEvents.TriggerMoneyChanged(PlayerWallet.Instance.Balance);
    }
}