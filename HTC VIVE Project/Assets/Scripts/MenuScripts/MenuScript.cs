using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    //TODO: Before finish delete all debug comments!

    /// <summary>
    /// Enum of all menu states.
    /// </summary>
    public enum MenuStates
    {
        Main,
        Player,
        Help,
        Highscore,
        Credits
    }

    #region variables
    
    // Variable for define current state.
    public MenuStates currentState;

    // Menu panel objects as GameObjects.
    public GameObject mainMenu;
    public GameObject playerMenu;
    public GameObject helpMenu;
    public GameObject highscoreMenu;
    public GameObject creditsMenu;
    
    // Player menu toggles
    public bool isSelected;
    public Toggle IsToggleEasy;
    public Toggle IsToggleNormal;
    public Toggle IsToggleHard;

    #endregion


    /// <summary>
    /// Initialize any variables or game states before the game starts.
    /// Is called only once during the lifetime of the script instance.
    /// </summary>
    void Awake()
    {
        // Always sets first state to main menu
        currentState = MenuStates.Main;
    }


    /// <summary>
    /// Update is called every frame.
    /// Checks menu state every frame.
    /// </summary>
    void Update()
    {
        //TODO: Delete options menu if not needed

        // Checks current menu state
        switch (currentState)
        {
            case MenuStates.Main:
                // Sets active gameobject for main menu
                mainMenu.SetActive(true);
                playerMenu.SetActive(false);
                helpMenu.SetActive(false);
                highscoreMenu.SetActive(false);
                creditsMenu.SetActive(false);
                break;

            case MenuStates.Player:
                // Sets active gameobject for player menu
                playerMenu.SetActive(true);
                mainMenu.SetActive(false);
                helpMenu.SetActive(false);
                highscoreMenu.SetActive(false);
                creditsMenu.SetActive(false);
                break;

            case MenuStates.Help:
                // Sets active gameobject for help menu
                helpMenu.SetActive(true);
                mainMenu.SetActive(false);
                playerMenu.SetActive(false);
                highscoreMenu.SetActive(false);
                creditsMenu.SetActive(false);
                break;

            case MenuStates.Highscore:
                // Sets active gameobject for highscore menu
                highscoreMenu.SetActive(true);
                mainMenu.SetActive(false);
                playerMenu.SetActive(false);
                helpMenu.SetActive(false);
                creditsMenu.SetActive(false);
                break;

            case MenuStates.Credits:
                // Sets active gameobject for options menu
                creditsMenu.SetActive(true);
                mainMenu.SetActive(false);
                playerMenu.SetActive(false);
                helpMenu.SetActive(false);
                highscoreMenu.SetActive(false);
                break;
        }
    }

    #region Button OnClick

    /// <summary>
    /// Interaction with start button.
    /// Changes current state to playerMenu.
    /// </summary>
    public void OnStartGame()
    {
        // Log activity
        Debug.Log("You pressed start game!");
        currentState = MenuStates.Player;
    }

    /// <summary>
    /// Interaction with play button.
    /// Loads game scene.
    /// </summary>
    public void OnPlayGame()
    {
        // Log activity
        Debug.Log("You pressed Let's play!");

        // Check active toggle through function.
        ActiveToggle();

        // Add load level for new scene here.
        if (isSelected == true)
        {
            // Log activity
            Debug.Log("Load game");

            SceneManager.LoadScene("GameScene");
        }

        else
        {
            // Log activity
            Debug.Log("Select Difficulty first");
        }
    }


    /// <summary>
    /// Interaction with back button.
    /// Changes current state to mainMenu.
    /// Back button is in every submenue except on "Start" and "Exit".
    /// </summary>
    public void OnBackToMenu()
    {
        // Log activity
        Debug.Log("Go back to main menu.");

        // Change menu state
        currentState = MenuStates.Main;
    }

    /// <summary>
    /// Interaction with help button.
    /// Changes current state to helpMenu.
    /// </summary>
    public void OnHelp()
    {
        // Log activity
        Debug.Log("Help button clicked.");

        // Change menu state
        currentState = MenuStates.Help;
    }

    /// <summary>
    /// Interaction with highscore button.
    /// Changes current state to highscoreMenu.
    /// </summary>
    public void OnHighscore()
    {
        // Log activity
        Debug.Log("Highscore button clicked.");

        // Change menu state
        currentState = MenuStates.Highscore;
    }

    /// <summary>
    /// Interaction with credits button.
    /// Changes current state to creditsMenu.
    /// </summary>
    public void OnCredits()
    {
        // Log activity
        Debug.Log("Credits button clicked.");

        // Change menu state
        currentState = MenuStates.Credits;
    }

    #endregion

    #region Player menu toggles

    /// <summary>
    /// Checks if toggle is active.
    /// </summary>
    public void ActiveToggle()
    {
        if (IsToggleEasy.isOn)
        {
            // Log activity
            Debug.Log("Easy selected.");

            
            TetroFall.fSpeed = 0.6f;
            SpawnBorder.iSpawnPosY = 30;

            isSelected = true;
        }

        if (IsToggleNormal)
        {
            // Log activity
            Debug.Log("Normal selected.");

            TetroFall.fSpeed = 0.4f;
            SpawnBorder.iSpawnPosY = 25;

            isSelected = true;
        }

        else if (IsToggleHard)
        {
            // Log activity
            Debug.Log("Hard selected.");

            TetroFall.fSpeed = 0.4f;
            SpawnBorder.iSpawnPosY = 23;

            isSelected = true;
        }
    }

    #endregion
}