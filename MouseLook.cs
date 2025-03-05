using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{   
    // assign the rotations to numbers 
    public enum RotationAxes {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY; // variable that will appear in the inspector in the drop-down menu
    public float sensitivityHor = 9.0f; // variable for the speed of the rotation
    public float sensitivityVer = 9.0f; // variable for the speed of the rotation 

    // limit on the degrees of movement from the vertical rotation
    public float minimumVert = -45f;
    public float maximumVert = 45f;

    private float verticalRot = 0; // variable for the vertical angle 

    void Start() 
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null) {
            body.freezeRotation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX) {
            // Horizontal rotation 
            transform.Rotate(0, sensitivityHor * Input.GetAxis("Mouse X"), 0);

        } else if (axes == RotationAxes.MouseY) {
            // Vertical roataion 
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVer; // Get the rotation input and assign it to a value
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert); // Clamp the value so it doesn't exceed the limits 

            float horizontalRot = transform.localEulerAngles.y; 

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);

        } else {
            // Horizontal and Vertical rotation 
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVer; // Get the rotation input and assign it to a value
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert); // Clamp the value so it doesn't exceed the limits 

            // Increment the horizontal rotation 
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float horizontalRot = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
        }
    }
}
