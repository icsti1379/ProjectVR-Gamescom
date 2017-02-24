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

    public int iTimesCorrected = 0;
    public bool bInitiate = true;

    public bool bFalling = false;
    public bool bChangingWall = false;

    int RotatingWall;

    public TetroProperties tPropertiesOfSpawn;
    public TetroHolderProperties tPropertiesOfFall;
    public Vector3[] LeftRight;

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
            v1 = new Vector3(0, -1);
            v2 = new Vector3(-1, 0);
            v3 = new Vector3(-1, 1);
        }

        else if (iType == 2)
        {
            v1 = new Vector3(0, 1);
            v2 = new Vector3(-1, 0);
            v3 = new Vector3(-1, -1);
        }

        else if (iType == 3)
        {
            v1 = new Vector3(1, 0);
            v2 = new Vector3(-1, 0);
            v3 = new Vector3(-2, 0);
        }

        else if (iType == 4)
        {
            v1 = new Vector3(-1, 0);
            v2 = new Vector3(1, 0);
            v3 = new Vector3(0, 1);
        }

        else if (iType == 5)
        {
            v1 = new Vector3(0, 1);
            v2 = new Vector3(0, -1);
            v3 = new Vector3(-1, -1);
        }

        else if (iType == 6)
        {
            v1 = new Vector3(0, 1);
            v2 = new Vector3(-1, 1);
            v3 = new Vector3(0, -1);
        }
        else if (iType == 7)
        {
            v1 = new Vector3(-1, 0);
            v2 = new Vector3(0, -1);
            v3 = new Vector3(-1, -1);
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
            transform.position += new Vector3(-transform.position.x + iColumn - fPosbig, 0, -transform.position.z + fPoslil);

        // Front
        else if (iWall == 3)
            transform.position += new Vector3(-transform.position.x + fPosbig - iColumn, 0, -transform.position.z + -fPoslil);

        // Right
        else if (iWall == 2)
            transform.position += new Vector3(-transform.position.x + fPoslil, 0, -transform.position.z + fPosbig - iColumn);

        // Left
        else if (iWall == 4)
            transform.position += new Vector3(-transform.position.x + -fPoslil, 0, -transform.position.z + iColumn - fPosbig);
    }

    public void RotateTetro(int iAngle)
    {
        if (iType == 7)
            return;

        vRotation = tPropertiesOfSpawn.vRotation;

        transform.Rotate(new Vector3(0, 0, iAngle));
        vRotation += new Vector3(0, 0, iAngle);
        if (vRotation.z >= 360)
            vRotation.z -= 360;
    }

    public void SynchroniseWithTetro()
    {
        iTetroID = tPropertiesOfSpawn.iTetroID;
        iType = tPropertiesOfSpawn.iType;
        LeftRight = tPropertiesOfSpawn.LeftRight;

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
        SynchroniseWithTetro();
        CalculateLeftRight();

        if (Direction == -1)
        {
            if (CalculateColumn(LeftRight[0]) <= 2)
                bChangingWall = true;

            bMovingLeft = true;
            tPropertiesOfSpawn.iLastMovement = 0;
        }

        else if (Direction == 1)
        {
            if (CalculateColumn(LeftRight[3]) >= 9)
                bChangingWall = true;

            bMovingRight = true;
            tPropertiesOfSpawn.iLastMovement = 1;
        }

        bNeedToCheck = true;

        if (!bChangingWall)
        {
            iColumn += Direction;
            UpdatePosition();
        }
        else
        {
            if (bMovingRight)
            {
                iWall++;

                if (iWall > 4)
                    iWall = 1;

                iColumn = 2;

                int z = (int)tPropertiesOfSpawn.vRotation.z;
                Debug.Log(tPropertiesOfSpawn.vRotation.z);

                if (z == 0)
                {
                    transform.Rotate(new Vector3(0, 90));
                    vRotation.y += 90;
                }
                else if (z == 90)
                {
                    transform.Rotate(new Vector3(90, 0));
                    vRotation.y += 90;
                }
                else if (z == 180)
                {
                    transform.Rotate(new Vector3(0, -90));
                    vRotation.y += 90;
                }
                else if (z == 270)
                {
                    transform.Rotate(new Vector3(-90, 0));
                    vRotation.y += 90;
                }

                RotatingWall = 90;

                UpdatePosition();
                CalculateLeftRight();

                while (CalculateColumn(LeftRight[0]) < 2)
                {
                    iColumn++;
                    UpdatePosition();
                    CalculateLeftRight();
                }
                Log();
            }

            else if (bMovingLeft)
            {
                iWall--;

                if (iWall < 1)
                    iWall = 4;

                iColumn = 9;

                int z = (int)tPropertiesOfSpawn.vRotation.z;
                Debug.Log(tPropertiesOfSpawn.vRotation.z);

                if (z == 0)
                {
                    transform.Rotate(new Vector3(0, -90));
                    vRotation.y -= 90;
                }
                else if (z == 90)
                {
                    transform.Rotate(new Vector3(-90, 0));
                    vRotation.y -= 90;
                }
                else if (z == 180)
                {
                    transform.Rotate(new Vector3(0, 90));
                    vRotation.y -= 90;
                }
                else if (z == 270)
                {
                    transform.Rotate(new Vector3(90, 0));
                    vRotation.y -= 90;
                }

                RotatingWall = -90;

                UpdatePosition();
                CalculateLeftRight();

                while (CalculateColumn(LeftRight[3]) > 9)
                {
                    iColumn--;
                    UpdatePosition();
                    CalculateLeftRight();
                }
                Log();
            }
        }
    }

    public void LookIfAbleToRotate()
    {
        bNeedToCheck = true;
        bRotating = true;
        RotateTetro(90);
        tPropertiesOfSpawn.iLastMovement = 2;
    }

    public void LookToFall()
    {
        bFalling = true;
        transform.position -= new Vector3(0, 0.25f, 0);
        tPropertiesOfSpawn.iLastMovement = 3;
    }

    public void Update()
    {
        if (bFalling)
        {
            bCheckedFrameFalling++;

            if (bCheckedFrameFalling > 2)
            {
                bCheckedFrameFalling = 0;
                transform.localPosition = new Vector3();
                tPropertiesOfSpawn.transform.position -= new Vector3(0, 1, 0);
                bFalling = false;
            }
        }

        if (bNeedToCheck)
        {
            bCheckedFrame++;

            if (bCheckedFrame > 2)
            {
                bCheckedFrame = 0;

                if (bChangingWall)
                {
                    bChangingWall = false;
                    tPropertiesOfFall.iWall = iWall;
                    tPropertiesOfFall.iColumn = iColumn;
                    transform.localRotation = new Quaternion();

                    tPropertiesOfSpawn.vRotation.y += RotatingWall;
                }

                bMovingRight = false;
                bMovingLeft = false;
                bNeedToCheck = false;
                bInitiate = false;

                if (bRotating)
                {
                    tPropertiesOfSpawn.RotateTetro(90);
                    transform.localRotation = new Quaternion();
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
        tPropertiesOfSpawn.iWall = iWall;
        tPropertiesOfSpawn.UpdatePosition();
    }

    public void CalculateLeftRight()
    {
        CalculateCubes();
        LeftRight = new Vector3[4];

        LeftRight[0] = vPositionCube;
        LeftRight[1] = vPositionCube2;
        LeftRight[2] = vPositionCube3;
        LeftRight[3] = vPositionCube4;

        BubbleSort(LeftRight, iWall);
    }

    public void BubbleSort(Vector3[] VectorArray, int iWall)
    {
        for (int x = VectorArray.Count() - 1; x > 0; x--)
        {
            for (int y = 0; y < x; y++)
            {
                if (iWall == 1)
                {

                    if (VectorArray[y].x > VectorArray[y + 1].x)
                    {
                        Vector3 ElementToChange = VectorArray[y];
                        VectorArray[y] = VectorArray[y + 1];
                        VectorArray[y + 1] = ElementToChange;
                    }
                }

                else if (iWall == 3)
                {

                    if (VectorArray[y].x < VectorArray[y + 1].x)
                    {
                        Vector3 ElementToChange = VectorArray[y];
                        VectorArray[y] = VectorArray[y + 1];
                        VectorArray[y + 1] = ElementToChange;
                    }
                }

                else if (iWall == 2)
                {
                    if (VectorArray[y].z < VectorArray[y + 1].z)
                    {
                        Vector3 ElementToChange = VectorArray[y];
                        VectorArray[y] = VectorArray[y + 1];
                        VectorArray[y + 1] = ElementToChange;
                    }
                }

                else if (iWall == 4)
                {
                    if (VectorArray[y].z > VectorArray[y + 1].z)
                    {
                        Vector3 ElementToChange = VectorArray[y];
                        VectorArray[y] = VectorArray[y + 1];
                        VectorArray[y + 1] = ElementToChange;
                    }
                }
            }
        }
    }

    public void Log()
    {
        CalculateLeftRight();

        Debug.Log("");
        Debug.Log(CalculateColumn(LeftRight[0]));
        Debug.Log(CalculateColumn(LeftRight[1]));
        Debug.Log(CalculateColumn(LeftRight[2]));
        Debug.Log(CalculateColumn(LeftRight[3]));
        Debug.Log("");
        
        Debug.Log(LeftRight[0]);
        Debug.Log(LeftRight[1]);
        Debug.Log(LeftRight[2]);
        Debug.Log(LeftRight[3]);
        Debug.Log("");
    }

    public float CalculateColumn(Vector3 vPosition)
    {
        float vPositionx = Mathf.Round(vPosition.x * 2) / 2;
        float vPositionz = Mathf.Round(vPosition.z * 2) / 2;

        if (iWall == 1)
            return (Vector3.Distance(new Vector3(-(SpawnBorder.iMapScale / 2 + 0.5f), 0, 0), new Vector3(vPositionx, 0, 0)));

        else if (iWall == 3)
            return (Vector3.Distance(new Vector3((SpawnBorder.iMapScale / 2 + 0.5f), 0, 0), new Vector3(vPositionx, 0, 0)));

        else if (iWall == 2)
            return (Vector3.Distance(new Vector3(0, 0, (SpawnBorder.iMapScale / 2 + 0.5f)), new Vector3(0, 0, vPositionz)));

        else
            return (Vector3.Distance(new Vector3(0, 0, -(SpawnBorder.iMapScale / 2 + 0.5f)), new Vector3(0, 0, vPositionz)));
    }
}
