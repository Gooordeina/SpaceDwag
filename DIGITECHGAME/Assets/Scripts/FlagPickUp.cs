using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class FlagPickUp : MonoBehaviour
{

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
    private void Start()
    {
        totalflag = 0;
        Flag = 0;
        Coins = 0;
    }
    void Update()
    {

        Coinandflagspawner planet = planets.GetComponent<Coinandflagspawner>();
        List<Transform> objectivelist = planet.objectives;
        int looplength = objectivelist.Count-1;
        float dist = (objectivelist[loopstep].position - transform.position).magnitude;
        if(dist < 100)
        {
            foreach(Transform child in objectivelist[loopstep])
            {
                float childist = (child.transform.position - transform.position).magnitude;

                if(childist < hitboxrange && child.gameObject.activeSelf)
                {
                    if(child.tag == "CO")
                    {
                        Flag += 1;
                        totalflag += 1;

                    }
                    else{
                    Coins += 1;
                        coingather += 1;
                        savetime = true;
                        savecoins = true;
                        savesystem.saveplayer(this);
                        coingather -= 1;
                    }
                    child.gameObject.SetActive(false);
                }
                
            }
        }

        
        scoreText.text = "Flag: " + Mathf.Round(Flag) + "/3";
        coinText.text = "Coin: " + Mathf.Round(Coins);
        scoreText2.text = "Flag: " + Mathf.Round(totalflag);
        coinText2.text = "Coin: " + Mathf.Round(Coins);
        if (Flag == 3 )
        {
            time = pausem.GetComponent<PauseM>().CDTime;
            if(besttime < time)
            {
                besttime = time;
            }
            for(int i = 0; i < objectivelist.Count; i++)
            {
                foreach (Transform child in objectivelist[i])
                {
                    Debug.Log(child);
                    child.gameObject.SetActive(true);
                }
            }   
            besttimescore.text = "Fastest Time: " + Mathf.Round (120 - besttime) + " Seconds";
            
            coinandflagspawner.GetComponent<Coinandflagspawner>().activate();
            pausem.GetComponent<PauseM>().resettimer();
            glowtrigger.GetComponent<glowtrigger>().Reset();
            Flag = 0;
            savecoins = false;
            savetime = true;
            savesystem.saveplayer(this);
            
        }
        loopstep += 1;
        if (loopstep > looplength)
        {
            loopstep = 0;
        }
    }

}
