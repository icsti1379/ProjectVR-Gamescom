using UnityEngine;
using System.Collections;

public class SpawnTetromino : MonoBehaviour
{
    /// <summary>
    /// Tetromino type to be instantiated
    /// </summary>
    GameObject gTetroType;

    /// <summary>
    /// Tetromino that is beeing instantiated
    /// </summary>
    GameObject gTetroSpawn;

    FixTetromino fFixTetro;

    Vector3 vSpawnPosition;
    Vector3 vSpawnRotation;

    /// <summary>
    /// Y Coordinate of the SpawnPosition
    /// </summary>
    public static int iSpawnPosY;
    public static int iMapScale;

    [SerializeField]
    [Range(0.05f, 0.3f)]
    float fFallingSpeed;

    int iTetroID;

    public int iRandomColumn;
    public int iRandomWall;

    [SerializeField]
    GameObject gTetro1;

    [SerializeField]
    GameObject gTetro2;

    [SerializeField]
    GameObject gTetro3;

    [SerializeField]
    GameObject gTetro4;

    [SerializeField]
    GameObject gTetro5;

    [SerializeField]
    GameObject gTetro6;

    /// <summary>
    /// Necessary to start the Spawn logic, allows first Instance
    /// </summary>
    bool bFirstCube;


    void Start()
    {
        iTetroID = 0;
        bFirstCube = true;
        TetroFall.fSpeed = fFallingSpeed;
    }

    void Update()
    {
        if (bFirstCube)
        {
            SpawnNewTetromino();
            bFirstCube = false;
        }

        Rigidbody rLastSpawned = gTetroSpawn.GetComponent<Rigidbody>();

        if(rLastSpawned.isKinematic)    // Instantiates a new Tetromino if the one that was instantiated before is kinematic (=has touched the ground)
            SpawnNewTetromino();
    }

    /// <summary>
    /// Instantiates a new Tetromino in a random Wall and Column 
    /// </summary>
    void SpawnNewTetromino()
    {
        int iRandomTetro = Random.Range(1, 7);

        if (iRandomTetro == 1)
            gTetroType = gTetro1;

        if (iRandomTetro == 2)
            gTetroType = gTetro2;

        if (iRandomTetro == 3)
            gTetroType = gTetro3;

        if (iRandomTetro == 4)
            gTetroType = gTetro4;

        if (iRandomTetro == 5)
            gTetroType = gTetro5;

        if (iRandomTetro == 6)
            gTetroType = gTetro6;


        iRandomColumn = Random.Range(2, iMapScale - 1);
        iRandomWall = Random.Range(1, 5);

        vSpawnPosition = new Vector3(0, iSpawnPosY, 0);

        // Back
        if (iRandomWall == 1)
        {
            vSpawnPosition += new Vector3(iRandomColumn - (iMapScale / 2 + 0.5f), 0, (iMapScale / 2 - 0.5f));
            vSpawnRotation = new Vector3();
        }

        // Front
        else if (iRandomWall == 3)
        {
            vSpawnPosition += new Vector3((iMapScale / 2 + 0.5f) - iRandomColumn, 0, -(iMapScale / 2 - 0.5f));
            vSpawnRotation = new Vector3();
        }

        // Right
        else if (iRandomWall == 2)
        {
            vSpawnPosition += new Vector3((iMapScale / 2 - 0.5f), 0, (iMapScale / 2 + 0.5f) - iRandomColumn);
            vSpawnRotation = new Vector3(0, 90, 0);
        }

        // Left
        else if (iRandomWall == 4)
        {
            vSpawnPosition += new Vector3(-(iMapScale / 2 - 0.5f), 0, iRandomColumn - (iMapScale / 2 + 0.5f));
            vSpawnRotation = new Vector3(0, -90, 0);
        }


        gTetroSpawn = (GameObject)Instantiate(gTetroType, vSpawnPosition, new Quaternion());
        fFixTetro = gTetroSpawn.GetComponent<FixTetromino>();

        gTetroSpawn.transform.SetParent(transform);
        gTetroSpawn.transform.Rotate(vSpawnRotation);

        if (gTetroType == gTetro3)
            gTetroSpawn.transform.Rotate(new Vector3(90,0,0));

        iTetroID++;
        gTetroSpawn.name = "Tetromino " + iTetroID;

        fFixTetro.iCubeID = iTetroID;
        fFixTetro.iColumn = iRandomColumn;
        fFixTetro.iWall = iRandomWall;
        fFixTetro.sType = gTetroType.name;
    }
}
