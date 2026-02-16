using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicFreeCam : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float fastMoveMultiplier = 3f;
    public float sensitivity = 2f;

    [Header("State Control")]
    public KeyCode toggleKey = KeyCode.G;
    private bool isActive;

    private float rotationX;
    private float rotationY;

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleFreeCam();
        }

        if (isActive)
        {
            HandleFreeMovement();
        }
    }

    private void ToggleFreeCam()
    {
        isActive = !isActive;
        
        Cursor.lockState = isActive ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isActive;

        if (GameManager.Instance != null)
        {
            // GameManager.Instance.SetPlayerControl(!isActive);
        }
    }

    private void HandleFreeMovement()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * fastMoveMultiplier : moveSpeed;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = (transform.forward * v) + (transform.right * h);
        transform.position += moveDir * currentSpeed * Time.deltaTime;
    }
}