using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseM : MonoBehaviour
{
    public bool GamePause = false;

    public GameObject PauseUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePause)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    void Resume ()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        GamePause = false;
    }
    void Pause ()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        GamePause = true;
    }
}
