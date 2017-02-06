using UnityEngine;
using System.Collections;

//TODO: Add/update player attributes. Add timer class or make a game class which contains timer for enemy waves, level time etc. & call it here.

public class PlayerProperties : MonoBehaviour
{
    #region PlayerVariables

    private static string playerName;

    private int Level;
    private int playingTime;
    private static int playerScore;
    private int playerTotalScore;
    private int playerHighscore;

    #endregion

    #region Getter/setter

    public static string PlayerName
    {
        get { return playerName; }

        set { playerName = value; }
    }

    public int PlayerScore
    {
        get { return playerScore; }
        set { playerScore = value; }
    }

    public int PlayingTime
    {
        get { return playingTime; }
        set { playingTime = value; }
    }
       
    #endregion

    void AddScore(playerName playerTotalScore)
    {
        PlayerPrefs
    }

    void Start ()
    {
	    
	}
	
	void Update ()
    {
	    
	}
}
