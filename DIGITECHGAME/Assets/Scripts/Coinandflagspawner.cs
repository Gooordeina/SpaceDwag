using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Coinandflagspawner : MonoBehaviour
{
    public List<Transform> objectives;
    public Transform planets;
    public int numbere;
    public List<Transform> planetlist;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Transform planet in planets)
        {
            planetlist.Add(planet);
        }
        numbere = objectives.Count;
        for(int i = 0; i< numbere; i++)
        {   
            
            int planetnumber = UnityEngine.Random.Range(0,planetlist.Count);
            objectives[i].position = planetlist[planetnumber].transform.position;
            objectives[i].parent = planetlist[planetnumber].transform;


            foreach(Transform child in objectives[i])
            {
                child.transform.localPosition = new Vector3(0,0,0);
                randomposition rampos = child.GetComponent<randomposition>();
                rampos.planetscale = planetlist[planetnumber].transform.localScale;
            }
        }
 
    }


}
