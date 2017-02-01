using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplitTetrominos : MonoBehaviour {
    [SerializeField]
    GameObject gEmpty;

    [SerializeField]
    GameObject gCube;

    GameObject gEmptySpawned;
    GameObject gCubeSpawned;

    TetroProperties tProperties;


    public void SplitTetromino()
    {
        tProperties = GetComponent<TetroProperties>();
        gEmptySpawned = (GameObject)Instantiate(gEmpty, transform.position, new Quaternion(), transform.parent);
        gEmptySpawned.name = name;

        if (tProperties.iWall == 1 || tProperties.iWall == 3)
            tProperties.CalculateCubes();
        else
            tProperties.CalculateCubesZ();

        SpawnCube(tProperties.vPositionCube, 1);
        SpawnCube(tProperties.vPositionCube2, 2);
        SpawnCube(tProperties.vPositionCube3, 3);
        SpawnCube(tProperties.vPositionCube4, 4);

        Destroy(gameObject);
        SpawnTetromino.bTetroSplitted = true;
    }


    void SpawnCube(Vector3 vPosition, int iID)
    {
        TetroProperties tTetro;
        tTetro = GetComponent<TetroProperties>();

        gCubeSpawned = (GameObject)Instantiate(gCube, vPosition, new Quaternion(), gEmptySpawned.transform);
        gCubeSpawned.name = "Cube " + iID;

        CubeProperties tCube;
        tCube = gCubeSpawned.GetComponent<CubeProperties>();

        tCube.iCubeID = tTetro.iTetroID;
        tCube.iWall = tTetro.iWall;
        tCube.LocalPosition = vPosition;

        tCube.iTetroType = gameObject.GetComponent<TetroProperties>().iType;

        tCube.iRow = (int)Mathf.Round(Vector3.Distance(gCubeSpawned.transform.position, new Vector3(gCubeSpawned.transform.position.x, -0.5f, gCubeSpawned.transform.position.z)));

        Rigidbody rb;
        rb = gCubeSpawned.GetComponent<Rigidbody>();
        rb.isKinematic = true;


        TetroDismount.lListOfWall(tCube.iWall)[tCube.iRow - 1].Add(gCubeSpawned);
        TetroDismount.CheckComplete(tCube.iRow, tCube.iWall);
    }
}
