using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class FlagPickUp : MonoBehaviour
{

    public float Flag;
    public float Coins;
    public int loopstep = 0;
    public float hitboxrange = 5;
    public Text scoreText;
    public int scoreCount;
        public Text coinText;
    public int coinCount;
    public GameObject coinandflagspawner;
    public GameObject pausem;
    public GameObject planets;
    public GameObject glowtrigger;

    private void Start()
    {
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

                    }
                    else{
                    Coins += 1;
                    }
                    child.gameObject.SetActive(false);
                }
                
            }
        }
        loopstep += 1;
        if(loopstep > looplength)
        {
            loopstep = 0;
        }
        
        scoreText.text = "Flag: " + Mathf.Round(Flag) + "/3";
        coinText.text = "Coin: " + Mathf.Round(Coins);
        if(Flag == 3 )
        {
            foreach (Transform child in objectivelist[loopstep])
            {
                    child.gameObject.SetActive(true);
            }

            coinandflagspawner.GetComponent<Coinandflagspawner>().activate();
            pausem.GetComponent<PauseM>().resettimer();
            glowtrigger.GetComponent<glowtrigger>().Reset();
            Flag = 0;
        }
    }

}
