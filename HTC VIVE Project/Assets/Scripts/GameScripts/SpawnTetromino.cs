using UnityEngine;
using System.Collections;

public class SpawnTetromino : MonoBehaviour
{
    /// <summary>
    /// Tetromino type to be instantiated
    /// </summary>
    GameObject gTetroType;

    /// <summary>
    /// Tetromino PlaceHolder type to be instantiated
    /// </summary>
    GameObject gTetroTypeP;

    /// <summary>
    /// Tetromino Type saved as number
    /// </summary>
    int iTetroType;



    /// <summary>
    /// Tetromino to be instantiated
    /// </summary>
    GameObject gTetroSpawn;

    /// <summary>
    /// Properties of gTetroSpawn
    /// </summary>
    TetroProperties tProperties;


    /// <summary>
    /// Tetromino Placeholder that checks if gTetroSpawn is able to rotate, move left or move right
    /// </summary>
    GameObject gTetroSpawnPMoving;

    /// <summary>
    /// Properties of gTetroSpawnPMoving
    /// </summary>
    TetroHolderProperties tPropertiesPMoving;


    /// <summary>
    /// Tetromino Placeholder that checks if gTetroSpawn will be fixed when falling
    /// </summary>
    GameObject gTetroSpawnPFalling;

    /// <summary>
    /// Properties of gTetroSpawn
    /// </summary>
    TetroHolderProperties tPropertiesPFalling;


    /// <summary>
    /// Unique ID of the Tetromino to be asigned
    /// </summary>
    int iTetroID;


    /// <summary>
    /// Random Column where the Tetromino is spawned
    /// </summary>
    public int iRandomColumn;

    /// <summary>
    /// Random Wall where the Tetromino is spawned
    /// </summary>
    public int iRandomWall;

    /// <summary>
    /// Spawnposition of gTetroSpawn
    /// </summary>
    Vector3 vSpawnPosition;

    /// <summary>
    /// Spawnrotation of gTetroSpawn
    /// </summary>
    Vector3 vSpawnRotation;

    /// <summary>
    /// Y Coordinate of the SpawnPosition
    /// </summary>
    public static int iSpawnPosY;

    /// <summary>
    /// Scale of the Map
    /// </summary>
    public static int iMapScale;


    /// <summary>
    /// Falling speed of the Tetromino, indicates how much time passes between falling
    /// </summary>
    [SerializeField]
    [Range(0.2f, 2)]
    float fFallingSpeed;


    /// <summary>
    /// Necessary to start the Spawn logic, allows first Instance of Tetromino
    /// </summary>
    private bool bFirstCube;

    /// <summary>
    /// True if the last Tetromino spawn has been splitted
    /// </summary>
    public static bool bTetroSplitted;



    // TETROMINOS
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

    [SerializeField]
    GameObject gTetro7;

    [SerializeField]
    GameObject gTetro1P;

    [SerializeField]
    GameObject gTetro2P;

    [SerializeField]
    GameObject gTetro3P;

    [SerializeField]
    GameObject gTetro4P;

    [SerializeField]
    GameObject gTetro5P;

    [SerializeField]
    GameObject gTetro6P;

    [SerializeField]
    GameObject gTetro7P;


    void Start()
    {
        iTetroID = 0;
        bFirstCube = true;
        TetroFall.fSpeed = fFallingSpeed;
        bTetroSplitted = false;
    }


    /// <summary>
    /// Updates PlayerInput and SpawnLogic
    /// </summary>
    void Update()
    {
        if (bFirstCube)
        {
            SpawnNewTetromino();
            bFirstCube = false;
        }

        if (bTetroSplitted && !TetroDismount.bMovingFamiliar)    // Instantiates a new Tetromino if the one that was instantiated before has been splitted
        {
            SpawnNewTetromino();
            bTetroSplitted = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            tPropertiesPMoving = gTetroSpawnPMoving.GetComponent<TetroHolderProperties>();

            if (!tPropertiesPMoving.bMovingRight && !tPropertiesPMoving.bRotating)
                tPropertiesPMoving.LookIfAbleToMove(-1);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            tPropertiesPMoving = gTetroSpawnPMoving.GetComponent<TetroHolderProperties>();

            if(!tPropertiesPMoving.bMovingLeft && !tPropertiesPMoving.bRotating)
                tPropertiesPMoving.LookIfAbleToMove(1);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            tPropertiesPMoving = gTetroSpawnPMoving.GetComponent<TetroHolderProperties>();

            if (!tPropertiesPMoving.bMovingLeft && !tPropertiesPMoving.bMovingRight)
                tPropertiesPMoving.LookIfAbleToRotate();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("\n");
            Debug.Log("Row 7 " + TetroDismount.lListOfWall(tProperties.iWall)[6].Count);
            Debug.Log("Row 6 " + TetroDismount.lListOfWall(tProperties.iWall)[5].Count);
            Debug.Log("Row 5 " + TetroDismount.lListOfWall(tProperties.iWall)[4].Count);
            Debug.Log("Row 4 " + TetroDismount.lListOfWall(tProperties.iWall)[3].Count);
            Debug.Log("Row 3 " + TetroDismount.lListOfWall(tProperties.iWall)[2].Count);
            Debug.Log("Row 2 " + TetroDismount.lListOfWall(tProperties.iWall)[1].Count);
            Debug.Log("Row 1 " + TetroDismount.lListOfWall(tProperties.iWall)[0].Count);
        }
    }

