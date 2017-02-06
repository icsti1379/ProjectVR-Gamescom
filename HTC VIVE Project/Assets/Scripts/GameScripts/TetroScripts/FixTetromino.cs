using UnityEngine;
using System.Collections;

public class FixTetromino : MonoBehaviour {

    Rigidbody rRbody;
    SpawnTetromino sTetromino;
    TetroProperties tProperties;
    bool bTetroSplitted;




    void Start () {
        rRbody = GetComponent<Rigidbody>();
        tProperties = GetComponent<TetroProperties>();
        bTetroSplitted = false;
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
            if (tProperties.iWall == 1)
                TetroPos = new Vector3(tProperties.iColumn - fPosbig, 0, fPoslil);

            // Front
            else if (tProperties.iWall == 3)
                TetroPos = new Vector3(fPosbig - tProperties.iColumn, 0, -fPoslil);

            // Right
            else if (tProperties.iWall == 2)
                TetroPos = new Vector3(fPoslil, 0, fPosbig - tProperties.iColumn);

            // Left
            else if (tProperties.iWall == 4)
                TetroPos = new Vector3(-fPoslil, 0, tProperties.iColumn - fPosbig);

            float yPos = transform.position.y - 0.5f;
            yPos = Mathf.Round(yPos);

            transform.position = new Vector3(TetroPos.x, yPos + 0.5f, TetroPos.z);

            SplitTetrominos sSplit;
            sSplit = GetComponent<SplitTetrominos>();

            if (!bTetroSplitted)
            {
                sSplit.SplitTetromino();
                bTetroSplitted = true;
            }
        }
    }
}