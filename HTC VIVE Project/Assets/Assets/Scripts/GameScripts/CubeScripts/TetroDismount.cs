using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetroDismount : MonoBehaviour {

    public static List<List<GameObject>> CubeCollection1;
    public static List<List<GameObject>> CubeCollection2;
    public static List<List<GameObject>> CubeCollection3;
    public static List<List<GameObject>> CubeCollection4;

    public static List<List<GameObject>> FamiliarCubes;

    public static int iCompleted = 1;

    public static float fTime = 0.3f;

    int iRepeating = 0;

    public static int LowestRowToMove;
    public static int HighestRowToMove;

    public static bool bMovingFamiliar = false;
    public static bool bInitiated = false;

    static int iWallToCheck;


    public void Start()
    {
        CubeCollection1 = new List<List<GameObject>>();
        CubeCollection2 = new List<List<GameObject>>();
        CubeCollection3 = new List<List<GameObject>>();
        CubeCollection4 = new List<List<GameObject>>();
        FamiliarCubes = new List<List<GameObject>>();

        for (int x = 1; x < SpawnBorder.iSpawnPosY - 2; x++)
        {
            CubeCollection1.Add(new List<GameObject>());
            CubeCollection2.Add(new List<GameObject>());
            CubeCollection3.Add(new List<GameObject>());
            CubeCollection4.Add(new List<GameObject>());
            FamiliarCubes.Add(new List<GameObject>());
        }
    }

    public static void LeftRight(CubeProperties Cproperties, CubeProperties Cproperties1)
    {
        try
        {
            GameObject vCubeRight = Cproperties.gCubeRight_Group;

            for (int a = 0; a < 3; a++)
            {
                CubeProperties CpropertiesRight = vCubeRight.GetComponent<CubeProperties>();

                if (CpropertiesRight.iCubeID == Cproperties1.iCubeID)
                    FamiliarCubes[CpropertiesRight.iRow - 1].Add(vCubeRight);

                try { vCubeRight = CpropertiesRight.gCubeRight_Group; }
                catch { break; }
            }
        }
        catch { }

        try
        {
            GameObject vCubeLeft = Cproperties.gCubeLeft_Group;

            for (int a = 0; a < 3; a++)
            {
                CubeProperties CpropertiesLeft = vCubeLeft.GetComponent<CubeProperties>();

                if (CpropertiesLeft.iCubeID == Cproperties1.iCubeID)
                    FamiliarCubes[CpropertiesLeft.iRow - 1].Add(vCubeLeft);

                try { vCubeLeft = CpropertiesLeft.gCubeLeft_Group; }
                catch { break; }
            }
        }
        catch { }
    }


    public static void Dismount(int iRow, int iWall)
    {
        List<int> lIndexToRemove = new List<int>();

        LowestRowToMove = iRow;
        HighestRowToMove = iRow;

        iWallToCheck = iWall;

        for (int x = 0; x < lListOfWall(iWall)[iRow - 1].Count; x++)
        {
            GameObject gObjectInRow = lListOfWall(iWall)[iRow - 1][x];
            CubeProperties cObjectInRow = gObjectInRow.GetComponent<CubeProperties>();

            FamiliarCubes[iRow - 1].Add(gObjectInRow);

            try
            {
                GameObject vCubeBeneath = lListOfWall(iWall)[iRow - 1][x].GetComponent<CubeProperties>().gCubeBeneath;

                for (int y = 1; y < vCubeBeneath.GetComponent<CubeProperties>().iRow; y++)
                {
                    CubeProperties Cproperties = vCubeBeneath.GetComponent<CubeProperties>();

                    if (LowestRowToMove > Cproperties.iRow)
                        LowestRowToMove = Cproperties.iRow;

                    if (HighestRowToMove < Cproperties.iRow)
                        HighestRowToMove = Cproperties.iRow;

                    FamiliarCubes[Cproperties.iRow - 1].Add(vCubeBeneath);

                    //LeftRight(iWall, iRow, x);

                    try { vCubeBeneath = Cproperties.gCubeBeneath; }
                    catch { break; }
                }
            }
            catch { }

            try
            {
                GameObject vCubeAbove = lListOfWall(iWall)[iRow - 1][x].GetComponent<CubeProperties>().gCubeAbove;
                CubeProperties Cproperties1 = vCubeAbove.GetComponent<CubeProperties>();

                for (int y = 1; y < SpawnBorder.iSpawnPosY; y++)
                {
                    CubeProperties Cproperties = vCubeAbove.GetComponent<CubeProperties>();

                    if (LowestRowToMove > Cproperties.iRow)
                        LowestRowToMove = Cproperties.iRow;

                    if (HighestRowToMove < Cproperties.iRow)
                        HighestRowToMove = Cproperties.iRow;

                    FamiliarCubes[Cproperties.iRow - 1].Add(vCubeAbove);

                    LeftRight(Cproperties, Cproperties1);

                    try { vCubeAbove = Cproperties.gCubeAbove; }
                    catch { break; }
                }
            }
            catch { }

            bMovingFamiliar = true;

            lIndexToRemove.Add(x);
        }
    }

    public void Update()
    {
        if (bMovingFamiliar)
        {
            fTime += Time.deltaTime;
            if (!bInitiated)
            {
                iRepeating = LowestRowToMove - 1;
                bInitiated = true;
            }
        }

        if (fTime > 0.3f)
            MoveFamiliar();
    }

    public void MoveFamiliar()
    {
        for (int y = 0; y < FamiliarCubes[iRepeating].Count; y++)
            FamiliarCubes[iRepeating][y].GetComponent<CubeProperties>().Fall();

        iRepeating++;
        fTime = 0;

        if (iRepeating > HighestRowToMove)
        {
            FamiliarCubes = new List<List<GameObject>>();
            fTime = 0.3f;
            bInitiated = false;
            bMovingFamiliar = false;

            for (int z = 1; z < SpawnBorder.iSpawnPosY - 2; z++)
                FamiliarCubes.Add(new List<GameObject>());

            for (int c = 1; c < HighestRowToMove; c++)
            {
                if (CheckRowComplete(c, iWallToCheck))
                    break;
            }
        }
    }

    public static bool CheckRowComplete(int iRow, int iWall)
    {
        if (iRow > 1)
        {
            bool bDismount = false;

            if (lListOfWall(iWall)[iRow - 1].Count >= (SpawnBorder.iMapScale - 2))
            {
                for (int x = iRow - 1; x > 0; x--)
                {
                    if (lListOfWall(iWall)[x - 1].Count < (SpawnBorder.iMapScale - 2))
                    {
                        bDismount = true;
                        break;
                    }
                }

                if (bDismount)
                {
                    Dismount(iRow, iWall);
                    return true;
                }
            }
        }
        return false;
    }

    public static List<List<GameObject>> lListOfWall(int iWall)
    {
        if (iWall == 1)
            return CubeCollection1;

        if (iWall == 2)
            return CubeCollection2;

        if (iWall == 3)
            return CubeCollection3;

        if (iWall == 4)
            return CubeCollection4;

        return null;
    }
}
