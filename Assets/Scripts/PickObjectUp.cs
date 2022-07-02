using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickObjectUp : MonoBehaviour
{
    //params
    [SerializeField] float pickUpRange = 2f;
    
    //state
    [SerializeField] Transform objectHolder;
    bool holding = false;
    Holdable holdableObject;

    Vector3 posAtCollision;
    float holderToPlayerDistance;
    bool holdableTouchingSurface = false;

    //input actions
    PlayerCharacterInputActions inputActions;

    //cache
    Collider holdableObjectCollider;
    Rigidbody holdableObjectRigidbody;

    private void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();
    }

    private void Start()
    {
        holderToPlayerDistance = Mathf.Abs(transform.position.z - objectHolder.transform.position.z);
    }

    private void OnEnable()
    {
        inputActions.Player.ToggleHoldObject.performed += ToggleHoldObject;
    }

    private void OnDisable()
    {
        inputActions.Player.ToggleHoldObject.performed -= ToggleHoldObject;
    }

    private void Update()
    {
        HandleObjectCollisions();
    }

    void ToggleHoldObject(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpRange) && !holdableTouchingSurface)
        {
            if (hit.transform.gameObject.GetComponent<Holdable>())
            {
                holdableObject = hit.transform.gameObject.GetComponent<Holdable>();

                holdableObjectRigidbody = hit.transform.GetComponent<Rigidbody>();
                holdableObjectCollider = hit.transform.GetComponent<Collider>();

                if (!holding)
                {
                    holdableObjectRigidbody.useGravity = false;
                    holdableObject.transform.rotation = Quaternion.Euler(Vector3.zero);

                    holdableObject.transform.parent = objectHolder.transform;
                    holdableObject.transform.localPosition = new Vector3(0f, 0f, 0f);

                    holding = true; 
                }
                else
                {
                    holdableObjectRigidbody.useGravity = true;
                    holdableObject.transform.rotation = Quaternion.Euler(Vector3.zero);
                    
                    holding = false;

                    holdableObject.transform.parent = null;
                }
            }
            else
            {
                holding = false;
            }
        }
    }


    void HandleObjectCollisions()
    {
        if (holdableObject && holding)
        {
            holdableTouchingSurface = Physics.CheckSphere(holdableObject.transform.position, holdableObject.Radius, LayerMask.GetMask("Ground"));

            bool playerWithinRange = Mathf.Abs(transform.position.z - holdableObject.transform.position.z) < holderToPlayerDistance;

            if (holdableTouchingSurface)
            {
                holdableObject.transform.parent = null;
                holdableObjectRigidbody.velocity = new Vector3(0f, 0f, 0f);
            }
            else
            {
                holdableObject.transform.parent = objectHolder.transform;
                holdableObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            
            if(playerWithinRange)
            {
                holdableObject.transform.parent = null;
            }
            else
            {
                holdableObject.transform.parent = objectHolder.transform;
                //holdableObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
        }
    }

    public void ResetHoldingState()
    {
        holding = false;
        holdableTouchingSurface = false;
    }
}
