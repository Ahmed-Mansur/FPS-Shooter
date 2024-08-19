using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevel;

public class PlayerInput : MonoBehaviour
{
    private InputActions playerInput;
    private InputActions.OnFootActions OnFoot;
    private PlayerMovement playerMovement;
    private PlayerLook playerLook;

    private void Awake()
    {
        playerInput = new InputActions();
        OnFoot = playerInput.OnFoot;
        playerMovement = GetComponent<PlayerMovement>();
        playerLook = GetComponent<PlayerLook>();
        OnFoot.Jump.performed += _ => playerMovement.Jump();
        OnFoot.Sprint.started += _ => playerMovement.Sprint(true);
        OnFoot.Sprint.canceled += _ => playerMovement.Sprint(false);
        OnFoot.Crouch.started += _ => playerMovement.Crouch(true);
        OnFoot.Crouch.canceled += _ => playerMovement.Crouch(false);


        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component not found on the GameObject.");
        }

        if (playerLook == null)
        {
            Debug.LogError("PlayerMovement component not found on the GameObject.");
        }
    }

    private void FixedUpdate()
    {
        // Correct the method call by adding parentheses
        playerMovement.ProcessMove(OnFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {

        playerLook.ProcessLook(OnFoot.Look.ReadValue<Vector2>());
    }


    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
