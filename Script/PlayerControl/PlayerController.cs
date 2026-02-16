using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float rotationSpeed = 10f;
    public CharacterController controller;
    public Animator animator;
    public FarmingSystem farmingSystem;

    private Vector3 velocity;
    private bool isBusy;

    private void Update()
    {
        if (InventoryUI.IsOpen || (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive))
        {
            animator.SetFloat("Speed", 0);
            return;
        }

        HandleMovement();
        HandleInputActions();
    }

    private void HandleMovement()
    {
        Vector2 input = InputHandler.Instance.MoveInput;
        Vector3 move = new Vector3(input.x, 0, input.y).normalized;

        if (move.magnitude >= 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            controller.Move(move * walkSpeed * Time.deltaTime);
        }

        animator.SetFloat("Speed", move.magnitude);

        if (controller.isGrounded && velocity.y < 0) velocity.y = -2f;
        velocity.y += -9.81f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleInputActions()
    {
        if (isBusy) return;

        if (InputHandler.Instance.ActionTriggered)
        {
            ExecutePrimaryAction();
        }
        
        if (InputHandler.Instance.InteractTriggered)
        {
            ExecuteInteraction();
        }
    }

    private void ExecutePrimaryAction()
    {
        ItemSO heldItem = HotbarUI.Instance.GetHeldItem();
        if (heldItem == null) return;

        if (farmingSystem.TryGetTargetSoil(out SoilTile tile))
        {
            StartCoroutine(ActionRoutine("Interact", () => farmingSystem.ApplyItemOnTile(heldItem, tile)));
        }
    }

    private void ExecuteInteraction()
    {
        if (farmingSystem.TryGetTargetSoil(out SoilTile tile) && tile.IsReadyToHarvest)
        {
            StartCoroutine(ActionRoutine("Harvest", () => farmingSystem.TryHarvestExternal(tile)));
        }
    }

    private IEnumerator ActionRoutine(string trigger, System.Action logic)
    {
        isBusy = true;
        animator.SetTrigger(trigger);
        yield return new WaitForSeconds(0.5f);
        logic?.Invoke();
        yield return new WaitForSeconds(0.5f);
        isBusy = false;
    }
}