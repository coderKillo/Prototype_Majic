using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputAction jumpAction;
    public InputAction shootAction;
    public Vector2 movement;

    private PlayerInput controls;
    private PlayerMovement playerMovement;

    private InputAction moveAction;

    private void Awake()
    {
        controls = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();

        moveAction = controls.actions["Move"];
        jumpAction = controls.actions["Jump"];
        shootAction = controls.actions["Shoot"];
    }

    private void OnEnable()
    {
        jumpAction.performed += _ => playerMovement.HandleJump();
    }

    public void HandleAllInputs()
    {
        movement = moveAction.ReadValue<Vector2>();
    }
}