    /// <summary>
    /// Instantiates a new Tetromino on a random wall and column 
    /// </summary>
    void SpawnNewTetromino()
    {
        int iRandomTetro = Random.Range(1, 2);
        iRandomColumn = Random.Range(2, iMapScale);
        iRandomWall = Random.Range(1, 2);

        vSpawnPosition = new Vector3(0, iSpawnPosY + 0.51f, 0);

        if (iRandomTetro == 1)
        {
            gTetroType = gTetro1;
            gTetroTypeP = gTetro1P;
            iTetroType = 1;
        }

        if (iRandomTetro == 2)
        {
            gTetroType = gTetro2;
            gTetroTypeP = gTetro2P;
            iTetroType = 2;
        }

        if (iRandomTetro == 3)
        {
            gTetroType = gTetro3;
            gTetroTypeP = gTetro3P;
            iTetroType = 3;
        }

        if (iRandomTetro == 4)
        {
            gTetroType = gTetro4;
            gTetroTypeP = gTetro4P;
            iTetroType = 4;
        }

        if (iRandomTetro == 5)
        {
            gTetroType = gTetro5;
            gTetroTypeP = gTetro5P;
            iTetroType = 5;
        }

        if (iRandomTetro == 6)
        {
            gTetroType = gTetro6;
            gTetroTypeP = gTetro6P;
            iTetroType = 6;
        }

        if (iRandomTetro == 7)
        {
            gTetroType = gTetro7;
            gTetroTypeP = gTetro7P;
            iTetroType = 7;
        }


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
            vSpawnRotation = new Vector3(0, 90, 0);
        }

        if (iRandomTetro == 3 || iRandomTetro == 7)
            vSpawnRotation -= new Vector3(0, 90, 0);

        gTetroSpawn = (GameObject)Instantiate(gTetroType, vSpawnPosition, new Quaternion());
        gTetroSpawnPMoving = (GameObject)Instantiate(gTetroTypeP, vSpawnPosition, new Quaternion(), gTetroSpawn.transform);
        gTetroSpawnPFalling = (GameObject)Instantiate(gTetroTypeP, vSpawnPosition, new Quaternion(), gTetroSpawn.transform);

        tProperties = gTetroSpawn.GetComponent<TetroProperties>();

        iTetroID++;
        gTetroSpawn.name = "Tetromino " + iTetroID;
        gTetroSpawnPMoving.name = "Tetromino M Placeholder " + iTetroID;
        gTetroSpawnPFalling.name = "Tetromino F Placeholder " + iTetroID;

        tProperties.iTetroID = iTetroID;
        tProperties.iColumn = iRandomColumn;
        tProperties.iWall = iRandomWall;
        tProperties.iType = iTetroType;
        tProperties.vRotation = vSpawnRotation;

        gTetroSpawn.transform.SetParent(transform);
        gTetroSpawn.transform.Rotate(vSpawnRotation);

        tProperties.UpdatePosition();

        tPropertiesPMoving = gTetroSpawnPMoving.GetComponent<TetroHolderProperties>();
        tPropertiesPFalling = gTetroSpawnPFalling.GetComponent<TetroHolderProperties>();

        tProperties.gPMoving = tPropertiesPMoving;
        tProperties.gPFalling = tPropertiesPFalling;

        tPropertiesPMoving.tPropertiesOfSpawn = tProperties;
        tPropertiesPFalling.tPropertiesOfSpawn = tProperties;

        tPropertiesPMoving.SynchroniseWithTetro();
        tPropertiesPFalling.SynchroniseWithTetro();
    }
}