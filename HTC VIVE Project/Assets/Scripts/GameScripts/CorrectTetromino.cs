using UnityEngine;
using System.Collections;

public class CorrectTetromino : MonoBehaviour {

    SpawnTetromino sTetromino;
    TetroProperties tProperties;

    int iMax;
    int iMin;

    /*private void OnTriggerEnter(Collider other)
    {
        tProperties = GetComponent<TetroProperties>();

        if (other.gameObject.CompareTag("Border"))
        {
            if (tProperties.iColumn <= 4)
            {
                tProperties.iColumn++;

                tProperties.UpdatePosition();

                Debug.Log(name + " moved from Column " + (tProperties.iColumn - 1) + " to " + tProperties.iColumn);
            }


            else if (tProperties.iColumn >= SpawnBorder.iMapScale -3)
            {
                tProperties.iColumn--;

                tProperties.UpdatePosition();

                Debug.Log(name + " moved from Column " + (tProperties.iColumn + 1) + " to " + tProperties.iColumn);
            }
        }
    }*/

    private void Start()
    {
        iMax = SpawnBorder.iMapScale / 2 - 1;
        iMin = -iMax;
    }

    public void CorrectTetro()
    {
        tProperties = GetComponent<TetroProperties>();

        if (tProperties.iWall == 1)
        {
            tProperties.CalculateCubes();

            if (tProperties.vPositionCube.x <= iMin || tProperties.vPositionCube2.x <= iMin || 
                tProperties.vPositionCube3.x <= iMin || tProperties.vPositionCube4.x <= iMin)
            {
                tProperties.iColumn++;
                tProperties.UpdatePosition();
            }
            else if(tProperties.vPositionCube.x >= iMax || tProperties.vPositionCube2.x >= iMax ||
                    tProperties.vPositionCube3.x >= iMax || tProperties.vPositionCube4.x >= iMax)
            {
                tProperties.iColumn--;
                tProperties.UpdatePosition();
            }
        }

        else if (tProperties.iWall == 3)
        {
            tProperties.CalculateCubes();

            if (tProperties.vPositionCube.x <= iMin || tProperties.vPositionCube2.x <= iMin ||
                tProperties.vPositionCube3.x <= iMin || tProperties.vPositionCube4.x <= iMin)
            {
                tProperties.iColumn--;
                tProperties.UpdatePosition();
            }
            else if (tProperties.vPositionCube.x >= iMax || tProperties.vPositionCube2.x >= iMax ||
                    tProperties.vPositionCube3.x >= iMax || tProperties.vPositionCube4.x >= iMax)
            {
                tProperties.iColumn++;
                tProperties.UpdatePosition();
            }
        }

        else if (tProperties.iWall == 4)
        {
            tProperties.CalculateCubesZ();

            if (tProperties.vPositionCube.z <= iMin || tProperties.vPositionCube2.z <= iMin ||
                tProperties.vPositionCube3.z <= iMin || tProperties.vPositionCube4.z <= iMin)
            {
                tProperties.iColumn++;
                tProperties.UpdatePosition();
            }
            else if (tProperties.vPositionCube.z >= iMax || tProperties.vPositionCube2.z >= iMax ||
                    tProperties.vPositionCube3.z >= iMax || tProperties.vPositionCube4.z >= iMax)
            {
                tProperties.iColumn--;
                tProperties.UpdatePosition();
            }
        }

        else if (tProperties.iWall == 2)
        {
            tProperties.CalculateCubesZ();

            if (tProperties.vPositionCube.z <= iMin || tProperties.vPositionCube2.z <= iMin ||
                tProperties.vPositionCube3.z <= iMin || tProperties.vPositionCube4.z <= iMin)
            {
                tProperties.iColumn--;
                tProperties.UpdatePosition();
            }
            else if (tProperties.vPositionCube.z >= iMax || tProperties.vPositionCube2.z >= iMax ||
                    tProperties.vPositionCube3.z >= iMax || tProperties.vPositionCube4.z >= iMax)
            {
                tProperties.iColumn++;
                tProperties.UpdatePosition();
            }
        }
    }
}
