using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;//extra package (Not native to base unity)
using UnityEngine.Audio;//extra package (Not native to base unity)
using UnityEngine.Rendering; //extra package (Not native to base unity)
using UnityEngine.Rendering.Universal;//extra package (Not native to base unity)

public class MainMenu : MonoBehaviour
{
    public AudioSource audio; //audio source for background music or sound effects
    public Text coins; //UI text element to display the player's coin total
    public int shipNumber = 0; //Index for the selected ship
    public Slider volume; //UI slider to adjust audio volume
    public bool advancedShipPurchased = false; //Bool for advanced ship purchase status
    public bool supremeShipPurchased = false; //Bool for supreme ship purchase status
    public bool volumeOn = true; //bool that signifies whether this script has to deal with the volume slider or not
    public float totalCoins = 0; //Total coins the player has
    public int updateCoins; //Variable to track coin updates
    public Text basic; //UI text for basic ship
    public Text advanced; //UI text for advanced ship
    public Text supreme; //UI text for supreme ship
    public Volume vol; //Volume component for post-processing effects
    public bool shopMenu = true; //bool that signifies whether this script has to deal with the shope menu activity or not
    //trigger once per frame
    private void Update()
    {
        //Update audio volume based on slider value if volume is enabled
        if (volumeOn)
        {
            audio.volume = volume.value;
        }

        //Load player data and update coin display
        if (savesystem.loadplayer() != null)
        {
            //Retrieve total coins from saved data
            Playerdata data = savesystem.loadplayer();
            totalCoins = data.coins; 

            coins.text = "coin: " + Mathf.Round(totalCoins); //Display coins
        }

        //Handle ship selection and display status in the shop menu if shopmenu is enabled
        if (shopMenu)
        {
            //null check to avoid crashing if there is no save file made yet (For first time players)
            if (savesystem.loadship() != null)
            {

                shipNumber = savesystem.loadship().shipnum; //load selected ship number
                advancedShipPurchased = savesystem.loadship().advancedshippurchased; //load advanced ship status
                supremeShipPurchased = savesystem.loadship().supremeshippurchased; //load supreme ship status

                // Update UI text based on purchased ships
                if (advancedShipPurchased && shipNumber == 1)
                {
                    advanced.text = "Selected"; // Advanced ship is selected
                }
                else if (advancedShipPurchased)
                {
                    advanced.text = "Select"; // Advanced ship is available to select
                }

                if (supremeShipPurchased && shipNumber == 2)
                {
                    supreme.text = "Selected"; // Supreme ship is selected
                }
                else if (supremeShipPurchased)
                {
                    supreme.text = "Select"; // Supreme ship is available to select
                }

                // Basic ship is selected if shipNumber is 0
                if (shipNumber == 0)
                {
                    basic.text = "Selected";
                }
                else
                {
                    basic.text = "Select"; // Basic ship is available to select
                }
            }
            else //If no save file is found create one
            {
                savesystem.saveship(this);
            }
        }
    }

    //Load the game scene if called
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Quit the application if called
    public void Quit()
    {
        Application.Quit();
    }

    //load the shop menu scene
    public void shop()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    //load the main menu scene
    public void Home()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    //select the starter ship
    public void startership()
    {
        shipNumber = 0; // Set ship number to starter
        savesystem.saveship(this); // Save ship selection
    }

    //select the advanced ship when called
    public void advancedship()
    {
        if (totalCoins >= 10 && !advancedShipPurchased) //check if player can afford and hasn't purchased
        {
            shipNumber = 1; //set ship number to advanced
            advancedShipPurchased = true; //mark advanced ship as purchased
            savesystem.saveship(this); //save ship purchase status to shipdata and which ship is selected
            updateCoins = -10; //deduct coins
            savesystem.saveplayer(null, this); //save updated coin data to player data
            advanced.text = "Selected"; // Update UI to reflect selection
            updateCoins = 0; // Reset updateCoins
        }
        else if (advancedShipPurchased) //if already purchased
        {
            shipNumber = 1; //select advanced ship
            savesystem.saveship(this); //save selection
            advanced.text = "Selected"; //update UI
        }
    }

    //select the supreme ship
    public void supremeship()
    {
        if (totalCoins >= 20 && !supremeShipPurchased) //check if player can afford and hasn't purchased
        {
            shipNumber = 2; //set ship number to supreme
            supremeShipPurchased = true; //Mark supreme ship as purchased
            savesystem.saveship(this);  //save ship purchase status to shipdata and which ship is selected
            updateCoins = -20; //Deduct coins
            savesystem.saveplayer(null, this);//save updated coin data to player data
            supreme.text = "Selected"; // Update UI to reflect selection
            updateCoins = 0; // Reset updateCoins
        }
        else if (supremeShipPurchased) // If already purchased
        {
            shipNumber = 2; // Select supreme ship
            savesystem.saveship(this); // Save selection
            supreme.text = "Selected"; // Update UI
        }
    }
}