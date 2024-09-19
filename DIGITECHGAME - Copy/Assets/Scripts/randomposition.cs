using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script to randomise the position of each flag and coin after they have already been assigned to a planet in a previous script
public class randomposition : MonoBehaviour
{
    public bool velocity; //Bool to determine if the object's velocity should be updated
    public GameObject player; //Reference to the player GameObject

    public Vector3 planetscale; //Planet Scale of the planet the objective is around to determine random positioning within a certain range
    Rigidbody self; //Rigidbody component for the current object
    public bool coin; //Bool to indicate if this object is a coin
    public bool flag; //Bool to indicate if this object is a flag

    void Start()
    {
        //If the object is a flag, set its random position
        if (flag)
        {
            self = transform.GetComponent<Rigidbody>(); //Get the Rigidbody component
            float xpnn = 0; //Variable to determine offset for x-axis
            float ypnn = 0; //Variable to determine offset for y-axis
            float zpnn = 0; //Variable to determine offset for z-axis

            //Generate random values to determine offset
            float xpn = Random.Range(-1, 1);
            float ypn = Random.Range(-1, 1);
            float zpn = Random.Range(-1, 1);

            //Set offset for x-axis
            xpnn = (xpn == 0) ? -1 : 1;
            //Set offset for y-axis
            ypnn = (ypn == 0) ? -1 : 1;
            //Set offset for z-axis
            zpnn = (zpn == 0) ? -1 : 1;

            //Set the local position randomly within a range based on planetscale
            transform.localPosition = new Vector3(
                Random.Range(planetscale.x, planetscale.x * 1.1f) * xpnn,
                Random.Range(planetscale.x, planetscale.x * 1.1f) * ypnn,
                Random.Range(planetscale.x, planetscale.x * 1.1f) * zpnn
            );
        }

        //If the object is a coin, set its random position similarly; seperate if statment so as the random number generator doesnt generate similar numbers as it seemed to when the coin and flag positions were randomised in the same if statment
        if (coin)
        {
            self = transform.GetComponent<Rigidbody>(); //Get the Rigidbody component
            float xpnn = 0; //Variable to determine direction for x-axis
            float ypnn = 0; //Variable to determine direction for y-axis
            float zpnn = 0; //Variable to determine direction for z-axis

            //Generate random values to determine direction
            float xpn = Random.Range(1, -1);
            float ypn = Random.Range(1, -1);
            float zpn = Random.Range(1, -1);

            //Set direction for x-axis
            xpnn = (xpn == 0) ? -1 : 1;
            //Set direction for y-axis
            ypnn = (ypn == 0) ? -1 : 1;
            //Set direction for z-axis
            zpnn = (zpn == 0) ? -1 : 1;

            //Set the local position randomly within a range based on planetscale
            transform.localPosition = new Vector3(
                Random.Range(planetscale.x, planetscale.x * 1.1f) * xpnn,
                Random.Range(planetscale.x, planetscale.x * 1.1f) * ypnn,
                Random.Range(planetscale.x, planetscale.x * 1.1f) * zpnn
            );
        }
    }

    void Update()
    {
        //If velocity updates are enabled
        if (velocity)
        {
            //Iterate through the planets associated with the player
            foreach (GameObject planet in player.GetComponent<Movement>().planets)
            {
                float dist = (transform.position - planet.transform.position).magnitude; //Calculate distance to the planet

                //If within the detection radius of the planet
                if (dist < planet.GetComponent<Gravity>().DetectiveRadius)
                {
                    //Set this object's velocity to match the planet's velocity
                    self.velocity = planet.transform.GetComponent<Rigidbody>().velocity;
                }
            }
        }

        //If the player has collected 3 flags, reset the position of this object
        if (player.GetComponent<FlagPickUp>().Flag == 3)
        {
            Start(); //Call Start method to reset position
        }
    }
}