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
        if (!other.gameObject.CompareTag("Tetromino"))
        {
            if (tProperties.bFalling)
            {
                tProperties.transform.localPosition = new Vector3();
                tProperties.tPropertiesOfSpawn.transform.position -= new Vector3(0, 0.25f, 0);
                tProperties.bFalling = false;
            }

            if (tProperties.bNeedToCheck)
            {
                if (tProperties.bMovingLeft)
                {
                    tProperties.transform.localPosition = new Vector3();
                    tProperties.transform.localRotation = new Quaternion();

                    tProperties.bMovingLeft = false;
                    tProperties.SynchroniseWithTetro();
                    tProperties.bChangingWall = false;
                }

                if (tProperties.bMovingRight)
                {
                    tProperties.transform.localPosition = new Vector3();
                    tProperties.transform.localRotation = new Quaternion();

                    tProperties.bMovingRight = false;
                    tProperties.SynchroniseWithTetro();
                    tProperties.bChangingWall = false;
                }


                if (tProperties.bRotating)
                {
                    tProperties.transform.localRotation = new Quaternion();
                    tProperties.bRotating = false;
                }

                tProperties.bNeedToCheck = false;
                tProperties.bCheckedFrame = 0;
                tProperties.UpdatePosition();
            }
            
            else if (other.gameObject.CompareTag("Border") && tProperties.bInitiate)
            {
                Debug.Log("Corrected");
                bIncorrectSpawn = true;
                tProperties.bInitiate = false;
                tProperties.CalculateLeftRight();

                while (tProperties.CalculateColumn(tProperties.LeftRight[0]) < 2)
                {
                    tProperties.iColumn++;
                    tProperties.UpdateTetro();
                    tProperties.tPropertiesOfSpawn.UpdatePosition();
                    tProperties.UpdatePosition();
                    tProperties.CalculateLeftRight();
                }

                while (tProperties.CalculateColumn(tProperties.LeftRight[3]) > 9)
                {
                    tProperties.iColumn--;
                    tProperties.UpdateTetro();
                    tProperties.tPropertiesOfSpawn.UpdatePosition();
                    tProperties.UpdatePosition();
                    tProperties.CalculateLeftRight();
                }
                
                tProperties.tPropertiesOfSpawn.GetComponent<TetroProperties>().ReplaceBorder();
            }
        }
    }
}
