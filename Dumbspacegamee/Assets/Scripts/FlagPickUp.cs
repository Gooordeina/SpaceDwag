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
   
    public GameObject planets;

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

                if(childist < hitboxrange)
                {
                    if(child.tag == "CO")
                {
                    Flag += 1;
                }
                else{
                    Coins += 1;
                }   
                    Destroy(child.gameObject);
                }
            }
        }
        loopstep += 1;
        if(loopstep > looplength)
        {
            loopstep = 0;
        }
        
        scoreText.text = "Flag: " + Mathf.Round(Flag);
        coinText.text = "Coin: " + Mathf.Round(Coins);
    }
}
