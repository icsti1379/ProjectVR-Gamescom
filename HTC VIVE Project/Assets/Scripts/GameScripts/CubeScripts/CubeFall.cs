using UnityEngine;
using System.Collections;

public class CubeFall : MonoBehaviour {

    public static float fSpeed;
    CubeProperties cProperties;
    Rigidbody rb;

    private void Start()
    {
        cProperties = GetComponent<CubeProperties>();
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Makes the Tetromino fall with constant speed
    /// </summary>
    void Update()
    {
        transform.position -= new Vector3(0, 0.05f, 0);
        rb.AddForce(-rb.velocity);
        transform.rotation = new Quaternion();
    }
}
