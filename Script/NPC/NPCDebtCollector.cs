using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDebtCollector : MonoBehaviour
{
    [SerializeField] private float interactRange = 3f;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < interactRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Talk();
            }
        }
    }

    private void Talk()
    {
        int debt = DebtManager.Instance.remainingDebt;
        string msg = "Own" + debt + ". Back to work!";
        
        if (debt <= 0) msg = "Wow, you actually paid it off. Good dog.";

        DialogueManager.Instance.ShowMessage(msg);
    }
}