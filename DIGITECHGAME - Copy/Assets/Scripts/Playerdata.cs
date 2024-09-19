using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Playerdata class to hold player-related variables
[System.Serializable]
public class Playerdata
{
    public bool savecoins; //Bool to indicate if coins should be saved
    public bool savetime; //Bool to indicate if time should be saved
    public int coins; //Number of coins the player has
    public float besttime; //The player's best time

    //Constructor to initialize Playerdata from FlagPickUp and MainMenu classes
    public Playerdata(FlagPickUp stats, MainMenu menu)
    {
        //Check if stats is not null to avoid errors
        if (stats != null)
        {
            savecoins = stats.savecoins; //Assign savecoins flag from stats
            savetime = stats.savetime; //Assign savetime flag from stats

            //If coins should be saved, get the gathered coins
            if (savecoins)
            {
                coins = stats.coingather; //Set coins from stats
            }
            else
            {
                coins = 0; //If not saving coins, set to 0
            }

            //If time should be saved, set the best time
            if (savetime)
            {
                besttime = stats.besttime; //Set best time from stats
            }
        }

        //Check if menu is not null to avoid errors
        if (menu != null)
        {
            coins = menu.updateCoins; //Set coins from menu if available
        }
    }
}

//Shipdata class to hold ship-related variables
[System.Serializable]
public class Shipdata
{
    public int shipnum = 0; //The number representing the ship type (default to 0)
    public bool advancedshippurchased; //Flag to indicate if the advanced ship has been purchased
    public bool supremeshippurchased; //Flag to indicate if the supreme ship has been purchased

    //Constructor to initialize Shipdata from MainMenu class
    public Shipdata(MainMenu menu)
    {
        //Assign values based on the state of the MainMenu
        supremeshippurchased = menu.supremeShipPurchased; //Set supreme ship purchase status
        advancedshippurchased = menu.advancedShipPurchased; //Set advanced ship purchase status
        shipnum = menu.shipNumber; //Set the currently selected ship number
    }
}