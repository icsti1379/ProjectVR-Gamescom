using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    #region PlayerVariables

    private int experience;

    #endregion

    #region PlayerInputvariables
    
    GameObject gTetroSpawn;
    TetroProperties fProperties;

    #endregion

    #region Getter and setter

    // Getter and setter for player experience (if needed?)
    public int Experience
    {
        get
        {
            return experience;
        }
        set
        {
            experience = value;
        }
    }

    // Level property that converts experience the player level
    public int Level
    {
        get
        {
            return experience / 1000;
        }
        set
        {
            experience = value * 1000;
        }
    }

    // Player health (if needed?)
    public int Health { get; set; }
    
    #endregion


    public void PlayerInput()
    {
        // Move tetromino one column to the left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            fProperties = gTetroSpawn.GetComponent<TetroProperties>();
            fProperties.iColumn--;
            fProperties.UpdatePosition();
            gTetroSpawn.GetComponent<CorrectTetromino>().CorrectTetro();
        }

        // Move tetromino one column to the right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            fProperties = gTetroSpawn.GetComponent<TetroProperties>();
            fProperties.iColumn++;
            fProperties.UpdatePosition();
            gTetroSpawn.GetComponent<CorrectTetromino>().CorrectTetro();
        }

        // Rotates tetromino
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            fProperties.RotateTetro();
        }
    }

}