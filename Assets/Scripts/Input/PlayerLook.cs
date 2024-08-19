using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam; // Make sure to assign this in the inspector
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    public float xSensitivity = 1f;
    public float ySensitivity = 1f;

    private void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Hide the cursor
        Cursor.visible = false;
    }

    public void ProcessLook(Vector2 input)
    {
        // Adjust input by sensitivity settings
        float mouseX = input.x * mouseSensitivity * xSensitivity * Time.deltaTime;
        float mouseY = input.y * mouseSensitivity * ySensitivity * Time.deltaTime;

        // Apply vertical rotation (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit rotation to prevent flipping

        // Rotate the camera on the x-axis
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Apply horizontal rotation (yaw)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
