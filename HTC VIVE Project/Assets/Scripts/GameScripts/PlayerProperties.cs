using UnityEngine;
using System.Collections;

//TODO: Add/update player attributes. Add timer class or make a game class which contains timer for enemy waves, level time etc. & call it here.

public class PlayerProperties : MonoBehaviour
{
    #region PlayerVariables

    private static string playerName;

    private int Level;
    private int playingTime;
    private int playerScore;

    #endregion

    #region Getter/setter

    public static string PlayerName
    {
        get { return playerName; }

        set { playerName = value; }
    }

    public int PlayerScore { get; set; }

    public int PlayingTime { get; set; }
       
    #endregion

    void Start ()
    {
	    
	}
	
	void Update ()
    {
	    
	}
}
