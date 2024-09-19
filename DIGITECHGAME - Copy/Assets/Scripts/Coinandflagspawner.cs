using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Coinandflagspawner : MonoBehaviour
{
    //variables
    
    public List<Transform> objectives; //(There are 3 objectives, each one  has a coin and a flag contained within it)
    public Transform planets;
    public int numbere;
    public List<Transform> planetlist;
    // called before the first frame update
    void Awake()
    {
        //public function that, when called, spawns the objectives around random planets
        activate();
    }
    public void activate()
    {
        // add all planets in scene to a list
        foreach (Transform planet in planets)
        {
            planetlist.Add(planet);
        }
        // define the number of objectives the script needs to handle as an integer
        numbere = objectives.Count;

        // loop over every objective added to the objective list
        for (int i = 0; i < numbere; i++)
        {
            // choose a planet
            int planetnumber = UnityEngine.Random.Range(0, planetlist.Count);

            // change the objective at the index we are at in the loop's position and change its position
            objectives[i].position = planetlist[planetnumber].transform.position;
            objectives[i].parent = planetlist[planetnumber].transform;

            //ensure the position of ecah flag and coin within the objective the loop is currently iterating on is at 0,0,0 relative to the objectives new position
            foreach (Transform child in objectives[i])
            {
                child.transform.localPosition = new Vector3(0, 0, 0);
                randomposition rampos = child.GetComponent<randomposition>();
                rampos.planetscale = planetlist[planetnumber].transform.localScale;
            }
        }
    }


}
