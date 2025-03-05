using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    // Private variable that has a reference to the camera 
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        // Use GetComponent<camera> to get a reference to the camera
        cam = GetComponent<Camera>();

        // Hide the cursor at the center of the screen 
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }
  
    // OnGUI method, for drawing crosshairs on the screen 
    private void OnGUI() {
        // Font size 
        int size = 12;

        // Coordinates at which the crosshairs are drawn 
        float posX = cam.pixelWidth/2 - size/4;
        float posY = cam.pixelHeight/2 - size/4;

        // Draw the crosshairs as text
        GUI.Label(new Rect(posX, posY, size, size), "*");

        if (GUI.Button(new Rect(10, 10, 180, 20), "Click here fore a free iPod!")) {
            Debug.Log("Button has been pressed");
        }
    }
       
    // Using a Coroutine, this will place a set of coords, then removes the sphere after 1 second 
    private IEnumerator SphereIndicator(Vector3 pos) {
        // Create a new game object that's a sphere 
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        // Place it at the given position 
        sphere.transform.position = pos;

        // Wait one second 
        yield return new WaitForSeconds(1);

        // Destroy the sphere
        Destroy(sphere);
    } 
    
    // Update is called once per frame
    void Update()
    {   
        // Run the following code if the Player clicks the left mouse button 
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            // Use a Vector3 to store the location of the middle of the screen 
            // Divide the width and height by 2 to get the midpoint; these become 
            // The x and y values of the vector, with the z value being zero 
            Vector3 point = new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0); 

            // Create a ray by calling ScreenPointToRay 
            // Pass in the point, as this is used as the origin for the ray 
            Ray ray = cam.ScreenPointToRay(point);

            // Create a RaycastHit object to figure out where the ray hit
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                // Prints out the coordinates of when the enemy was hit
                Debug.Log("Hit: " + hit.point);

                // Get a reference to the object that was hit
                // Get a reference to that object's ReactiveTarget script, if there's one
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                // Check if the ray hit an enemy, indicate it otherwise place a sphere
                if (target != null) {
                    target.ReactToHit();
                    if (target.deathAnim != null) Messenger.Broadcast(GameEvent.ENEMY_HIT);
                    Debug.Log("Target hit!");
                } else {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            } 
        }
    }
}
