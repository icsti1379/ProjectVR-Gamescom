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
    public bool bJustFell = false;
    public bool bGroupSplitted = false;
    public bool bWillFall = false;

    public float pos;

    public bool bInList = false;
    public bool bOnFallList = false;
    public bool bFallingFinished = false;

    public int iLastMovement;

    private void Start()
    {
        iLastMovement = -1;
        fHealth = 1;
        if (iWall == 1 || iWall == 3)
        {
            pos = transform.position.x;
        }
        else
            pos = transform.position.z;
    }

    public void Fall()
    {
        bFallingFinished = false;
        bJustFell = true;
        bIsFalling = true;

        GetComponent<Renderer>().material.color = Color.cyan;

        GetComponent<CubeFall>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;

        if (bInList)
        {
            TetroDismount.lListOfWall(iWall)[iRow - 1].Remove(TetroDismount.lListOfWall(iWall)[iRow - 1].Find(obj => obj.name == name && obj.GetComponent<CubeProperties>().iCubeID == iCubeID));
            bInList = false;
        }

        if (!bGroupSplitted)
        {
            if (gCube1 != null && gameObject != gCube1 && !gCube1.GetComponent<CubeProperties>().bIsFalling && TetroDismount.NeedToFall(gCube1.GetComponent<CubeProperties>(), true))
                gCube1.GetComponent<CubeProperties>().Fall();

            if (gCube2 != null && gameObject != gCube2 && !gCube2.GetComponent<CubeProperties>().bIsFalling && TetroDismount.NeedToFall(gCube1.GetComponent<CubeProperties>(), true))
                gCube2.GetComponent<CubeProperties>().Fall();

            if (gCube3 != null && gameObject != gCube3 && !gCube3.GetComponent<CubeProperties>().bIsFalling && TetroDismount.NeedToFall(gCube1.GetComponent<CubeProperties>(), true))
                gCube3.GetComponent<CubeProperties>().Fall();

            if (gCube4 != null && gameObject != gCube4 && !gCube4.GetComponent<CubeProperties>().bIsFalling && TetroDismount.NeedToFall(gCube1.GetComponent<CubeProperties>(), true))
                gCube4.GetComponent<CubeProperties>().Fall();
        }
    }


    public void SearchVertical()
    {
        try { gCubeAbove = TetroDismount.lListOfWall(iWall)[iRow].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn).SingleOrDefault(); }
        catch { }

        try { TetroDismount.lListOfWall(iWall)[iRow - 2].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn).SingleOrDefault().GetComponent<CubeProperties>().gCubeAbove = gameObject; }
        catch { }

        try { gCubeBeneath = TetroDismount.lListOfWall(iWall)[iRow - 2].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn && obj.GetComponent<CubeProperties>().iCubeID == iCubeID).SingleOrDefault(); }
        catch { }

        try { TetroDismount.lListOfWall(iWall)[iRow].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn && obj.GetComponent<CubeProperties>().iCubeID == iCubeID).SingleOrDefault().GetComponent<CubeProperties>().gCubeBeneath = gameObject; }
        catch { }
    }

    public void SearchHorizontal()
    {
        try { gCubeRight_Group = TetroDismount.lListOfWall(iWall)[iRow - 1].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn + 1 && obj.GetComponent<CubeProperties>().iCubeID == iCubeID).Single(); }
        catch { }

        try { TetroDismount.lListOfWall(iWall)[iRow - 1].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn - 1 && obj.GetComponent<CubeProperties>().iCubeID == iCubeID).SingleOrDefault().GetComponent<CubeProperties>().gCubeRight_Group = gameObject; }
        catch { }

        try { gCubeLeft_Group = TetroDismount.lListOfWall(iWall)[iRow - 1].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn - 1 && obj.GetComponent<CubeProperties>().iCubeID == iCubeID).SingleOrDefault(); }
        catch { }

        try { TetroDismount.lListOfWall(iWall)[iRow - 1].Where(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn + 1 && obj.GetComponent<CubeProperties>().iCubeID == iCubeID).SingleOrDefault().GetComponent<CubeProperties>().gCubeLeft_Group = gameObject; }
        catch { }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Hit();
        }

        else if (bIsFalling && (collision.gameObject.tag == "Plane" || !collision.gameObject.GetComponent<CubeProperties>().bIsFalling))
        {
            GetComponent<Renderer>().material.color = new Color(1,1,0,0.75f);
            GetComponent<CubeFall>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            bIsFalling = false;
            UpdatePosition();

            Vector3 GlobalPos = transform.position;

            if (gCubeAbove != null && gCubeAbove.GetComponent<CubeProperties>().bOnFallList)
                gCubeAbove.GetComponent<CubeProperties>().OnCollisionEnter(collision);

            if (!bGroupSplitted)
            {
                if (gCube1 != null && gameObject != gCube1 && !gCube1.GetComponent<CubeProperties>().bFallingFinished)
                    gCube1.GetComponent<CubeProperties>().OnCollisionEnter(collision);

                if (gCube2 != null && gameObject != gCube2 && !gCube2.GetComponent<CubeProperties>().bFallingFinished)
                    gCube2.GetComponent<CubeProperties>().OnCollisionEnter(collision);

                if (gCube3 != null && gameObject != gCube3 && !gCube3.GetComponent<CubeProperties>().bFallingFinished)
                    gCube3.GetComponent<CubeProperties>().OnCollisionEnter(collision);

                if (gCube4 != null && gameObject != gCube4 && !gCube4.GetComponent<CubeProperties>().bFallingFinished)
                    gCube4.GetComponent<CubeProperties>().OnCollisionEnter(collision);
            }

            iRow = (int)Mathf.Round(Vector3.Distance(GlobalPos, new Vector3(GlobalPos.x, -0.5f, GlobalPos.z)));

            TetroDismount.lListOfWall(iWall)[iRow - 1].Add(gameObject);
            bInList = true;

            if (iRow < TetroDismount.LowestFallPos)
                TetroDismount.LowestFallPos = iRow;

            bFallingFinished = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        OnCollisionEnter(collision);
    }

    public void UpdatePosition()
    {
        float yPosition = Mathf.Round(transform.position.y * 2) / 2;

        float xPosition = Mathf.Round(transform.position.x * 2) / 2;
        float zPosition = Mathf.Round(transform.position.z * 2) / 2;

        transform.position = new Vector3(xPosition, yPosition, zPosition);
        transform.rotation = new Quaternion();
    }

    public void Update()
    {
        if (iWall == 1 || iWall == 3)
        {
            if (pos != transform.position.x)
            {
                transform.position += new Vector3(-transform.position.x + pos, 0);
            }
        }
        else
        {
            if (pos != transform.position.z)
            {
                transform.position += new Vector3(0, 0, -transform.position.z + pos);
            }
        }
    }

    public void Hit()
    {
        fHealth -= 0.25f;
        Changematerial();

        if (tag == "Cube")
            tag = "DamagedCube";

        if (fHealth <= 0)
        {
            try { gCubeAbove.GetComponent<CubeProperties>().gCubeBeneath = null; }
            catch { }
            TetroDismount.lListOfWall(iWall)[iRow - 1].Remove(TetroDismount.lListOfWall(iWall)[iRow - 1].Find(obj => obj.name == name && obj.GetComponent<CubeProperties>().iCubeID == iCubeID));
            Destroy(gameObject);
        }
    }

    public void CheckBugFix()
    {
        if(TetroDismount.lListOfWall(iWall)[iRow - 1].Exists(obj => obj.GetComponent<CubeProperties>().iColumn == iColumn && obj.GetComponent<CubeProperties>().iCubeID != iCubeID))
         {
            Debug.Log("Bug Fixed");
            if (iLastMovement >= 2)
            {
                transform.position += new Vector3(0, 1);
                iRow++;

                if(gCube1 != gameObject)
                {
                    gCube1.transform.position += new Vector3(0, 1);
                    gCube1.GetComponent<CubeProperties>().iRow++;
                }
                if (gCube2 != gameObject)
                {
                    gCube2.transform.position += new Vector3(0, 1);
                    gCube2.GetComponent<CubeProperties>().iRow++;
                }
                if (gCube3 != gameObject)
                {
                    gCube3.transform.position += new Vector3(0, 1);
                    gCube3.GetComponent<CubeProperties>().iRow++;
                }
                if (gCube4 != gameObject)
                {
                    gCube4.transform.position += new Vector3(0, 1);
                    gCube4.GetComponent<CubeProperties>().iRow++;
                }
            }

            else if (iLastMovement == 0)
            {
                iColumn++;
                UpdatePositionProp();

                if (gCube1 != gameObject)
                {
                    gCube1.GetComponent<CubeProperties>().iColumn++;
                    gCube1.GetComponent<CubeProperties>().UpdatePositionProp();
                }
                if (gCube2 != gameObject)
                {
                    gCube2.GetComponent<CubeProperties>().iColumn++;
                    gCube2.GetComponent<CubeProperties>().UpdatePositionProp();
                }
                if (gCube3 != gameObject)
                {
                    gCube3.GetComponent<CubeProperties>().iColumn++;
                    gCube3.GetComponent<CubeProperties>().UpdatePositionProp();
                }
                if (gCube4 != gameObject)
                {
                    gCube4.GetComponent<CubeProperties>().iColumn++;
                    gCube4.GetComponent<CubeProperties>().UpdatePositionProp();
                }
            }

            else if (iLastMovement == 1)
            {
                iColumn--;
                UpdatePositionProp();

                if (gCube1 != gameObject)
                {
                    gCube1.GetComponent<CubeProperties>().iColumn--;
                    gCube1.GetComponent<CubeProperties>().UpdatePositionProp();
                }
                if (gCube2 != gameObject)
                {
                    gCube2.GetComponent<CubeProperties>().iColumn--;
                    gCube2.GetComponent<CubeProperties>().UpdatePositionProp();
                }
                if (gCube3 != gameObject)
                {
                    gCube3.GetComponent<CubeProperties>().iColumn--;
                    gCube3.GetComponent<CubeProperties>().UpdatePositionProp();
                }
                if (gCube4 != gameObject)
                {
                    gCube4.GetComponent<CubeProperties>().iColumn--;
                    gCube4.GetComponent<CubeProperties>().UpdatePositionProp();
                }
            }
        }
    }

    /// <summary>
    /// Updates the Position of the Tetromino (Useful when Column or Wall needs to be changed)
    /// </summary>
    public void UpdatePositionProp()
    {
        float fPoslil = SpawnTetromino.iMapScale / 2 - 0.5f;
        float fPosbig = SpawnTetromino.iMapScale / 2 + 0.5f;

        if (iWall == 1)
        {
            transform.position += new Vector3(-transform.position.x + iColumn - fPosbig, 0, -transform.position.z + fPoslil);
            transform.rotation = new Quaternion();
        }

        // Front
        else if (iWall == 3)
        {
            transform.position += new Vector3(-transform.position.x + fPosbig - iColumn, 0, -transform.position.z + -fPoslil);
            transform.rotation = new Quaternion();
        }

        // Right
        else if (iWall == 2)
        {
            transform.position += new Vector3(-transform.position.x + fPoslil, 0, -transform.position.z + fPosbig - iColumn);
            transform.rotation = new Quaternion();
        }

        // Left
        else if (iWall == 4)
        {
            transform.position += new Vector3(-transform.position.x + -fPoslil, 0, -transform.position.z + iColumn - fPosbig);
            transform.rotation = new Quaternion();
        }
    }

    private void Changematerial()
    {
        GetComponent<Renderer>().material.color = new Color(1, fHealth, fHealth, 1);
    }
}