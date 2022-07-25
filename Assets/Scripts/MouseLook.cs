using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 5f;
    [SerializeField] private Transform playerParentTransform;

    private PlayerCharacterInputActions inputActions;

    private float mouseY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inputActions = new();
        inputActions.Player.Enable();
    }

    private void Update()
    {
        Vector2 inputVec = inputActions.Player.MouseLook.ReadValue<Vector2>();

        float mouseX = inputVec.x * mouseSensitivity * Time.deltaTime;
        mouseY -= inputVec.y * mouseSensitivity * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        playerParentTransform.Rotate(0f, mouseX, 0f);
        transform.localRotation = Quaternion.Euler(mouseY, 0f, 0f);
    }
}

