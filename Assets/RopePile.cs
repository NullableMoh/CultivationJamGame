using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class RopePile : MonoBehaviour
{
    [SerializeField] private GameObject newRope;
    [SerializeField] private float useRange = 2f;
    
    private PlayerCharacterInputActions inputActions;
    private bool canMergePile;
    private bool raycastHappened = false;

    public delegate void PileMergeableEventHandler();
    public event PileMergeableEventHandler OnPileMergeable;

    public delegate void PileNotMergeableEventHandler();
    public event PileNotMergeableEventHandler OnPileNotMergeable;

    public delegate void PileDestroyedEventHandler();
    public event PileMergeableEventHandler OnPileDestroyed;

    public delegate void PileMergedEventHandler();
    public event PileMergedEventHandler OnPileMerged;
    
    private void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += MergePile;
    }

    private void OnEnable()
    {
        inputActions.Player.Interact.performed += MergePile;
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= MergePile;
        OnPileDestroyed?.Invoke();
    }

    private void MergePile(InputAction.CallbackContext context)
    {
        if(!canMergePile || !newRope) return;
        
        Instantiate(newRope, transform.position, quaternion.identity);
        OnPileMerged?.Invoke();
        
        Destroy(this.gameObject);
    }

    private void Update()
    {
        bool raycast = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, useRange, LayerMask.GetMask("Rope Pile"));
        if (raycast)
        {
            raycastHappened = true;
            if (newRope)
            {
                canMergePile = true;
                OnPileMergeable?.Invoke();
            }
            else
            {
                canMergePile = false;
                OnPileNotMergeable?.Invoke();
            }
        }
        else if (!raycast && raycastHappened)
        {
            OnPileMergeable?.Invoke();
            raycastHappened = false;
        }
    }
}
