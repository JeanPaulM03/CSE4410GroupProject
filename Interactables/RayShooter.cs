using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint; // spawn point attached to player
    [SerializeField] private GameObject grenade; // grenade variant from prefab

    private int numGrenades = 5;

    private Camera cam;
    private float range = 10f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        // If 'E' is pressed spawn and throw grande prefab
        if (Input.GetKeyDown(KeyCode.E) && numGrenades > 0) {
            ThrowGrenade();
            numGrenades--;
            Debug.Log("Grenade Thrown");
        }
         
        
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);

            Ray ray = cam.ScreenPointToRay(point);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                //Debug.Log("Hit: " + hit.point);
                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos) {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }

    private void OnGUI()
    {
        int size = 12;

        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;

        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void ThrowGrenade()
    {
        GameObject thrownGrenade = Instantiate(grenade, spawnPoint.position, spawnPoint.rotation);
        thrownGrenade.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * range, ForceMode.Impulse);
    }
}
