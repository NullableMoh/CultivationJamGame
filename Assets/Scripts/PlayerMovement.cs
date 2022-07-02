using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool enableJump = false;
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float jumpPower = 50f;
    [SerializeField] float accelDueToGravity = -2f;

    [SerializeField] float groundCheckDistance = 4.3f;

    Vector3 moveVecNoGravity;

    bool isGrounded = true;
    Vector3 gravityMoveVec;
    PlayerCharacterInputActions inputActions;
    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputActions = new();
        inputActions.Player.Enable();
    }

    void Update()
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
