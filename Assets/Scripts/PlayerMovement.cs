using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool enableJump = false;
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private float jumpPower = 50f;
    [SerializeField] private float accelDueToGravity = -2f;

    [SerializeField] private float groundCheckDistance = 4.3f;

    private Vector3 moveVecNoGravity;

    private bool isGrounded = true;
    private Vector3 gravityMoveVec;
    private PlayerCharacterInputActions inputActions;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputActions = new();
        inputActions.Player.Enable();
    }

    private void Update()
    {
        //move
        Vector2 inputVec = inputActions.Player.Movement.ReadValue<Vector2>();   
        moveVecNoGravity = transform.right * inputVec.x + transform.forward * inputVec.y;

        //grounded check
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, LayerMask.GetMask("Ground"));

        //fall
        gravityMoveVec.y = isGrounded ? 0f : gravityMoveVec.y + accelDueToGravity;

        //jump
        if(enableJump)
        {
            gravityMoveVec.y = inputActions.Player.Jump.IsPressed() && isGrounded ? jumpPower : gravityMoveVec.y;
        }
    }

    private void FixedUpdate()
    {
        //move
        characterController.Move(moveVecNoGravity * Time.fixedDeltaTime * moveSpeed);
        
        //fall & jump
        characterController.Move(gravityMoveVec * Time.fixedDeltaTime);
    }
}
