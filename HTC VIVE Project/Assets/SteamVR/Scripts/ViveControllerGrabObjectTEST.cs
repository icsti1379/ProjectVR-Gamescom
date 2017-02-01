using UnityEngine;
using System.Collections;

public class ViveControllerGrabObjectTEST : MonoBehaviour
{
    // Reference to comntroller as object being tracked
    private SteamVR_TrackedObject trackedObj;

    // Stores GameObject which trigger is currently colliding.
    private GameObject collidingObject;

    // Serves as a reference ro the GameObject that the player is currently grabbing.
    private GameObject objectInHand;

    // Device property to easy access the controller. Uses tracked object's index to return controllers's input.
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    /// <summary>
    /// Method accepts a collider as a parameter and uses its GameObject as the 
    /// collidingObject for grabbing and releasing.
    /// </summary>
    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = col.gameObject;
    }

    #region Trigger methods

    // When the trigger collider enters another, 
    // this sets up the other collider as a potential grab target.
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // Ensures that the target is set when the player holds a controller over an object for a while.
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // Whe the collider exits an object, abandoning an ungrabbed target,
    // this code removes its target by setting it to null.
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    #endregion

    #region Object grabbing methods

    private void GrabObject()
    {
        // Move GameObject inside the player's hand and remove it from the collidingObject variable.
        objectInHand = collidingObject;
        collidingObject = null;
        // Add a new joint that connects the controller to the object using the AddFixedJoint() method below.
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // Make a new fixes joint, add it to the controller, and then set it up so it doesn't break easily.
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    // NOTE: Maybe not needed for our project.
    /// <summary>
    /// This method removes the grabbed object's fixed joint and controls its speed
    /// and rotation when the player tosses it away
    /// </summary>
    private void ReleaseObject()
    {
        // Check if there's a fixed joint attached to the controller.
        if (GetComponent<FixedJoint>())
        {
            // Remove connection to the object held by the joint and destroy it.
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            
            // Add the speed and rotation of the controller when the player releases the object.
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }

        // Remove the reference to the formerly attached object.
        objectInHand = null;
    }

    #endregion

    void Update ()
    {
        // Grab potential target when player sqeezes the trigger.
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        // Release object attached to the controller when player releases the trigger.
        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
	}
}
