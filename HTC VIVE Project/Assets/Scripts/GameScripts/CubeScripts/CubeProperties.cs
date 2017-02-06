using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public class CubeProperties : MonoBehaviour {

    public int iCubeID;

    public float fHealth;

    public int iColumn;
    public int iWall;
    public int iRow;
    public int iTetroType;
    public Vector3 GlobalPosition;

    public Color cColor;

    public GameObject gCubeAbove;
    public GameObject gCubeBeneath;
    public GameObject gCubeRight_Group;
    public GameObject gCubeLeft_Group;

    public GameObject gCube1;
    public GameObject gCube2;
    public GameObject gCube3;
    public GameObject gCube4;

    public bool bIsFalling = false;

    public void Fall()
    {
        GetComponent<CubeFall>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;

        TetroDismount.lListOfWall(iWall)[iRow - 1].Remove(  TetroDismount.lListOfWall(iWall)[iRow - 1].Find(obj => obj.name == name && obj.GetComponent<CubeProperties>().iCubeID == iCubeID));

        bIsFalling = true;
        UpdatePosition();
    }


    public void SearchVertical()
    {
        try { gCubeAbove = TetroDismount.lListOfWall(iWall)[iRow].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn).SingleOrDefault(); }
        catch { }

        try { TetroDismount.lListOfWall(iWall)[iRow - 2].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn).SingleOrDefault().GetComponent<CubeProperties>().gCubeAbove = gameObject; }
        catch { }

        try { gCubeBeneath = TetroDismount.lListOfWall(iWall)[iRow - 2].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn).SingleOrDefault(); }
        catch { }

        try { TetroDismount.lListOfWall(iWall)[iRow].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn).SingleOrDefault().GetComponent<CubeProperties>().gCubeBeneath = gameObject; }
        catch { }
    }

    public void SearchHorizontal()
    {
        try { gCubeRight_Group = TetroDismount.lListOfWall(iWall)[iRow - 1].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn + 1 && obj.GetComponent<CubeProperties>().iCubeID == iCubeID).Single(); }
        catch { }

        try { TetroDismount.lListOfWall(iWall)[iRow - 1].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn - 1 && obj.GetComponent<CubeProperties>().iCubeID == iCubeID).SingleOrDefault().GetComponent<CubeProperties>().gCubeRight_Group = gameObject; }
        catch { }

        if (iRow > 0)
            gCubeLeft_Group = TetroDismount.lListOfWall(iWall)[iRow - 1].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn - 1 && obj.GetComponent<CubeProperties>().iCubeID == iCubeID).SingleOrDefault();

        try { TetroDismount.lListOfWall(iWall)[iRow - 1].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn + 1 && obj.GetComponent<CubeProperties>().iCubeID == iCubeID).SingleOrDefault().GetComponent<CubeProperties>().gCubeLeft_Group = gameObject; }
        catch { }
    }

    public void UpdateYPosition()
    {
        float yPosition = Mathf.Round(transform.position.y);

        float xPosition = Mathf.Round(transform.position.x * 2) / 2;
        float zPosition = Mathf.Round(transform.position.z * 2) / 2;

        transform.position = new Vector3(xPosition, yPosition + 0.5f, zPosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        try
        {
            if (collision.gameObject.transform.position.y + 0.5f < iRow && bIsFalling && (collision.gameObject.tag == "Plane" || !collision.gameObject.GetComponent<CubeProperties>().bIsFalling))
            {
                bIsFalling = false;

                GetComponent<CubeFall>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
                UpdateYPosition();
                Vector3 GlobalPos = transform.position;

                iRow = (int)Mathf.Round(Vector3.Distance(GlobalPos, new Vector3(GlobalPos.x, -0.5f, GlobalPos.z)));

                TetroDismount.lListOfWall(iWall)[iRow - 1].Add(gameObject);

                if (iRow == TetroDismount.iCompleted + 1)
                    TetroDismount.iCompleted++;
            
                SearchVertical();
                SearchHorizontal();
            }
        }
        catch { }
    }

    public void UpdatePosition()
    {
        float yPosition = Mathf.Round(transform.position.y * 2) / 2;

        float xPosition = Mathf.Round(transform.position.x * 2) / 2;
        float zPosition = Mathf.Round(transform.position.z * 2) / 2;

        transform.position = new Vector3(xPosition, yPosition, zPosition);
        transform.rotation = new Quaternion();
    }

    public List<GameObject> GetHighestCube()
    {
        List<GameObject> Result = new List<GameObject>();

        if (gCube1.name != name && gCube1.GetComponent<CubeProperties>().iColumn != iColumn && gCube1.transform.position.y >= gCube2.transform.position.y && gCube1.transform.position.y >= gCube3.transform.position.y && gCube1.transform.position.y >= gCube4.transform.position.y)
            Result.Add(gCube1);

        if (gCube2.name != name && gCube2.GetComponent<CubeProperties>().iColumn != iColumn && gCube2.transform.position.y >= gCube1.transform.position.y && gCube2.transform.position.y >= gCube3.transform.position.y && gCube2.transform.position.y >= gCube4.transform.position.y)
            Result.Add(gCube2);

        if (gCube3.name != name && gCube3.GetComponent<CubeProperties>().iColumn != iColumn && gCube3.transform.position.y >= gCube1.transform.position.y && gCube3.transform.position.y >= gCube2.transform.position.y && gCube3.transform.position.y >= gCube4.transform.position.y)
            Result.Add(gCube3);

        if (gCube4.name != name && gCube4.GetComponent<CubeProperties>().iColumn != iColumn && gCube4.transform.position.y >= gCube2.transform.position.y && gCube4.transform.position.y >= gCube3.transform.position.y && gCube4.transform.position.y >= gCube1.transform.position.y)
            Result.Add(gCube4);

        return Result;
    }
}