using UnityEngine;
using System.Collections;

public class CorrectTetromino : MonoBehaviour {

    SpawnTetromino sTetromino;
    TetroProperties tProperties;


    private void OnTriggerEnter(Collider other)
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
    }
}
