using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float rotationSpeed = 0.2f;

    [Header("Falling")]
    [SerializeField] private float jumpHeight = 0.5f;
    [SerializeField] private float gravityValue = -20.00f;
    [SerializeField] private LayerMask groundLayer;

    [HideInInspector] bool isGrounded = false;

    private AnimationManager playerAnimator;
    private CharacterController controller;
    private InputManager inputManager;
    private Transform cameraTransform;

    private Vector3 playerVelocity;

    private void Start()
    {
        playerAnimator = GetComponent<AnimationManager>();
        controller = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();

        cameraTransform = Camera.main.transform;

        inputManager.jumpAction.performed += _ => HandleJump();
    }

    private void FixedUpdate()
    {
        HandleFallingAndLanding();
    }

    private void Update()
    {
        controller.Move(playerVelocity * Time.deltaTime);
    }

    // moves the player relative to the reference rotation
    public void HandleMovement()
    {
        Vector2 moveInput = inputManager.movement;
        Vector3 move = cameraTransform.forward * moveInput.y + cameraTransform.right * moveInput.x;
        move *= playerSpeed;

        playerVelocity.x = move.x;
        playerVelocity.z = move.z;

        // playerAnimator.UpdateAnimatorValues(moveInput.magnitude, 0);
        playerAnimator.UpdateAnimatorValues(moveInput.y, moveInput.x);
    }

    // rotate player around the y axis relative to the reference
    public void HandleRotation()
    {
        #region rotate with moving
        // Vector2 moveInput = inputManager.movement;
        // if (moveInput == Vector2.zero)
        //     return;

        // Vector3 targetDirection = cameraTransform.forward * moveInput.y + cameraTransform.right * moveInput.x;
        #endregion
        Vector3 targetDirection = cameraTransform.forward;
        targetDirection.y = 0f;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetDirection), rotationSpeed);
    }

    public void HandleJump()
    {
        if (isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue + 0.5f);
            playerAnimator.PlayTargetAnimation("Jump", true);
        }

        // TODO: Add Animation Rigging for Look direction
        // TODO: Add Animation Rigging for Falling animation (Falling upwards) depend on Y velocity
    }

    private void HandleFallingAndLanding()
    {
        if (!isGrounded)
        {
            playerVelocity.y += gravityValue * Time.fixedDeltaTime;
            if (!playerAnimator.isInteracting)
            {
                playerAnimator.PlayTargetAnimation("Falling", true);
            }
        }
        else if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        if (CheckGrounded())
        {
            if (!isGrounded)
            {
                playerAnimator.PlayTargetAnimation("Landing", true);
            }

            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private bool CheckGrounded()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y += 0.5f;

        return Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, 0.51f, groundLayer);
    }
}
