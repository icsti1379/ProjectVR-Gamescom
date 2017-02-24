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

    private int iRow1;
    private int iRow2;
    private int iRow3;
    private int iRow4;

    private int iColumn1;
    private int iColumn2;
    private int iColumn3;
    private int iColumn4;

    public Vector3 vRotation;

    public bool bRotating;
    public bool bMovingLeft;
    public bool bMovingRight;
    public int bCheckedFrame = 0;
    public int bCheckedFrameFalling = 0;

    public bool bNeedToCheck;
    public bool bFalling;

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

        if (iType > 3)
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

    private void CalculateCubeRowsCloumns()
    {
        CalculateCubes();

        iRow1 = (int)Mathf.Round(Vector3.Distance(vPositionCube, new Vector3(vPositionCube.x, -0.5f, vPositionCube.z)));
        iRow2 = (int)Mathf.Round(Vector3.Distance(vPositionCube2, new Vector3(vPositionCube2.x, -0.5f, vPositionCube2.z)));
        iRow3 = (int)Mathf.Round(Vector3.Distance(vPositionCube3, new Vector3(vPositionCube3.x, -0.5f, vPositionCube3.z)));
        iRow4 = (int)Mathf.Round(Vector3.Distance(vPositionCube4, new Vector3(vPositionCube4.x, -0.5f, vPositionCube4.z)));

        iColumn1 = 0;
        iColumn2 = 0;
        iColumn3 = 0;
        iColumn4 = 0;

        if (iWall == 1)
        {
            iColumn1 = SpawnBorder.iMapScale - (int)(-vPositionCube.x + (SpawnBorder.iMapScale / 2 + 0.5f));
            iColumn2 = SpawnBorder.iMapScale - (int)(-vPositionCube2.x + (SpawnBorder.iMapScale / 2 + 0.5f));
            iColumn3 = SpawnBorder.iMapScale - (int)(-vPositionCube3.x + (SpawnBorder.iMapScale / 2 + 0.5f));
            iColumn4 = SpawnBorder.iMapScale - (int)(-vPositionCube4.x + (SpawnBorder.iMapScale / 2 + 0.5f));
        }

        if (iWall == 3)
        {
            iColumn1 = (int)(-vPositionCube.x + ((float)SpawnBorder.iMapScale / 2 + 1.5));
            iColumn2 = (int)(-vPositionCube2.x + ((float)SpawnBorder.iMapScale / 2 + 1.5));
            iColumn3 = (int)(-vPositionCube3.x + ((float)SpawnBorder.iMapScale / 2 + 1.5));
            iColumn4 = (int)(-vPositionCube4.x + ((float)SpawnBorder.iMapScale / 2 + 1.5));
        }

        if (iWall == 2)
        {
            iColumn1 = SpawnBorder.iMapScale - (int)(-vPositionCube.z + (SpawnBorder.iMapScale / 2 + 0.5f));
            iColumn2 = SpawnBorder.iMapScale - (int)(-vPositionCube2.z + (SpawnBorder.iMapScale / 2 + 0.5f));
            iColumn3 = SpawnBorder.iMapScale - (int)(-vPositionCube3.z + (SpawnBorder.iMapScale / 2 + 0.5f));
            iColumn4 = SpawnBorder.iMapScale - (int)(-vPositionCube4.z + (SpawnBorder.iMapScale / 2 + 0.5f));
        }

        if (iWall == 4)
        {
            iColumn1 = (int)(-vPositionCube.z + (SpawnBorder.iMapScale / 2 + 0.5f)) - 1;
            iColumn2 = (int)(-vPositionCube2.z + (SpawnBorder.iMapScale / 2 + 0.5f)) - 1;
            iColumn3 = (int)(-vPositionCube3.z + (SpawnBorder.iMapScale / 2 + 0.5f)) - 1;
            iColumn4 = (int)(-vPositionCube4.z + (SpawnBorder.iMapScale / 2 + 0.5f)) - 1;
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

        try
        {
            iRow1 = tPropertiesOfSpawn.iRow1;
            iRow2 = tPropertiesOfSpawn.iRow2;
            iRow3 = tPropertiesOfSpawn.iRow3;
            iRow4 = tPropertiesOfSpawn.iRow4;

            iColumn1 = tPropertiesOfSpawn.iColumn1;
            iColumn2 = tPropertiesOfSpawn.iColumn2;
            iColumn3 = tPropertiesOfSpawn.iColumn3;
            iColumn4 = tPropertiesOfSpawn.iColumn4;
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

        if (iType == 1)
            transform.position -= new Vector3(0, 1, 0);

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

        if (bNeedToCheck)
        {
            bCheckedFrame++;

            if (bCheckedFrame > 2)
            {
                bCheckedFrame = 0;

                bMovingRight = false;
                bMovingLeft = false;
                bNeedToCheck = false;


                if (bRotating)
                {
                    transform.rotation = new Quaternion();
                    tPropertiesOfSpawn.RotateTetro(90);
                    bRotating = false;
                }

                UpdateTetro();
                UpdatePosition();
            }
        }
    }


    public void UpdateTetro()
    {
        tPropertiesOfSpawn.iColumn = iColumn;
        tPropertiesOfSpawn.UpdatePosition();
    }
}
