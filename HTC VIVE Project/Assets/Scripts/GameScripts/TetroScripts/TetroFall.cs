using UnityEngine;
using System.Collections;

public class TetroFall : MonoBehaviour
{
    private TetroProperties tProperties;
    public static float fSpeed;
    public static float fTime;
    bool bFalling = false;

    private void Start()
    {
        tProperties = GetComponent<TetroProperties>();
    }

    /// <summary>
    /// Makes the Tetromino fall with constant speed
    /// </summary>
    void Update()
    {
        fTime += Time.deltaTime;

        if (fTime > fSpeed)
        {
            tProperties.gPFalling.LookToFall();
            fTime = 0;
        }

    }
}

