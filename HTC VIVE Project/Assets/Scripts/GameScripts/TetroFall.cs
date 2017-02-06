using UnityEngine;
using System.Collections;

public class TetroFall : MonoBehaviour {


    //NOTE: ?? Where is this been used? Needed for speed down tetromino!
    public static float fSpeed;

    /// <summary>
    /// Makes the Tetromino fall with constant speed
    /// </summary>
    void Update ()
    {
        transform.position -= new Vector3(0, 0.15f, 0) ;
    }
}
