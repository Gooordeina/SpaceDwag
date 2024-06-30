using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Vector3 initalv;
    public float timescale;
    public float multiplier;
    public bool timemain;
    public List<GameObject> blacklst;
    public float selfmass = 10;
    public bool moon;
    Rigidbody self;
    private void Start()
    {
        self = transform.GetComponent<Rigidbody>();
        self.velocity = initalv;
    }
    void FixedUpdate()
    {
        foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Gravity"))
        {

            if(!blacklst.Contains(planet))
            {
                Vector3 dir = (planet.transform.position - transform.position);
                Rigidbody pbody = planet.GetComponent<Rigidbody>();
                float force = ((6.67f * Mathf.Pow(10, -11)) * selfmass * planet.GetComponent<Gravity>().selfmass) / (transform.position - planet.transform.position).magnitude;
                if (moon)
                    {
                    force = force * multiplier;
                }
                
                self.AddForce(dir * force, ForceMode.Force);
                
            }
            

        }
    }
    private void Update()
    {
        if(timemain)
        {
            Time.timeScale = timescale;
        }

    }
}
