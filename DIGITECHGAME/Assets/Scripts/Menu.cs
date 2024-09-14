using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioSource audio; 
    public Text coins;
    public Slider volume;
    public float totalcoins = 0;
    private void Update()
    {
        audio.volume = volume.value;
        Playerdata data = savesystem.loadplayer();
        totalcoins = data.coins;
        coins.text = "Coin: " + Mathf.Round(totalcoins);
    }
    public void Play ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
