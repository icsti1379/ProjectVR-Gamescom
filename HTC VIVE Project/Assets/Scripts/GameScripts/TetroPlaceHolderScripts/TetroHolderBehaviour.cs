using UnityEngine;
using System.Collections;

public class TetroHolderBehaviour : MonoBehaviour {

    TetroHolderProperties tProperties;
    bool bIncorrectSpawn = false;

    private void Start()
    {
        tProperties = GetComponent<TetroHolderProperties>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Placeholder"))
        {
            if (tProperties.bFalling)
            {
                tProperties.transform.localPosition = new Vector3();
                tProperties.tPropertiesOfSpawn.transform.position -= new Vector3(0, 0.15f, 0);
                tProperties.bFalling = false;
            }

            if (tProperties.bNeedToCheck)
            {
                if (tProperties.bMovingLeft)
                {
                    tProperties.iColumn++;
                    tProperties.bMovingLeft = false;
                }

                if (tProperties.bMovingRight)
                {
                    tProperties.iColumn--;
                    tProperties.bMovingRight = false;
                }

                if (tProperties.bRotating)
                {
                    tProperties.transform.rotation = new Quaternion();
                    tProperties.bRotating = false;
                    tProperties.transform.position = new Vector3();
                }

                tProperties.bNeedToCheck = false;
                tProperties.bCheckedFrame = 0;
                tProperties.UpdatePosition();
            }
            else if (other.gameObject.CompareTag("Border"))
            {
                bIncorrectSpawn = true;

                if (tProperties.iColumn < 5)
                    tProperties.iColumn++;

                else if (tProperties.iColumn > 5)
                    tProperties.iColumn--;

                tProperties.UpdateTetro();
                tProperties.UpdatePosition();
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(bIncorrectSpawn)
        {
            if (tProperties.iColumn < 5)
                tProperties.iColumn++;

            else if (tProperties.iColumn > 5)
                tProperties.iColumn--;

            tProperties.UpdateTetro();
            tProperties.UpdatePosition();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (bIncorrectSpawn)
            bIncorrectSpawn = false;
    }
}
