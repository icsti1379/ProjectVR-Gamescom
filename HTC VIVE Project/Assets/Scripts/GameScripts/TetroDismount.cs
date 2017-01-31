using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetroDismount : MonoBehaviour {

    public static List<List<GameObject>> CubeCollection1;
    public static List<List<GameObject>> CubeCollection2;
    public static List<List<GameObject>> CubeCollection3;
    public static List<List<GameObject>> CubeCollection4;


    public void Start()
    {
        CubeCollection1 = new List<List<GameObject>>();
        CubeCollection2 = new List<List<GameObject>>();
        CubeCollection3 = new List<List<GameObject>>();
        CubeCollection4 = new List<List<GameObject>>();


        for (int x = 1; x < SpawnBorder.iSpawnPosY - 2; x++)
        {
            CubeCollection1.Add(new List<GameObject>());
            CubeCollection2.Add(new List<GameObject>());
            CubeCollection3.Add(new List<GameObject>());
            CubeCollection4.Add(new List<GameObject>());
        }
    }

    public static void Dismount(int iRow, int iWall)
    {
        List<int> lIndexToRemove = new List<int>();

        for (int x = 0; x < lListOfWall(iWall)[iRow - 1].Count; x++)
        {
            lListOfWall(iWall)[iRow - 1][x].GetComponent<TetroFall>().enabled = true;
            lListOfWall(iWall)[iRow - 1][x].GetComponent<Rigidbody>().isKinematic = false;
            lIndexToRemove.Add(x);
        }
        for (int x = lIndexToRemove.Count - 1; x >= 0; x--)
            lListOfWall(iWall)[iRow - 1].RemoveAt(lIndexToRemove[x]);
    }


    public static void CheckComplete(int iRow, int iWall)
    {
        if (iRow != 1)
        {
            if (lListOfWall(iWall)[iRow - 1].Count >= (SpawnBorder.iMapScale - 2))
                Dismount(iRow, iWall);
        }
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
