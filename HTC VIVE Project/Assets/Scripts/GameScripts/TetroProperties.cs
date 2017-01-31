using UnityEngine;
using System.Collections;
using System;

public class TetroProperties : MonoBehaviour {

    public Color cColor;
    public int iTetroID;
    public int iType;

    public int iColumn;
    public int iWall;

    public Vector3 vPositionCube;
    public Vector3 vPositionCube2;
    public Vector3 vPositionCube3;
    public Vector3 vPositionCube4;

    public Vector3 vRotation;

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
        }

        // Front
        else if (iWall == 3)
        {
            transform.position += new Vector3(-transform.position.x + fPosbig - iColumn, 0, -transform.position.z + -fPoslil);
            transform.rotation = new Quaternion();
        }

        // Right
        else if (iWall == 2)
        {
            transform.position += new Vector3(-transform.position.x + fPoslil, 0, -transform.position.z + fPosbig - iColumn);
            transform.rotation = new Quaternion();
            transform.Rotate(new Vector3(0, 90, 0));
        }

        // Left
        else if (iWall == 4)
        {
            transform.position += new Vector3(-transform.position.x + -fPoslil, 0, -transform.position.z + iColumn - fPosbig);
            transform.rotation = new Quaternion();
            transform.Rotate(new Vector3(0, -90, 0));
        }

        if (iType == 3)
            transform.Rotate(90, 0, 0);
    }

    internal void RotateTetro()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Rotates the Tetromino by a certain Angle
    /// </summary>
    public void RotateTetro(int iAngle)
    {
        if (iWall == 1 || iWall == 3)
            transform.Rotate(new Vector3(iAngle, 0, 0));
        else
            transform.Rotate(new Vector3(0, 0, iAngle));

        vRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }

    /// <summary>
    /// Calculates the position of each Cube
    /// </summary>
    public void CalculateCubes()
    {
        vPositionCube = transform.position;

        if (iType == 1)
            AsignCubes(new Vector3(0, 1), new Vector3(-1, 1), new Vector3(-1, 2));

        else if (iType == 2)
            AsignCubes(new Vector3(-1, -1), new Vector3(0, -1), new Vector3(-1, -2));

        else if (iType == 3)
            AsignCubes(new Vector3(0, 1), new Vector3(0, 2), new Vector3(0, 3));

        else if (iType == 4)
            AsignCubes(new Vector3(-1, 0), new Vector3(-2, 0), new Vector3(-1, 1));

        else if (iType == 5)
            AsignCubes(new Vector3(0, -1), new Vector3(0, -2), new Vector3(-1, -2));

        else if (iType == 6)
            AsignCubes(new Vector3(1, -1), new Vector3(1, -2), new Vector3(1, 0));
    }

    public void CalculateCubesZ()
    {
        vPositionCube = transform.position;

        int i;
        if (iWall == 4)
            i = -1;
        else
            i = 1;

        if (iType == 1)
            AsignCubes(new Vector3(0, 1), new Vector3(0, 1, 1 * i), new Vector3(0, 2, 1 * i));

        else if (iType == 2)
            AsignCubes(new Vector3(0, -1, 1 * i), new Vector3(0, -1), new Vector3(0, -2, 1 * i));

        else if (iType == 3)
            AsignCubes(new Vector3(0, 1), new Vector3(0, 2), new Vector3(0, 3));

        else if (iType == 4)
            AsignCubes(new Vector3(0, 0, 1 * i), new Vector3(0, 0, 2 * i), new Vector3(0, 1, 1 * i));

        else if (iType == 5)
            AsignCubes(new Vector3(0, -1), new Vector3(0, -2), new Vector3(0, -2, 1 * i));

        else if (iType == 6)
            AsignCubes(new Vector3(0, -1, -1 * i), new Vector3(0, -2, -1 * i), new Vector3(0, 0, -1 * i));
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
}
