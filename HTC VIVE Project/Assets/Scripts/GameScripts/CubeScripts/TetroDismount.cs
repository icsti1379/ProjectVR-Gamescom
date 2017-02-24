using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetroDismount : MonoBehaviour {

    public static List<List<GameObject>> CubeCollection1;
    public static List<List<GameObject>> CubeCollection2;
    public static List<List<GameObject>> CubeCollection3;
    public static List<List<GameObject>> CubeCollection4;

    public static List<List<GameObject>> lCubesToFall;

    public static int LowestRowToMove;
    public static int HighestRowToMove;

    public static bool bDismountInProcess = false;

    public static bool bInitiated = false;

    static int iWallToCheck;

    static bool bInitialiseFalling = false;

    public static int LowestFallPos = SpawnBorder.iSpawnPosY;


    private void Start()
    {
        CubeCollection1 = new List<List<GameObject>>();
        CubeCollection2 = new List<List<GameObject>>();
        CubeCollection3 = new List<List<GameObject>>();
        CubeCollection4 = new List<List<GameObject>>();
        lCubesToFall = new List<List<GameObject>>();

        for (int x = 1; x < SpawnBorder.iSpawnPosY - 2; x++)
        {
            CubeCollection1.Add(new List<GameObject>());
            CubeCollection2.Add(new List<GameObject>());
            CubeCollection3.Add(new List<GameObject>());
            CubeCollection4.Add(new List<GameObject>());
            lCubesToFall.Add(new List<GameObject>());
        }
    }

    private static void LeftRight(CubeProperties Cproperties, CubeProperties Cproperties1)
    {
        try
        {
            GameObject vCubeRight = Cproperties.gCubeRight_Group;

            for (int a = 0; a < 3; a++)
            {
                CubeProperties CpropertiesRight = vCubeRight.GetComponent<CubeProperties>();

                if (!lCubesToFall[CpropertiesRight.iRow - 1].Exists(obj => obj == vCubeRight) && NeedToFall(CpropertiesRight, false))
                {
                    lCubesToFall[CpropertiesRight.iRow - 1].Add(vCubeRight);
                    CpropertiesRight.bOnFallList = true;
                }

                Above(CpropertiesRight);
                Beneath(CpropertiesRight);

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

                if (!lCubesToFall[CpropertiesLeft.iRow - 1].Exists(obj => obj == vCubeLeft) && NeedToFall(CpropertiesLeft, false))
                {
                    lCubesToFall[CpropertiesLeft.iRow - 1].Add(vCubeLeft);
                    CpropertiesLeft.bOnFallList = true;
                }

                Above(CpropertiesLeft);
                Beneath(CpropertiesLeft);

                try { vCubeLeft = CpropertiesLeft.gCubeLeft_Group; }
                catch { break; }
            }
        }
        catch { }
    }

    private static void Above(CubeProperties cCubeAbove)
    {
        try
        {
            GameObject vCubeAbove = cCubeAbove.gCubeAbove;
            CubeProperties Cproperties1 = vCubeAbove.GetComponent<CubeProperties>();

            for (int y = 1; y < SpawnBorder.iSpawnPosY; y++)
            {
                CubeProperties Cproperties = vCubeAbove.GetComponent<CubeProperties>();

                if (HighestRowToMove < Cproperties.iRow)
                    HighestRowToMove = Cproperties.iRow;

                if (!lCubesToFall[Cproperties.iRow - 1].Exists(obj => obj == vCubeAbove) && NeedToFall(Cproperties, true))
                {
                    lCubesToFall[Cproperties.iRow - 1].Add(vCubeAbove);
                    Cproperties.bOnFallList = true;
                }

                LeftRight(Cproperties, Cproperties1);

                try { vCubeAbove = Cproperties.gCubeAbove; }
                catch { break; }
            }
        }
        catch { }
    }


    private static void Beneath(CubeProperties cCubeBeneath)
    {
        try
        {
            GameObject vCubeBeneath = cCubeBeneath.gCubeBeneath;
            CubeProperties Cproperties1 = vCubeBeneath.GetComponent<CubeProperties>();

            for (int y = 1; y < vCubeBeneath.GetComponent<CubeProperties>().iRow; y++)
            {
                CubeProperties Cproperties = vCubeBeneath.GetComponent<CubeProperties>();

                if (LowestRowToMove > Cproperties.iRow)
                    LowestRowToMove = Cproperties.iRow;

                if (!lCubesToFall[Cproperties.iRow - 1].Exists(obj => obj == vCubeBeneath) && NeedToFall(Cproperties, false))
                {
                    lCubesToFall[Cproperties.iRow - 1].Add(vCubeBeneath);
                    Cproperties.bOnFallList = true;
                }

                try { vCubeBeneath = Cproperties.gCubeBeneath; }
                catch { break; }
            }
        }
        catch { }
    }


    private static void AddToFallingList(int iRow, int iWall)
    {
        GameObject gObjectInRow;

        LowestRowToMove = iRow;
        HighestRowToMove = iRow;

        iWallToCheck = iWall;

        for (int x = 0; x < lListOfWall(iWall)[iRow - 1].Count; x++)
        {
            gObjectInRow = lListOfWall(iWall)[iRow - 1][x];
            CubeProperties cObjectInRow = gObjectInRow.GetComponent<CubeProperties>();

            if(cObjectInRow.gCube1 != null)
                cObjectInRow.gCube1.GetComponent<CubeProperties>().bGroupSplitted = true;

            if (cObjectInRow.gCube2 != null)
                cObjectInRow.gCube2.GetComponent<CubeProperties>().bGroupSplitted = true;

            if (cObjectInRow.gCube3 != null)
                cObjectInRow.gCube3.GetComponent<CubeProperties>().bGroupSplitted = true;

            if (cObjectInRow.gCube4 != null)
                cObjectInRow.gCube4.GetComponent<CubeProperties>().bGroupSplitted = true;

            if (!NeedToFall(cObjectInRow, true))
                continue;

            if (!lCubesToFall[cObjectInRow.iRow - 1].Exists(obj => obj == gObjectInRow))
            {
                lCubesToFall[iRow - 1].Add(gObjectInRow);
                cObjectInRow.bOnFallList = true;
            }

            Beneath(lListOfWall(iWall)[iRow - 1][x].GetComponent<CubeProperties>());
            Above(lListOfWall(iWall)[iRow - 1][x].GetComponent<CubeProperties>());

            bDismountInProcess = true;

            for (int q = 0; q < lCubesToFall[iRow - 1].Count; q++)
                lCubesToFall[iRow - 1][q].GetComponent<CubeProperties>().bGroupSplitted = true;
        }
    }

    public void Update()
    {
        if (bDismountInProcess)
        {
            if (!bInitialiseFalling)
            {
                for (int x = LowestRowToMove - 1; x < lCubesToFall.Count; x++)
                {
                    for (int y = 0; y < lCubesToFall[x].Count; y++)
                    {
                        lCubesToFall[x][y].GetComponent<CubeProperties>().bIsFalling = true;
                    }
                }
                bInitialiseFalling = true;
            }

            ApplyFallingList();

            for (int x = LowestRowToMove - 1; x < lCubesToFall.Count; x++)
            {
                for (int y = 0; y < lCubesToFall[x].Count; y++)
                {
                    if (!lCubesToFall[x][y].GetComponent<CubeProperties>().bFallingFinished)
                        return;
                }             
            }

            bDismountInProcess = false;
            bInitialiseFalling = false;

            for (int x = LowestRowToMove - 1; x < lCubesToFall.Count; x++)
            {
                for (int y = 0; y < lCubesToFall[x].Count; y++)
                {
                    CubeProperties cProperties = lCubesToFall[x][y].GetComponent<CubeProperties>();
                    cProperties.bJustFell = false;
                    cProperties.bOnFallList = false;
                    cProperties.SearchVertical();
                    cProperties.SearchHorizontal();
                }
            }

            lCubesToFall = new List<List<GameObject>>();

            for (int z = 1; z < SpawnBorder.iSpawnPosY - 2; z++)
                lCubesToFall.Add(new List<GameObject>());

            for (int c = LowestFallPos - 1; c < HighestRowToMove; c++)
            {
                if (CheckRowComplete(c, iWallToCheck))
                    break;
            }
        }
    }

    private void ApplyFallingList()
    {
        for (int x = LowestRowToMove - 1; x < lCubesToFall.Count; x++)
        {
            for (int y = 0; y < lCubesToFall[x].Count; y++)
            {
                if (!lCubesToFall[x][y].GetComponent<CubeProperties>().bJustFell)
                    lCubesToFall[x][y].GetComponent<CubeProperties>().Fall();
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
                    AddToFallingList(iRow, iWall);
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

    public static bool NeedToFall(CubeProperties tCube, bool bSides)
    {
        bool bLoop = true;

        try
        {
            GameObject gBeneathLoop = tCube.gCubeBeneath;

            while (bLoop && gBeneathLoop.GetComponent<CubeProperties>().iRow > 1)
            {
                try
                {
                    if (bSides)
                    {
                        if (!gBeneathLoop.GetComponent<CubeProperties>().bOnFallList && gBeneathLoop != tCube.gCube1 && gBeneathLoop != tCube.gCube2 && gBeneathLoop != tCube.gCube3 && gBeneathLoop != tCube.gCube4)
                            return false;
                    }

                    gBeneathLoop = gBeneathLoop.GetComponent<CubeProperties>().gCubeBeneath;
                }
                catch
                {
                    return true;
                }
            }
        }
        catch
        {
            return true;
        }
        return false;
    }
}
