using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoldObject : MonoBehaviour
{
    [SerializeField] private float holdDistance = 2f;
    
    //state
    private bool isHeld;
    
    //input actions
    private PlayerCharacterInputActions inputActions;

    //cache
    private Holdable currentObj;
    
    private void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();
    }

    private void OnEnable() => inputActions.Player.ToggleHoldObject.performed += ToggleHoldObject;

    private void OnDisable() => inputActions.Player.ToggleHoldObject.performed -= ToggleHoldObject;

    private void ToggleHoldObject(InputAction.CallbackContext context)
    {
        if (!currentObj) 
            isHeld = false;

        bool hitHoldable = Physics.Raycast(transform.position, transform.forward, out var hit, 20f, LayerMask.GetMask(("Holdable")));
        if (hitHoldable)
        {
            if (!isHeld)
            {
                currentObj = hit.transform.GetComponent<Holdable>();
                currentObj.transform.parent = transform;
                currentObj.transform.GetComponent<Rigidbody>().useGravity = false;
                isHeld = true;
            }
            else
            {
                currentObj.transform.GetComponent<Rigidbody>().useGravity = true;
                currentObj.transform.parent = null;
                isHeld = false;
            }
        }
    }

    private void Update()
    {
        if (currentObj && isHeld)
            currentObj.transform.localPosition = new Vector3(0f, 0f, holdDistance);
    }
}
