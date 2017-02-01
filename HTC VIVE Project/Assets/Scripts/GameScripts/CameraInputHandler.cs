using UnityEngine;
using System.Collections;

public class CameraInputHandler : MonoBehaviour
{
    // Variables
    public float turnSpeed = 4.0f;  // Speed of camera turning when mouse moves in along an axis
    private Vector3 mouseOrigin;    // Position of cursor when mouse dragging starts
    private bool isRotating;        // Is the camera being rotated?

    // Update
    void Update()
    {
        // Get mouse origin position as Vector3 (X,Y,Z)
        //mouseOrigin = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }

        //// Rotate camera along X and Y axis
        //Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

        //transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
        //transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);

        // Disable movements on button release
        if (!Input.GetMouseButton(0)) isRotating = false;

        // Rotate camera along X and Y axis
        if (isRotating)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
            transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
        }

    }
}

    /* Origin MouseCamera Script

    // Variables
    public float turnSpeed = 4.0f;  // Speed of camera turning when mouse moves in along an axis
    public float panSpeed = 4.0f;   // Speed of the camera when being panned
    public float zoomSpeed = 4.0f;  // Speed of the camera going back and forth

    private Vector3 mouseOrigin;    // Position of cursor when mouse dragging starts
    private bool isRotating;        // Is the camera being rotated?
    private bool isPanning;         // Is the camera being panned?
    private bool isZooming;         // Is the camera zooming?

    // UPDATE
    void Update()
    {
        // Get the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }

        // Get the right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }

        // Get the middle mouse button
        if (Input.GetMouseButtonDown(2))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isZooming = true;
        }

        // Disable movements on button release
        if (!Input.GetMouseButton(0)) isRotating = false;
        if (!Input.GetMouseButton(1)) isPanning = false;
        if (!Input.GetMouseButton(2)) isZooming = false;


        // Rotate camera along X and Y axis
        if (isRotating)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
            transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
        }

        // Move the camera on it's XY plane
        if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
            transform.Translate(move, Space.Self);
        }

        // Move the camera linearly along Z axis
        if (isZooming)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = pos.y * zoomSpeed * transform.forward;
            transform.Translate(move, Space.World);
        }
    }
    */