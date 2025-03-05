using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modified: Added a new speed type when the player holds the "Shift" key

public class FPSInput : MonoBehaviour
{   
    public float normalSpeed = 3.0f; 
    public float sprintSpeed = 6.0f; // New: Speed when "Shift" key is held <------------
    public float gravity = -9.8f;

    // Create a variable that references the character controller
    private CharacterController charController;
    public float currentSpeed; // New: variable to track which speed to use <------------

    public const float baseSpeed = 3.0f;

    private void OnEnable() {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDisable() {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnSpeedChanged(float value) {
        normalSpeed = baseSpeed * value; 
    }

    // Start is called before the first frame update
    void Start()
    {   
        // Get the reference to the character controller component using GetComponent<>()
        charController = GetComponent<CharacterController>();
        currentSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {   
        // New: Checks if the "Shift" key is being held, if so increases the speed otherwise the speed remains the same <------------
        if (Input.GetKey(KeyCode.LeftShift)) {
            currentSpeed = sprintSpeed + normalSpeed;
        } else {
            currentSpeed = normalSpeed;
        }

        float deltaX = Input.GetAxis("Horizontal") * normalSpeed;
        float deltaZ = Input.GetAxis("Vertical") * currentSpeed;

        // Instead of moving the player using transform.Translate, use the character controller 
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        // Ensures the player doesn't move too fast 
        movement = Vector3.ClampMagnitude(movement, currentSpeed);

        // Apply gravity 
        movement.y = gravity;

        // Since speed * time equals distance, multiply by Time.deltaTime to move a certain amount within one frame
        movement *= Time.deltaTime;

        movement = transform.TransformDirection(movement);
        charController.Move(movement);
    }
}
