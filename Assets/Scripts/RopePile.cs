using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class RopePile : MonoBehaviour
{
    [SerializeField] GameObject newRope;
    [SerializeField] float useRange = 2f;

    PlayerCharacterInputActions inputActions;
    bool pileHit;
    bool raycastHappened = false;

    public delegate void PileHitEventHandler();
    public event PileHitEventHandler OnPileHit;

    public delegate void PileNotHitEventHandler();
    public event PileNotHitEventHandler OnPileNotHit;

    public delegate void PileDestroyedEventHandler();
    public event PileHitEventHandler OnPileDestroyed;

    public delegate void PileMergedEventHandler();
    public event PileMergedEventHandler OnPileMerged;

    void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += MergePile;
    }

    void OnEnable()
    {
        inputActions.Player.Interact.performed += MergePile;
    }

    void OnDisable()
    {
        inputActions.Player.Interact.performed -= MergePile;
    }

    void MergePile(InputAction.CallbackContext context)
    {
        if (!pileHit || !newRope) return;

        Instantiate(newRope, transform.position, quaternion.identity);
        OnPileMerged?.Invoke();

        Destroy(gameObject);
        OnPileDestroyed?.Invoke();
    }

    void Update()
    {
        pileHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, useRange, LayerMask.GetMask("Rope Pile"));
        if (pileHit)
        {
            OnPileHit?.Invoke();
        }
        else
        {
            OnPileNotHit?.Invoke();
        }
    }
}
