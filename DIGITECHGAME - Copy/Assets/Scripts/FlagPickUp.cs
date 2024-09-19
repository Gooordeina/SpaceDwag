using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class FlagPickUp : MonoBehaviour
{
    //variables
    public int Flag;
    public int Coins;
    public int coingather;
    public int loopstep = 0;
    public float hitboxrange = 5;
    public Text scoreText;
    public Text scoreText2;
    public int scoreCount;
    public Text coinText;
    public Text coinText2;
    public int coinCount;
    public GameObject coinandflagspawner;
    public GameObject pausem;
    public GameObject planets;
    public GameObject glowtrigger;
    public float totalflag;
    float time;
    public bool savetime;
    public bool savecoins;
    public Text besttimescore;
    public float besttime = 0;
    //reset a few variables on the first frame to ensure they dont start at the wrong number after looping
    private void Start()
    {
        totalflag = 0;
        Flag = 0;
        Coins = 0;
    }
    //calls once per frame
    void Update()
    {
        //define some variables
        Coinandflagspawner planet = planets.GetComponent<Coinandflagspawner>();
        List<Transform> objectivelist = planet.objectives;
        int looplength = objectivelist.Count - 1;
        float dist = (objectivelist[loopstep].position - transform.position).magnitude;
        //If distance is less than 100 to an objective start checking the distance to flags and coins contained within it
        if (dist < 100)
        {
            //iterate through every child in objectivelist, to optimise instead of iterating through all steps in one frame it iterates through them one frame at a time and then resets when it reaches the final frame step

            foreach (Transform child in objectivelist[loopstep])
            {
                float childist = (child.transform.position - transform.position).magnitude;
                //checkposition of coin/flag the player is near, define which it is, and act on it
                if (childist < hitboxrange && child.gameObject.activeSelf)
                {
                    if (child.tag == "CO")
                    {
                        //if flag increase flag score
                        Flag += 1;
                        totalflag += 1;

                    }
                    else
                    {
                        //if coin add 50 coins to the coin score and add 50 then remove 50 to a temporary coin score in order to not add the coins of one coin collision and then the coins of the previous coin and the new coins of the new collision when the next collision happens
                        Coins += 1;

                        savetime = true;
                        savecoins = true;
                        coingather += 1;
                        //savesystem function to save coins
                        savesystem.saveplayer(this, null);
                        coingather -= 1;
                    }
                    //hide object regaurdless of which type it was
                    child.gameObject.SetActive(false);
                }

            }
        }

        //set score and coin texts for endscreen and gui
        scoreText.text = "Flag: " + Mathf.Round(Flag) + "/3";
        coinText.text = "Coin: " + Mathf.Round(Coins);
        scoreText2.text = "Flag: " + Mathf.Round(totalflag);
        coinText2.text = "Coin: " + Mathf.Round(Coins);
        //if all 3 flags are gathered reset game
        if (Flag == 3)
        {
            //figure out if the time the player just took to get the 3 flags is better than their previous
            time = pausem.GetComponent<PauseM>().CDTime;
            if (besttime < time)
            {
                besttime = time;
            }
            //iterate through every objective in objective list
            for (int i = 0; i < objectivelist.Count; i++)
            {
                //iterate through every coin and flag in the objective
                foreach (Transform child in objectivelist[i])
                { 
                    //make the coins and flags visible again
                    child.gameObject.SetActive(true);
                }
            }
            //update best time if the time the player just took to get the 3 flags is better than their previous
            besttimescore.text = "Fastest Time: " + Mathf.Round(120 - besttime) + " Seconds";

            //trigger all neccesary functions to reset the game, also save best time
            coinandflagspawner.GetComponent<Coinandflagspawner>().activate();
            pausem.GetComponent<PauseM>().resettimer();
            glowtrigger.GetComponent<glowtrigger>().Reset();
            Flag = 0;
            savecoins = false;
            savetime = true;
            savesystem.saveplayer(this, null);

        }
        //increase loopstep for the slower iteration through each objective so the computer only handles one objective each frame instead of 3
        loopstep += 1;
        if (loopstep > looplength)
        {
            loopstep = 0;
        }
    }

}