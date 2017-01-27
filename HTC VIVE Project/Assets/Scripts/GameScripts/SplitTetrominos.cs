using UnityEngine;
using System.Collections;

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

        tProperties.CalculateCubes();

        SpawnCube(tProperties.vPositionCube, 1);
        SpawnCube(tProperties.vPositionCube2, 2);
        SpawnCube(tProperties.vPositionCube3, 3);
        SpawnCube(tProperties.vPositionCube4, 4);

        if (tProperties.iWall == 2)
            gEmptySpawned.transform.Rotate(new Vector3(0, 90));

        else if (tProperties.iWall == 4)
            gEmptySpawned.transform.Rotate(new Vector3(0, -90));

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

        float yPos = gCubeSpawned.transform.position.y + gEmptySpawned.transform.position.y - 0.5f;

        tCube.iRow = (int)yPos + 1 + (int)gCubeSpawned.transform.position.y;

        Rigidbody rb;
        rb = gCubeSpawned.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
}
