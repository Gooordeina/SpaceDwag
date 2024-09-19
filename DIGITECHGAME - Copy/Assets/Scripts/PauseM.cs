using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseM : MonoBehaviour
{
    public bool GamePause = false; //Bool to check if the game is paused
    public GameObject PauseUI; //UI element for the pause menu
    public GameObject endgameui; //UI element for the end game screen
    bool gameover; //Bool to check if the game is over
    public GameObject gui; //Main game UI
    public GameObject flagpickup; //Reference to flag pickup object
    public GameObject player; //Reference to the player object
    public Text timerText; //UI element to display the timer
    public float CDTime; //Countdown timer for the game

    void Start()
    {
        gameover = false; //Initialize game over state
        Cursor.visible = false; //Hide the cursor
        Cursor.lockState = CursorLockMode.Locked; //Lock the cursor to the game window
    }

    //Update is called once per frame
    void Update()
    {
        //Check for the Escape key to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape) && !gameover)
        {
            if (GamePause)
            {
                Resume(); //Resume game if currently paused
            }
            else
            {
                Pause(); //Pause game if currently running
            }
        }

        //Update countdown timer
        if (CDTime > 0)
        {
            CDTime -= Time.deltaTime; //Decrease countdown time
        }
        else if (CDTime < 0)
        {
            CDTime = 0; //Ensure countdown doesn't go below zero
            StartCoroutine(player.GetComponent<Movement>().endscreenTrigger()); //Trigger end screen if countdown reaches zero
        }

        //Format and display the timer
        int minutes = Mathf.FloorToInt(CDTime / 60); //Calculate minutes
        int seconds = Mathf.FloorToInt(CDTime % 60); //Calculate seconds
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //Update timer text
    }

    public void Resume()
    {
        PauseUI.SetActive(false); //Hide pause menu UI
        Time.timeScale = 1f; //Resume game time
        GamePause = false; //Set pause flag to false
        Cursor.visible = false; //Hide the cursor
        Cursor.lockState = CursorLockMode.Locked; //Lock the cursor again
    }

    void Pause()
    {
        PauseUI.SetActive(true); //Show pause menu UI
        Time.timeScale = 0f; //Freeze game time
        GamePause = true; //Set pause flag to true
        Cursor.visible = true; //Show the cursor
        Cursor.lockState = CursorLockMode.None; //Unlock the cursor
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu"); //Load the main menu scene
    }

    public void Quit()
    {
        Application.Quit(); //Exit the application
        Debug.Log("Quit"); //Log quit action for debugging
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reload the current scene
        FlagPickUp fp = flagpickup.GetComponent<FlagPickUp>(); //Get the FlagPickUp component
        fp.Coins = 0; //Reset coins
        fp.Flag = 0; //Reset flag count
        Resume(); //Resume game after restarting
    }

    public void endscreen()
    {
        gui.SetActive(false); //Hide main game UI
        gameover = true; //Set game over flag
        endgameui.SetActive(true); //Show end game UI
        Time.timeScale = 0f; //Freeze game time
        GamePause = true; //Set pause flag to true
        Cursor.visible = true; //Show the cursor
        Cursor.lockState = CursorLockMode.None; //Unlock the cursor
    }

    public void resettimer()
    {
        CDTime = 120; //Reset countdown timer to 120 seconds
    }
}