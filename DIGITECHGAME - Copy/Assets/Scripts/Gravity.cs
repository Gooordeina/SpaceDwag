using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    //variables
    public Vector3 initalv;
    public float timescale;
    public float multiplier;
    public bool timemain;
    public List<GameObject> blacklst;
    public List<GameObject> materialholders;
    public float DetectiveRadius;
    public float selfmass; 
    public bool moon;
    Rigidbody self;
    public bool dontapplyforce;
    //define a few variables at the first frame
    private void Start()
    {
        self = transform.GetComponent<Rigidbody>(); 
        self.velocity = initalv;
    }
    //update seperatley to framerate
    void FixedUpdate()
    {

        //foreach object marked to be affected by gravity via the gravity tag
        //apply a gravitational pull coming from the object with this script attatched
        foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Gravity"))
        {
            //only apply gravity to planets not blacklisted by the planet with this script on it
            if(!blacklst.Contains(planet))
            {
                //calculate direction to the other planet
                Vector3 dir = (planet.transform.position - transform.position);
                //get the other planets rigidbody
                Rigidbody pbody = planet.GetComponent<Rigidbody>();
                //calculate force based on the real physics equation
                float force = ((6.67f * Mathf.Pow(10, -11)) * selfmass * planet.GetComponent<Gravity>().selfmass) / (transform.position - planet.transform.position).magnitude;
                //if moon is enabled increase the force it feels by a multiplier
                if (moon)
                    {
                    force = force * multiplier;
                }
                //if dontapplyforce isnt enabled apply the calculated force
                if(!dontapplyforce)
                {
                    self.AddForce(dir * force * 10000, ForceMode.Force);
                }
            }
        }
    }
    //if timemain enabled set the games time scale to time scale
    private void Update()
    {
        if(timemain)
        {
            Time.timeScale = timescale;
        }
    }
}
