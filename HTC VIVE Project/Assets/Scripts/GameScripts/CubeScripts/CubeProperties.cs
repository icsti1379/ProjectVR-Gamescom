using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public class CubeProperties : MonoBehaviour {

    public int iCubeID;

    public float fHealth = 1;

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
    public bool bJustFell = false;
    public bool bGroupSplitted = false;
    public bool bWillFall = false;

    public bool bInList = false;

    public void Fall()
    {
        if (bJustFell)
            return;

        bool bLoop = true;

        try
        {
            GameObject gBeneathLoop = gCubeBeneath;

            while (bLoop)
            {
                try
                {
                    if (gBeneathLoop.GetComponent<CubeProperties>().gCubeBeneath == null)
                        break;

                    gBeneathLoop = gBeneathLoop.GetComponent<CubeProperties>().gCubeBeneath;

                    if (gBeneathLoop.GetComponent<CubeProperties>().iRow <= 1 && !gBeneathLoop.GetComponent<CubeProperties>().bJustFell)
                        return;
                }
                catch
                {
                    break;
                }
            }
        }
        catch { }

        GetComponent<CubeFall>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;

        if (bInList)
        {
            TetroDismount.lListOfWall(iWall)[iRow - 1].Remove(TetroDismount.lListOfWall(iWall)[iRow - 1].Find(obj => obj.name == name && obj.GetComponent<CubeProperties>().iCubeID == iCubeID));
            bInList = false;
        }

        //if (gCubeAbove != null)
          //  gCubeAbove.GetComponent<CubeProperties>().SearchVertical();

        bJustFell = true;
        bIsFalling = true;

        if (!bGroupSplitted)
        {
            if (gCube1 != null && gameObject != gCube1 && !gCube1.GetComponent<CubeProperties>().bJustFell)
                gCube1.GetComponent<CubeProperties>().Fall();

            if (gCube1 != null && gameObject != gCube2 && !gCube2.GetComponent<CubeProperties>().bJustFell)
                gCube2.GetComponent<CubeProperties>().Fall();

            if (gCube3 != null && gameObject != gCube3 && !gCube3.GetComponent<CubeProperties>().bJustFell)
                gCube3.GetComponent<CubeProperties>().Fall();

            if (gCube4 != null && gameObject != gCube4 && !gCube4.GetComponent<CubeProperties>().bJustFell)
                gCube4.GetComponent<CubeProperties>().Fall();
        }

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
        if (collision.gameObject.tag == "Bullet")
        {
            Hit();
        }

        else if (collision.gameObject.transform.position.y + 0.5f < iRow && bIsFalling && (collision.gameObject.tag == "Plane" || !collision.gameObject.GetComponent<CubeProperties>().bIsFalling))
        {
            bIsFalling = false;

            GetComponent<CubeFall>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            UpdateYPosition();
            Vector3 GlobalPos = transform.position;

            iRow = (int)Mathf.Round(Vector3.Distance(GlobalPos, new Vector3(GlobalPos.x, -0.5f, GlobalPos.z)));

            TetroDismount.lListOfWall(iWall)[iRow - 1].Add(gameObject);
            bInList = true;

            if (iRow == TetroDismount.iCompleted + 1)
                TetroDismount.iCompleted++;

            SearchVertical();
            SearchHorizontal();

            if (!bGroupSplitted)
            {
                if (gCube1 != null && gameObject != gCube1)
                    gCube1.GetComponent<CubeProperties>().OnCollisionEnter(collision);

                if (gCube2 != null && gameObject != gCube2)
                    gCube2.GetComponent<CubeProperties>().OnCollisionEnter(collision);

                if (gCube3 != null && gameObject != gCube3)
                    gCube3.GetComponent<CubeProperties>().OnCollisionEnter(collision);

                if (gCube4 != null && gameObject != gCube4)
                    gCube4.GetComponent<CubeProperties>().OnCollisionEnter(collision);
            }
        }
    }

    public void UpdatePosition()
    {
        float yPosition = Mathf.Round(transform.position.y * 2) / 2;

        float xPosition = Mathf.Round(transform.position.x * 2) / 2;
        float zPosition = Mathf.Round(transform.position.z * 2) / 2;

        transform.position = new Vector3(xPosition, yPosition, zPosition);
        transform.rotation = new Quaternion();
    }

    public void Hit()
    {
        fHealth -= 1;
        if (fHealth <= 0)
        {
            try { gCubeAbove.GetComponent<CubeProperties>().gCubeBeneath = null; }
            catch { }
            //TetroDismount.WillBeDestroyed(iRow, iWall);
            TetroDismount.lListOfWall(iWall)[iRow - 1].Remove(TetroDismount.lListOfWall(iWall)[iRow - 1].Find(obj => obj.name == name && obj.GetComponent<CubeProperties>().iCubeID == iCubeID));
            Destroy(gameObject);
        }
    }
}