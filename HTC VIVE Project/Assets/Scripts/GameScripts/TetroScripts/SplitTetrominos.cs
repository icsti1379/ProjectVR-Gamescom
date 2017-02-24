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
    GameObject gCubeSpawned2;
    GameObject gCubeSpawned3;
    GameObject gCubeSpawned4;

    TetroProperties tProperties;
    TetroProperties tTetro;


    public void SplitTetromino()
    {
        tProperties = GetComponent<TetroProperties>();
        gEmptySpawned = (GameObject)Instantiate(gEmpty, transform.position, new Quaternion(), transform.parent);
        gEmptySpawned.name = name;

        tProperties.CalculateCubes();

        gCubeSpawned = SpawnCube(tProperties.vPositionCube, 1, gCubeSpawned);
        gCubeSpawned2 = SpawnCube(tProperties.vPositionCube2, 2, gCubeSpawned2);
        gCubeSpawned3 = SpawnCube(tProperties.vPositionCube3, 3, gCubeSpawned3);
        gCubeSpawned4 = SpawnCube(tProperties.vPositionCube4, 4, gCubeSpawned4);

        SearchCubes(gCubeSpawned, gCubeSpawned2, gCubeSpawned3, gCubeSpawned4);
        SearchCubes(gCubeSpawned2, gCubeSpawned, gCubeSpawned3, gCubeSpawned4);
        SearchCubes(gCubeSpawned3, gCubeSpawned, gCubeSpawned2, gCubeSpawned4);
        SearchCubes(gCubeSpawned4, gCubeSpawned, gCubeSpawned2, gCubeSpawned3);

        CheckRow(gCubeSpawned);
        CheckRow(gCubeSpawned2);
        CheckRow(gCubeSpawned3);
        CheckRow(gCubeSpawned4);

        SpawnTetromino.bTetroSplitted = true;
        TetroFall.fTime = 0;
        tProperties.gPFalling.GetComponent<TetroHolderProperties>().bFalling = false;
        Destroy(gameObject);
    }


    GameObject SpawnCube(Vector3 vPosition, int iID, GameObject gCubeSpawned)
    {
        vPosition = new Vector3(Mathf.Round(vPosition.x * 2) / 2, Mathf.Round(vPosition.y * 2) / 2, Mathf.Round(vPosition.z * 2) / 2);
        tTetro = GetComponent<TetroProperties>();

        gCubeSpawned = (GameObject)Instantiate(gCube, vPosition, new Quaternion(), gEmptySpawned.transform);
        gCubeSpawned.name = "Cube " + iID;

        CubeProperties tCube;
        tCube = gCubeSpawned.GetComponent<CubeProperties>();

        tCube.iCubeID = tTetro.iTetroID;
        tCube.iWall = tTetro.iWall;
        tCube.GlobalPosition = vPosition;
        tCube.iLastMovement = tTetro.iLastMovement;

        if (tCube.iWall == 1)
            tCube.iColumn = (int)Mathf.Round(Vector3.Distance(new Vector3(-(SpawnBorder.iMapScale / 2 + 0.5f), 0, 0), new Vector3(tCube.transform.position.x, 0, 0)));

        else if (tCube.iWall == 3)
            tCube.iColumn = (int)Mathf.Round(Vector3.Distance(new Vector3((SpawnBorder.iMapScale / 2 + 0.5f), 0, 0), new Vector3(tCube.transform.position.x, 0, 0)));

        else if (tCube.iWall == 2)
            tCube.iColumn = (int)Mathf.Round(Vector3.Distance(new Vector3(0, 0, (SpawnBorder.iMapScale / 2 + 0.5f)), new Vector3(0, 0, tCube.transform.position.z)));

        else if (tCube.iWall == 4)
            tCube.iColumn = (int)Mathf.Round(Vector3.Distance(new Vector3(0, 0, -(SpawnBorder.iMapScale / 2 + 0.5f)), new Vector3(0, 0, tCube.transform.position.z)));


        tCube.iTetroType = gameObject.GetComponent<TetroProperties>().iType;
        tCube.iRow = (int)Mathf.Round(Vector3.Distance(gCubeSpawned.transform.position, new Vector3(gCubeSpawned.transform.position.x, -0.5f, gCubeSpawned.transform.position.z)));

        Rigidbody rb;
        rb = gCubeSpawned.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        tCube.CheckBugFix();

        TetroDismount.lListOfWall(tCube.iWall)[tCube.iRow - 1].Add(gCubeSpawned);
        tCube.bInList = true;

        return gCubeSpawned;
    }


    void SearchCubes(GameObject gCubeSpawned, GameObject gCubeSpawnedGroup1, GameObject gCubeSpawnedGroup2, GameObject gCubeSpawnedGroup3)
    {
        CubeProperties tCube;
        tCube = gCubeSpawned.GetComponent<CubeProperties>();

        tCube.SearchVertical();
        tCube.SearchHorizontal();

        switch (gCubeSpawned.name) {
            case "Cube 1":
                tCube.gCube1 = gCubeSpawned;
                break;
            case "Cube 2":
                tCube.gCube2 = gCubeSpawned;
                break;
            case "Cube 3":
                tCube.gCube3 = gCubeSpawned;
                break;
            case "Cube 4":
                tCube.gCube4 = gCubeSpawned;
                break;
        }

        switch (gCubeSpawnedGroup1.name)
        {
            case "Cube 1":
                tCube.gCube1 = gCubeSpawnedGroup1;
                break;
            case "Cube 2":
                tCube.gCube2 = gCubeSpawnedGroup1;
                break;
            case "Cube 3":
                tCube.gCube3 = gCubeSpawnedGroup1;
                break;
            case "Cube 4":
                tCube.gCube4 = gCubeSpawnedGroup1;
                break;
        }

        switch (gCubeSpawnedGroup2.name)
        {
            case "Cube 1":
                tCube.gCube1 = gCubeSpawnedGroup2;
                break;
            case "Cube 2":
                tCube.gCube2 = gCubeSpawnedGroup2;
                break;
            case "Cube 3":
                tCube.gCube3 = gCubeSpawnedGroup2;
                break;
            case "Cube 4":
                tCube.gCube4 = gCubeSpawnedGroup2;
                break;
        }

        switch (gCubeSpawnedGroup3.name)
        {
            case "Cube 1":
                tCube.gCube1 = gCubeSpawnedGroup3;
                break;
            case "Cube 2":
                tCube.gCube2 = gCubeSpawnedGroup3;
                break;
            case "Cube 3":
                tCube.gCube3 = gCubeSpawnedGroup3;
                break;
            case "Cube 4":
                tCube.gCube4 = gCubeSpawnedGroup3;
                break;
        }
    }


    void CheckRow(GameObject gCubeSpawned)
    {
        CubeProperties tCube;
        tCube = gCubeSpawned.GetComponent<CubeProperties>();

        if (tCube.iRow >= SpawnBorder.iSpawnPosY - 6)
            SpawnTetromino.bGameOver = true;

        TetroDismount.CheckRowComplete(tCube.iRow, tCube.iWall);
    }
}
