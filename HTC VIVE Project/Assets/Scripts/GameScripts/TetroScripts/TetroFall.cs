using UnityEngine;
using System.Collections;

public class TetroFall : MonoBehaviour
{
    private TetroProperties tProperties;
    private TetroHolderProperties tFallingProperties;
    private TetroHolderProperties tMovingProperties;


    public static float fSpeed;
    public static float fTime;

    private void Start()
    {
        tProperties = GetComponent<TetroProperties>();
        tFallingProperties = tProperties.gPFalling.GetComponent<TetroHolderProperties>();
        tMovingProperties = tProperties.gPMoving.GetComponent<TetroHolderProperties>();
    }

    /// <summary>
    /// Makes the Tetromino fall with constant speed
    /// </summary>
    void Update()
    {
        if (tFallingProperties != null && !tFallingProperties.bFalling)
        {
            fTime += Time.deltaTime;

            if (fTime > fSpeed && !tMovingProperties.bNeedToCheck)
            {
                tProperties.gPFalling.LookToFall();
                fTime = 0;
            }
        }
    }
}

