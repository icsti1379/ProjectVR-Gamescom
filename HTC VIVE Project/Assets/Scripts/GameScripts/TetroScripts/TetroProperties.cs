using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public class TetroProperties : MonoBehaviour {


    public TetroHolderProperties gPMoving;
    public TetroHolderProperties gPFalling;

    public Color cColor;
    public int iTetroID;
    public int iType;

    public int iColumn;
    public int iWall;

    public Vector3 vPositionCube;
    public Vector3 vPositionCube2;
    public Vector3 vPositionCube3;
    public Vector3 vPositionCube4;

    public int iLastMovement;

    public Vector3 vRotation;

    public Vector3[] LeftRight;

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
            transform.rotation = new Quaternion();
            transform.Rotate(vRotation);
        }

        // Front
        else if (iWall == 3)
        {
            transform.position += new Vector3(-transform.position.x + fPosbig - iColumn, 0, -transform.position.z + -fPoslil);
            transform.rotation = new Quaternion();
            transform.Rotate(vRotation);
        }

        // Right
        else if (iWall == 2)
        {
            transform.position += new Vector3(-transform.position.x + fPoslil, 0, -transform.position.z + fPosbig - iColumn);
            transform.rotation = new Quaternion();
            transform.Rotate(vRotation);
        }

        // Left
        else if (iWall == 4)
        {
            transform.position += new Vector3(-transform.position.x + -fPoslil, 0, -transform.position.z + iColumn - fPosbig);
            transform.rotation = new Quaternion();
            transform.Rotate(vRotation);
        }
    }

    /// <summary>
    /// Rotates the Tetromino by a certain Angle
    /// </summary>
    public void RotateTetro(int iAngle)
    {
        if (iType == 7)
            return;

        transform.Rotate(new Vector3(0, 0, iAngle));
        vRotation += new Vector3(0, 0, iAngle);
        if (vRotation.z >= 360)
            vRotation.z -= 360;

    }

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

    public Vector3[] CalculateLeftRight()
    {
        CalculateCubes();
        Vector3[] VectorArray = new Vector3[4];

        VectorArray[0] = vPositionCube;
        VectorArray[1] = vPositionCube2;
        VectorArray[2] = vPositionCube3;
        VectorArray[3] = vPositionCube4;

        if (iWall == 1 || iWall == 3)
            BubbleSort(VectorArray, 'x');

        else if (iWall == 2 || iWall == 4)
            BubbleSort(VectorArray, 'z');

        return VectorArray;
    }

    public void BubbleSort(Vector3[] VectorArray, char iWallCoord)
    {
        for (int x = VectorArray.Count() - 1; x > 0; x--)
        {
            for (int y = 0; y < x; y++)
            {
                switch (iWallCoord)
                {
                    case 'x':
                        if (VectorArray[y].x > VectorArray[y + 1].x)
                        {
                            Vector3 ElementToChange = VectorArray[y];
                            VectorArray[y] = VectorArray[y + 1];
                            VectorArray[y + 1] = ElementToChange;
                        }
                        break;

                    case 'z':
                        if (VectorArray[y].z > VectorArray[y + 1].z)
                        {
                            Vector3 ElementToChange = VectorArray[y];
                            VectorArray[y] = VectorArray[y + 1];
                            VectorArray[y + 1] = ElementToChange;
                        }
                        break;
                }
            }
        }
    }

    public void ReplaceBorder()
    {
        LeftRight = CalculateLeftRight();

        if (iWall == 1 || iWall == 3)
        {
            SpawnTetromino.gBorderPlacement11.transform.position = new Vector3(LeftRight[0].x - 0.5f, SpawnTetromino.iSpawnPosY / 2, LeftRight[0].z + 0.5f);
            SpawnTetromino.gBorderPlacement22.transform.position = new Vector3(LeftRight[0].x - 0.5f, SpawnTetromino.iSpawnPosY / 2, LeftRight[0].z - 0.5f);
            SpawnTetromino.gBorderPlacement33.transform.position = new Vector3(LeftRight[3].x + 0.5f, SpawnTetromino.iSpawnPosY / 2, LeftRight[3].z + 0.5f);
            SpawnTetromino.gBorderPlacement44.transform.position = new Vector3(LeftRight[3].x + 0.5f, SpawnTetromino.iSpawnPosY / 2, LeftRight[3].z - 0.5f);
        }
        else
        {
            SpawnTetromino.gBorderPlacement11.transform.position = new Vector3(LeftRight[0].x + 0.5f, SpawnTetromino.iSpawnPosY / 2, LeftRight[0].z - 0.5f);
            SpawnTetromino.gBorderPlacement22.transform.position = new Vector3(LeftRight[0].x - 0.5f, SpawnTetromino.iSpawnPosY / 2, LeftRight[0].z - 0.5f);
            SpawnTetromino.gBorderPlacement33.transform.position = new Vector3(LeftRight[0].x + 0.5f, SpawnTetromino.iSpawnPosY / 2, LeftRight[3].z + 0.5f);
            SpawnTetromino.gBorderPlacement44.transform.position = new Vector3(LeftRight[0].x - 0.5f, SpawnTetromino.iSpawnPosY / 2, LeftRight[3].z + 0.5f);
        }
        SpawnTetromino.gBorderPlacement11.transform.localScale = new Vector3(0.05f, SpawnTetromino.iSpawnPosY, 0.05f);
        SpawnTetromino.gBorderPlacement22.transform.localScale = new Vector3(0.05f, SpawnTetromino.iSpawnPosY, 0.05f);
        SpawnTetromino.gBorderPlacement33.transform.localScale = new Vector3(0.05f, SpawnTetromino.iSpawnPosY, 0.05f);
        SpawnTetromino.gBorderPlacement44.transform.localScale = new Vector3(0.05f, SpawnTetromino.iSpawnPosY, 0.05f);
    }
}
