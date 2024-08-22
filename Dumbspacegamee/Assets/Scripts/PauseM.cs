using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseM : MonoBehaviour
{
    public bool GamePause = false;

    public GameObject PauseUI;

    public GameObject CoinCount;

    public GameObject ScoreCount;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float CDTime;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Resume();
        Scoresystem sco = ScoreCount.GetComponent<Scoresystem>();
        sco.scoreCount = 0;
        CoinCount.GetComponent<Coinsystem>();
        Coinsystem coi = CoinCount.GetComponent<Coinsystem>();
        coi.coinCount = 0;
    }

}

