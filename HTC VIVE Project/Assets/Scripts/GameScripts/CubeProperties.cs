using UnityEngine;
using System.Collections;

public class CubeProperties : MonoBehaviour {

    public int iCubeID;

    public float fHealth;

    public int iColumn;
    public int iWall;
    public int iRow;
    public int iTetroType;
    public Vector3 LocalPosition;

    public Color cColor;

    public void UpdateYPosition()
    {
        float yPosition = Mathf.Round(transform.position.y);

        float xPosition = Mathf.Round(transform.position.x * 2) / 2;
        float zPosition = Mathf.Round(transform.position.z * 2) / 2;

        transform.position = new Vector3(xPosition, yPosition + 0.5f, zPosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.position.y + 0.5f < iRow)
        {
            GetComponent<TetroFall>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            UpdateYPosition();
            Vector3 GlobalPos = transform.position;
            iRow = (int)Mathf.Round(Vector3.Distance(GlobalPos, new Vector3(GlobalPos.x, -0.5f, GlobalPos.z)));
            TetroDismount.lListOfWall(iWall)[iRow - 1].Add(gameObject);
        }
    }
}