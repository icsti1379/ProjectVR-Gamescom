using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TetroHolderProperties : MonoBehaviour {

    public int iTetroID;
    public int iType;

    public int iColumn;
    public int iWall;

    public Vector3 vPositionCube;
    public Vector3 vPositionCube2;
    public Vector3 vPositionCube3;
    public Vector3 vPositionCube4;

    public Vector3 vRotation;

    public bool bRotating;
    public bool bMovingLeft;
    public bool bMovingRight;
    public int bCheckedFrame = 0;
    public int bCheckedFrameFalling = 0;

    public bool bNeedToCheck;
    public bool bFalling;

    public int iTimesCorrected = 0;
    public bool bInitiate = true;


    public TetroProperties tPropertiesOfSpawn;

    /// <summary>
    /// Calculates the position of each Cube
    /// </summary>
    public void CalculateCubes()
    {
        vPositionCube = transform.position;
        Vector3 v1;
        Vector3 v2;
        Vector3 v3;


        if (iType == 1)
        {
            v1 = new Vector3(0, 1);
            v2 = new Vector3(-1, 1);
            v3 = new Vector3(-1, 2);
        }

        else if (iType == 2)
        {
            v1 = new Vector3(-1, -1);
            v2 = new Vector3(0, -1);
            v3 = new Vector3(-1, -2);
        }

        else if (iType == 3)
        {
            v1 = new Vector3(0, 0, -1);
            v2 = new Vector3(0, 0, -2);
            v3 = new Vector3(0, 0, -3);
        }

        else if (iType == 4)
        {
            v1 = new Vector3(-1, 0);
            v2 = new Vector3(-2, 0);
            v3 = new Vector3(-1, 1);
        }

        else if (iType == 5)
        {
            v1 = new Vector3(0, -1);
            v2 = new Vector3(0, -2);
            v3 = new Vector3(-1, -2);
        }

        else if (iType == 6)
        {
            v1 = new Vector3(1, -1);
            v2 = new Vector3(1, -2);
            v3 = new Vector3(1, 0);
        }
        else
        {
            v1 = new Vector3();
            v2 = new Vector3();
            v3 = new Vector3();
        }


        v1 = Quaternion.Euler(vRotation) * v1;
        v2 = Quaternion.Euler(vRotation) * v2;
        v3 = Quaternion.Euler(vRotation) * v3;

        AsignCubes(v1, v2, v3);
    }

    /// <summary>
    /// Needed in CalculateCubes
    /// </summary>
    public void AsignCubes(Vector3 vPos2, Vector3 vPos3, Vector3 vPos4)
    {
        vPositionCube2 = vPositionCube + vPos2;
        vPositionCube3 = vPositionCube + vPos3;
        vPositionCube4 = vPositionCube + vPos4;
    }


    /// <summary>
    /// Updates the Position of the Tetromino (Useful when Column or Wall needs to be changed)
    /// </summary>
    public void UpdatePosition()
    {
        float fPoslil = SpawnTetromino.iMapScale / 2 - 0.5f;
        float fPosbig = SpawnTetromino.iMapScale / 2 + 0.5f;

        if (iWall == 1)
        {
            transform.position += new Vector3(-transform.position.x + iColumn - fPosbig, 0, -transform.position.z + fPoslil);
        }

        // Front
        else if (iWall == 3)
        {
            transform.position += new Vector3(-transform.position.x + fPosbig - iColumn, 0, -transform.position.z + -fPoslil);
        }

        // Right
        else if (iWall == 2)
        {
            transform.position += new Vector3(-transform.position.x + fPoslil, 0, -transform.position.z + fPosbig - iColumn);
        }

        // Left
        else if (iWall == 4)
        {
            transform.position += new Vector3(-transform.position.x + -fPoslil, 0, -transform.position.z + iColumn - fPosbig);
        }
    }

    public void RotateTetro(int iAngle)
    {
        if (iType == 7)
            return;

        if (iType != 3)
        {
            transform.Rotate(new Vector3(0, 0, iAngle));
            vRotation += new Vector3(0, 0, iAngle);
            if (vRotation.z >= 360)
                vRotation.z -= 360;
        }

        else if(iType != 3)
        {

            vRotation = tPropertiesOfSpawn.vRotation;

            if (vRotation.z >= 90)
            {
                transform.position -= new Vector3(0, 1, 0);
                vRotation -= new Vector3(0, 0, iAngle);
                transform.Rotate(new Vector3(0, 0, -iAngle));
            }
            else
            {
                vRotation += new Vector3(0, 0, iAngle);
                transform.Rotate(new Vector3(0, 0, iAngle));
            }
        }
        else
        {
            vRotation = tPropertiesOfSpawn.vRotation;

            if (vRotation.x >= 90)
            {
                vRotation -= new Vector3(iAngle, 0, 0);
                transform.Rotate(new Vector3(-iAngle, 0, 0));
            }
            else
            {
                vRotation += new Vector3(iAngle, 0, 0);
                transform.Rotate(new Vector3(iAngle, 0, 0));
            }
        }
    }

    public void SynchroniseWithTetro()
    {
        iTetroID = tPropertiesOfSpawn.iTetroID;
        iType = tPropertiesOfSpawn.iType;

        iColumn = tPropertiesOfSpawn.iColumn;
        iWall = tPropertiesOfSpawn.iWall;

        vRotation = tPropertiesOfSpawn.vRotation;

        try
        {
            vPositionCube = tPropertiesOfSpawn.vPositionCube;
            vPositionCube2 = tPropertiesOfSpawn.vPositionCube2;
            vPositionCube3 = tPropertiesOfSpawn.vPositionCube3;
            vPositionCube4 = tPropertiesOfSpawn.vPositionCube4;
        }
        catch { }
    }

    public void LookIfAbleToMove(int Direction)
    {
        if (Direction == -1)
            bMovingLeft = true;

        else if (Direction == 1)
            bMovingRight = true;

        bNeedToCheck = true;

        iColumn += Direction;
        UpdatePosition();
    }

    public void LookIfAbleToRotate()
    {
        bNeedToCheck = true;
        bRotating = true;
        RotateTetro(90);
    }

    public void LookToFall()
    {
        transform.position -= new Vector3(0, 1, 0);
        bFalling = true;
    }

    public void Update()
    {
        if (bFalling)
        {
            bCheckedFrameFalling++;

            if (bCheckedFrameFalling > 2)
            {
                bFalling = false;
                bCheckedFrameFalling = 0;
                transform.localPosition = new Vector3();
                tPropertiesOfSpawn.transform.position -= new Vector3(0, 1, 0);
            }
        }

        else if (bNeedToCheck)
        {
            bCheckedFrame++;

            if (bCheckedFrame > 2)
            {
                bCheckedFrame = 0;

                bMovingRight = false;
                bMovingLeft = false;
                bNeedToCheck = false;
                bInitiate = false;


                if (bRotating)
                {
                    transform.rotation = new Quaternion();
                    //tPropertiesOfSpawn.transform.position += transform.localPosition;
                    //transform.localPosition = new Vector3();
                    tPropertiesOfSpawn.RotateTetro(90);
                    bRotating = false;
                }

                UpdateTetro();
                UpdatePosition();
                tPropertiesOfSpawn.ReplaceBorder();
            }
        }
        else if (!bNeedToCheck && bCheckedFrame > 0)
            bCheckedFrame = 0;
    }


    public void UpdateTetro()
    {
        tPropertiesOfSpawn.iColumn = iColumn;
        tPropertiesOfSpawn.UpdatePosition();
    }
}
