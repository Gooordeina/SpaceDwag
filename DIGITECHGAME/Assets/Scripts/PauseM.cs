using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseM : MonoBehaviour
{
    public bool GamePause = false;

    public GameObject PauseUI;

    public GameObject endgameui;
    bool gameover;
    public GameObject gui;
    public GameObject flagpickup;
    public GameObject player;
    public Text timerText;
    public float CDTime;

    void Start()
    {
        gameover = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape) && !gameover)
        {
            if (GamePause)
            {
                Resume();
            } 
            else
            {
                Pause();
            }
        }
    
        if (CDTime > 0)
        {
            CDTime -= Time.deltaTime;
        }
        else if (CDTime < 0)
        {
            CDTime = 0;
            StartCoroutine(player.GetComponent<Movment>().endscreentrigger());

        }
        int minutes = Mathf.FloorToInt(CDTime / 60);
        int seconds = Mathf.FloorToInt(CDTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    

    }

    public void Resume ()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        GamePause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Pause ()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        GamePause = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FlagPickUp fp = flagpickup.GetComponent<FlagPickUp>();
        fp.Coins = 0;
        fp.Flag = 0;
        Resume();

    }
    public void endscreen()
    {
        gui.SetActive(false);
        gameover = true;
        endgameui.SetActive(true);
        Time.timeScale = 0f;
        GamePause = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public  void resettimer()
    {
        CDTime = 120;
    }

}

