using UnityEngine;
using System.Collections;

public class FixTetromino : MonoBehaviour {

    Rigidbody rRbody;
    SpawnTetromino sTetromino;
    public Vector3 vRotation;

    public int iCubeID;
    public string sType;
    public float fHealth;
    public int iColumn;
    public int iWall;
    public int iRow;

    void Start () {
        rRbody = GetComponent<Rigidbody>();
    }
	
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tetromino") || collision.gameObject.CompareTag("Plane"))
        {
            TetroFall tFall = GetComponent<TetroFall>();
            tFall.enabled = false;                              // Stops the Tetromino from falling
            rRbody.isKinematic = true;

            // SETTING THE EXACT POSITION OF THE TETRIMINO

            Vector3 TetroPos = new Vector3();

            float fPoslil = SpawnTetromino.iMapScale / 2 - 0.5f;
            float fPosbig = SpawnTetromino.iMapScale / 2 + 0.5f;


            // Back
            if (iWall == 1)
            {
                TetroPos = new Vector3(iColumn - fPosbig, 0, fPoslil);
                vRotation = new Vector3();
            }

            // Front
            else if (iWall == 3)
            {
                TetroPos = new Vector3(fPosbig - iColumn, 0, -fPoslil);
                vRotation = new Vector3();
            }

            // Right
            else if (iWall == 2)
            {
                TetroPos = new Vector3(fPoslil, 0, fPosbig - iColumn);
                vRotation = new Vector3(0, 90, 0);
            }

            // Left
            else if (iWall == 4)
            {
                TetroPos = new Vector3(-fPoslil, 0, iColumn - fPosbig);
                vRotation = new Vector3(0, -90, 0);
            }

            float yPos = transform.position.y - 0.5f;
            yPos = Mathf.Round(yPos);

            iRow = (int)yPos + 1;
            transform.position = new Vector3(TetroPos.x, yPos + 0.5f, TetroPos.z);
            transform.rotation = new Quaternion();
            transform.Rotate(vRotation);

            if (sType == "Tetro 3") // if the Tetromino is I type (=4 Quads in a Row) it needs to be rotated
                transform.Rotate(new Vector3(90, 0, 0));
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Border"))
        {
            if (iColumn <= 4)
            {
                iColumn++;

                if (iWall == 1)
                    transform.position += new Vector3(1, 0, 0);

                else if (iWall == 3)
                    transform.position -= new Vector3(1, 0, 0);

                else if (iWall == 2)
                    transform.position -= new Vector3(0, 0, 1);

                else if (iWall == 4)
                    transform.position += new Vector3(0, 0, 1);

                Debug.Log(name + " moved from Column " + (iColumn - 1) + " to " + iColumn);
            }


            else
            {
                iColumn--;

                if (iWall == 1)
                    transform.position -= new Vector3(1, 0, 0);

                else if (iWall == 3)
                    transform.position += new Vector3(1, 0, 0);

                else if (iWall == 2)
                    transform.position += new Vector3(0, 0, 1);

                else if (iWall == 4)
                    transform.position -= new Vector3(0, 0, 1);

                Debug.Log(name + " moved from Column " + (iColumn + 1) + " to " + iColumn);
            }
        }
    }
}