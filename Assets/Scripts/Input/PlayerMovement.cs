using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 10f;
    public float sprintSpeedMultiplier = 1.5f;
    public float crouchSpeedMultiplier = 0.5f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -12.81f;
    private bool isSprinting;
    private bool isCrouching;
    [SerializeField] Camera cam;
    Vector3 camPos;
    Vector3 newCamPos;
    public float crouchTransitionDuration = 0.2f;
    private void Start()
    {
        // Initialize the CharacterController component
        characterController = GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("CharacterController component not found on the GameObject.");
        }
    }

    public void ProcessMove(Vector2 input)
    {
        // Check if the player is grounded
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Convert input to 3D movement
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = transform.TransformDirection(move);

        // Apply speed modifiers
        if (isSprinting)
        {
            move *= playerSpeed * sprintSpeedMultiplier;
        }
        else if (isCrouching)
        {
            move *= playerSpeed * crouchSpeedMultiplier;
        }
        else
        {
            move *= playerSpeed;
        }

        characterController.Move(move * Time.deltaTime);

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    public void Sprint(bool isSprinting)
    {
        this.isSprinting = isSprinting;
    }

    public void Crouch(bool isCrouching)
    {
        this.isCrouching = isCrouching;
        camPos = cam.transform.localPosition;
        newCamPos = camPos;
        newCamPos.y = isCrouching ? camPos.y - 0.5f : camPos.y + 0.5f;
        StopCoroutine("CrouchStandTransition");
        StartCoroutine(CrouchStandTransition(camPos, newCamPos));
    }

    private IEnumerator CrouchStandTransition(Vector3 start, Vector3 end)
    {
        float elapsedTime = 0f;
        while (elapsedTime < crouchTransitionDuration)
        {
            cam.transform.localPosition = Vector3.Lerp(start, end, elapsedTime / crouchTransitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cam.transform.localPosition = end;
    }
}
