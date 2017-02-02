using UnityEngine;
using System.Collections;

public class ViveLaserPointerTEST : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Reference to lasers prefab.
    public GameObject laserPrefab;
    // Reference to an instance of laser.
    private GameObject laser;
    // Transform component is stored for ease to use.
    private Transform laserTransform;
    // Position where the laser hits.
    private Vector3 hitPoint;

    private void ShowLaser(RaycastHit hit)
    {
        // Shows the laser.
        laser.SetActive(true);
        // Position the laser between controller and raycast hitpoint.
        // Use of lerp because 0.5f is the precise middle point.
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        // Points the laser at the position where the raycast hits.
        laserTransform.LookAt(hitPoint);
        // Scale the laser so it fits perfectly between the two position.
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }

    void Start()
    {
        // Spawn a new laser and save a reference to it in laser.
        laser = Instantiate(laserPrefab);
        // Store's the laser transform component.
        laserTransform = laser.transform;
    }

	void Update ()
    {
        // If pressed "shoot" a ray, if it hits something store the point where it hit and show the laser
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;

            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
            }
        }
        else
        {
            laser.SetActive(false);
        }
    }
}