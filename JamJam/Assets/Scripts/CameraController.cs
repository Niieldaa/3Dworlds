using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100.0f;
    [SerializeField] private float distanceAboveGround = 2.0f;
    [SerializeField] private float verticalRotationLimit = 80.0f;  // Limit for vertical rotation
    [SerializeField] private Transform target; // Character to follow

    private Transform character;
    private float verticalRotation = 0f;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target not set for CameraController. Please assign a target GameObject in the inspector.");
            return;
        }

        character = target;  // Assign character as target
        Cursor.lockState = CursorLockMode.Locked;   // Lock the cursor to the center of the screen
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        RotateCamera(); // Rotate around character based on mouse movement
        MaintainGroundDistance(); // Ensure camera stays above the ground
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the character horizontally based on mouse X input
        character.Rotate(Vector3.up, mouseX);

        // Adjust vertical rotation within specified limits
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

        // Apply vertical rotation to camera by rotating around the X-axis
        transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, 0);
    }

    private void MaintainGroundDistance()
    {
        // Check if camera is below minimum height and adjust if necessary
        if (transform.position.y < target.position.y + distanceAboveGround)
        {
            transform.position = new Vector3(transform.position.x, target.position.y + distanceAboveGround, transform.position.z);
        }
    }
}
