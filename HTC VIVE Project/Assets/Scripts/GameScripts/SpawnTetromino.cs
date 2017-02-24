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

    public GameObject gBorderPlacement1;

    public GameObject gBorderPlacement2;

    public GameObject gBorderPlacement3;

    public GameObject gBorderPlacement4;

    public static GameObject gBorderPlacement11;

    public static GameObject gBorderPlacement22;

    public static GameObject gBorderPlacement33;

    public static GameObject gBorderPlacement44;




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
    [Range(0.05f, 2)]
    float fFallingSpeed;


    /// <summary>
    /// Necessary to start the Spawn logic, allows first Instance of Tetromino
    /// </summary>
    private bool bFirstCube;

    /// <summary>
    /// True if the last Tetromino spawn has been splitted
    /// </summary>
    public static bool bTetroSplitted;

    public static bool bGameOver = false;



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

    int iDelayedMovement;
    float fNormalSpeed;


    void Start()
    {
        iDelayedMovement = -1;
        gBorderPlacement11 = gBorderPlacement1;
        gBorderPlacement22 = gBorderPlacement2;
        gBorderPlacement33 = gBorderPlacement3;
        gBorderPlacement44 = gBorderPlacement4;

        iTetroID = 0;
        bFirstCube = true;
        //TetroFall.fSpeed = fFallingSpeed;
        //fNormalSpeed = fFallingSpeed;
        fNormalSpeed = TetroFall.fSpeed;
        bTetroSplitted = false;
    }


    /// <summary>
    /// Updates PlayerInput and SpawnLogic
    /// </summary>
    void Update()
    {
        DelayedMovement();

        if (bFirstCube)
        {
            SpawnNewTetromino();
            bFirstCube = false;
        }

        if (!bGameOver && bTetroSplitted && !TetroDismount.bDismountInProcess)    // Instantiates a new Tetromino if the one that was instantiated before has been splitted
        {
            SpawnNewTetromino();
            bTetroSplitted = false;
        }


        if (gTetroSpawn != null)
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                tPropertiesPMoving = gTetroSpawnPMoving.GetComponent<TetroHolderProperties>();

                if (!tPropertiesPMoving.bMovingRight && !tPropertiesPMoving.bRotating)
                {
                    if (!tPropertiesPFalling.bFalling)
                        tPropertiesPMoving.LookIfAbleToMove(-1);
                    else
                        iDelayedMovement = 0;
                }
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                tPropertiesPMoving = gTetroSpawnPMoving.GetComponent<TetroHolderProperties>();

                if (!tPropertiesPMoving.bMovingLeft && !tPropertiesPMoving.bRotating)
                {
                    if (!tPropertiesPFalling.bFalling)
                        tPropertiesPMoving.LookIfAbleToMove(1);
                    else
                        iDelayedMovement = 1;
                }
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                tPropertiesPMoving = gTetroSpawnPMoving.GetComponent<TetroHolderProperties>();

                if (!tPropertiesPMoving.bMovingLeft && !tPropertiesPMoving.bMovingRight)
                {
                    if (!tPropertiesPFalling.bFalling)
                        tPropertiesPMoving.LookIfAbleToRotate();
                    else
                        iDelayedMovement = 2;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            fFallingSpeed = fNormalSpeed / 4;
            TetroFall.fSpeed = fNormalSpeed / 4;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            fFallingSpeed = fNormalSpeed;
            TetroFall.fSpeed = fNormalSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            tPropertiesPMoving = gTetroSpawnPMoving.GetComponent<TetroHolderProperties>();
            tPropertiesPMoving.Log();

            /*
            Debug.Log("\n");
            Debug.Log("Row 10 " + TetroDismount.lListOfWall(tProperties.iWall)[9].Count);
            Debug.Log("Row 9 " + TetroDismount.lListOfWall(tProperties.iWall)[8].Count);
            Debug.Log("Row 8 " + TetroDismount.lListOfWall(tProperties.iWall)[7].Count);
            Debug.Log("Row 7 " + TetroDismount.lListOfWall(tProperties.iWall)[6].Count);
            Debug.Log("Row 6 " + TetroDismount.lListOfWall(tProperties.iWall)[5].Count);
            Debug.Log("Row 5 " + TetroDismount.lListOfWall(tProperties.iWall)[4].Count);
            Debug.Log("Row 4 " + TetroDismount.lListOfWall(tProperties.iWall)[3].Count);
            Debug.Log("Row 3 " + TetroDismount.lListOfWall(tProperties.iWall)[2].Count);
            Debug.Log("Row 2 " + TetroDismount.lListOfWall(tProperties.iWall)[1].Count);
            Debug.Log("Row 1 " + TetroDismount.lListOfWall(tProperties.iWall)[0].Count);
            */
        }
    }

    public void DelayedMovement()
    {
        if (iDelayedMovement != -1 && gTetroSpawn != null)
        {
            tPropertiesPMoving = gTetroSpawnPMoving.GetComponent<TetroHolderProperties>();

            if (iDelayedMovement == 0)
            {
                if (!tPropertiesPMoving.bMovingRight && !tPropertiesPMoving.bRotating)
                {
                    if (!tPropertiesPFalling.bFalling)
                    {
                        tPropertiesPMoving.LookIfAbleToMove(-1);
                        iDelayedMovement = -1;
                    }
                    else
                        iDelayedMovement = 0;
                }
            }
            if (iDelayedMovement == 1)
            {
                if (!tPropertiesPMoving.bMovingLeft && !tPropertiesPMoving.bRotating)
                {
                    if (!tPropertiesPFalling.bFalling)
                    {
                        tPropertiesPMoving.LookIfAbleToMove(1);
                        iDelayedMovement = -1;
                    }
                    else
                        iDelayedMovement = 1;
                }
            }
            if (iDelayedMovement == 2)
            {
                if (!tPropertiesPMoving.bMovingLeft && !tPropertiesPMoving.bMovingRight)
                {
                    if (!tPropertiesPFalling.bFalling)
                    {
                        tPropertiesPMoving.LookIfAbleToRotate();
                        iDelayedMovement = -1;
                    }
                    else
                        iDelayedMovement = 2;
                }
            }
        }
    }

    /// <summary>
    /// Instantiates a new Tetromino on a random wall and column 
    /// </summary>
    void SpawnNewTetromino()
    {
        int iRandomTetro = Random.Range(1, 8);
        iRandomColumn = Random.Range(2, 10);
        iRandomWall = Random.Range(1, 5);

        vSpawnPosition = new Vector3(0, iSpawnPosY + 0.50f, 0);

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
        tPropertiesPMoving.tPropertiesOfFall = tPropertiesPFalling;
        tPropertiesPFalling.tPropertiesOfSpawn = tProperties;

        tPropertiesPMoving.SynchroniseWithTetro();
        tPropertiesPFalling.SynchroniseWithTetro();

        tProperties.ReplaceBorder();
    }
}