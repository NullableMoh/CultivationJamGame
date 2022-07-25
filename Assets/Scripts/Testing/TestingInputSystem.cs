using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 10f;

    private Vector2 inputVec;
    private float jumpInput;

    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.UI.Submit.performed += Submit;

        playerInputActions.Player.Disable();
        playerInputActions.Player.Jump.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnComplete(callback =>
            {
                callback.Dispose();
                playerInputActions.Player.Enable();
            }).Start();
    }

    private void SaveBindingOverrides()
    {
        string rebinds = playerInputActions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }

    private void LoadBindingOverrides()
    {
        string rebinds = PlayerPrefs.GetString("rebinds");
        playerInputActions.LoadBindingOverridesFromJson(rebinds);
    }

    private void Update()
    {
        inputVec = playerInputActions.Player.Movement.ReadValue<Vector2>();

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            Debug.Log("Switched to UI");
            playerInput.SwitchCurrentActionMap("UI");
            playerInputActions.Player.Disable();
            playerInputActions.UI.Enable();
        }
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            Debug.Log("switched to player");
            playerInput.SwitchCurrentActionMap("Player");
            playerInputActions.UI.Disable();
            playerInputActions.Player.Enable();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(inputVec.x * speed, rb.velocity.y, inputVec.y * speed);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("jump");
        rb.AddForce(new Vector3(0f, jumpPower, 0f));
    }

    private void Submit(InputAction.CallbackContext context)
    {
        Debug.Log("submit");
    }
}
