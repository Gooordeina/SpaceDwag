using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Abilities : MonoBehaviour
{
    //variables
    public Transform blackhole;
    public float distance;
    public Transform lookobject;
    float blackholetimer = 36;
    float d;
    public GameObject player;
    public float Radarange = 500;
    public GameObject planets;
    public GameObject radarparticlees;
    public float currentintensity;
    public float targetintensity;
    public float lerptime;
    public float radarexpandspeedtarget;
    public float radartime;
    public float horizontalshift;
    public float acelerationspeed = 50;
    float RTIME;
    public  float  radarcooldown;
    public Text cooldowntext;
    float CTIME;
    
    //resets a few variables at the first frame before everything else happens to ensure they start off at the correct value
    private void Start()
    {
        RTIME = radarcooldown;
        currentintensity = 1;
        blackhole.gameObject.SetActive(false);
    }
    //updates once every frame
    private void Update()
    {
        //Radarcooldown count down vfx handler (If it has been longer than the radar cooldown since radar was used hides text and resets cooldown, if not sets text to a countdown from 20)
        if(RTIME > radarcooldown)
        {
            cooldowntext.text = "";
            CTIME = radarcooldown;
        }
        else
        {
            CTIME -= Time.deltaTime;
            cooldowntext.text = "" + Mathf.Round(CTIME);
        }
        RTIME += Time.deltaTime;

        //Blackhole ability, discontinued because we ran out of time however the code is being left in incase we want to revisit and complete the abilites in the future
        d = (blackhole.transform.position - transform.position).magnitude;
        blackholetimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.T) && !blackhole.gameObject.activeSelf)
        {
            Blackhole();
        }

        //if it has been longer than the radar cooldown since it was used trigger radar
        if (Input.GetKeyDown(KeyCode.R) && RTIME > radarcooldown)
        {
            //radar function
            radar();
        }
    }

    //black hole function, handles the position, scale, cooldown, and visibility of the black hole
    void Blackhole()
    {
        blackholetimer = 0f;
        blackhole.transform.position = transform.position + transform.forward * distance;
        blackhole.localScale = new Vector3(20, 20, 20);
        Movement move = player.GetComponent<Movement>();
        move.blackholed(acelerationspeed, blackhole.transform.position);
        lookobject.right = transform.forward;
        blackhole.gameObject.SetActive(true);
    }
    //Radar function to set radar cooldown timer to 0 and start the scale coroutine
    void radar()
    {
        //coroutine that changes the scale of the radar vfx
        StartCoroutine(scale());
        //variable to keep track of time since radar was used
        RTIME = 0;
    }

    // Coroutine to smoothly change the intensity of a planet's material color over time
    public IEnumerator changeintensity(Transform Planet)
    {
        // Lists to hold the original colors and materials of the planet's renderers
        List<Color> colours = new List<Color>();
        List<Material> materials = new List<Material>();

        // Retrieve and store the original colors and materials from all material holders on the planet
        for (int i = 0; i < Planet.GetComponent<Gravity>().materialholders.Count; i++)
        {
            // Get the material and its color from the current material holder
            Material planetmat = Planet.GetComponent<Gravity>().materialholders[i].GetComponent<Renderer>().material;
            Color newcolor = Planet.GetComponent<Gravity>().materialholders[i].GetComponent<Renderer>().material.GetColor("_Color");

            // Add the color and material to the respective lists
            colours.Add(newcolor);
            materials.Add(planetmat);
        }

        // Time elapsed since the coroutine started
        float elapsedtime = 0;

        // Gradually increase the intensity of the material color
        while (elapsedtime < lerptime)
        {
            elapsedtime += Time.deltaTime; // Update elapsed time
            float s = (targetintensity - 1) / lerptime; // Calculate the scale factor
            float multiplier = s * elapsedtime + 1; // Calculate the multiplier for the color intensity

            // Apply the color change to each material
            for (int i = 0; i < colours.Count; i++)
            {
                materials[i].SetColor("_Color", colours[i] * multiplier);
            }

            yield return null; // Wait for the next frame
        }

        // Gradually decrease the intensity of the material color
        while (elapsedtime > lerptime && elapsedtime < (2 * lerptime))
        {
            elapsedtime += Time.deltaTime; // Update elapsed time
            float s = (targetintensity - 1) / lerptime; // Calculate the scale factor
            float multiplier = -s * (elapsedtime - lerptime) + targetintensity; // Calculate the multiplier for the color intensity

            // Apply the color change to each material
            for (int i = 0; i < colours.Count; i++)
            {
                materials[i].SetColor("_Color", colours[i] * multiplier);
            }

            yield return null; // Wait for the next frame
        }
    }

    // Coroutine to scale an object over time
    public IEnumerator scale()
    {
        float elapsedtime = 0; //time elapsed since the coroutine started

        //set the radar particles' rotation to match the object's rotation
        radarparticlees.transform.rotation = transform.rotation;

        //gradually scale the radar particles
        while (elapsedtime < radartime)
        {
            //set the radar particles' position to match the object's position
            radarparticlees.transform.position = transform.position;

            elapsedtime += Time.deltaTime; // Update elapsed time

            //calculate the rate of expansion based on the target expansion speed and total time
            float rate = Mathf.Pow(radarexpandspeedtarget, 1 / (radartime - horizontalshift));

            //calculate the scaling factor
            float speed = Mathf.Pow(rate, elapsedtime - horizontalshift);

            // Increase the scale of the radar particles
            radarparticlees.transform.localScale += new Vector3(speed, speed, speed) * Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        //=et the final scale to 0 inorder to make the vfx dissapear after it has finished growing
        radarparticlees.transform.localScale = new Vector3(0, 0, 0) * Time.deltaTime;
    }
}