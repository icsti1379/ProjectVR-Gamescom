using UnityEngine;
using System.Collections;

public class ViveControllerInputTest : MonoBehaviour
{
    // Reference to comntroller as object being tracked
    private SteamVR_TrackedObject trackedObj;

    // Device property to easy access the controller. Uses tracked object's index to return controllers's input.
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    
	// Update is called once per frame
	void Update ()
    {
        // Get the position of the finger when it's on the touchpad and write it to the Console.
        if (Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }

        // If HairTrigger down write it to the Console.
        if (Controller.GetHairTriggerDown())
        {
            Debug.Log(gameObject.name + " Trigger Press");
        }

        // If release HairTrigger write it to the Console.
        if (Controller.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + " Trigger Release");
        }

        // GripButton pressed down
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Press");
        }

        // GripButton released
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Release");
        }
    }
}