using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Playerdata 
{
    public bool savecoins;
    public bool savetime;
    public int coins;
    public float besttime;
    public Playerdata(FlagPickUp stats)
    {
        savecoins = stats.savecoins;
        savetime = stats.savetime;
        if(savecoins)
        {
            coins = stats.coingather;
        }
        else
        {
            coins = 0;
        }
        if (savetime)
        {
            besttime = stats.besttime;
        }
        
    }

}
