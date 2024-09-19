using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidsystemscript : MonoBehaviour
{
    //variables
    public float distancestep = 0.175f;
    public float radius = 1000;
    public List<GameObject> asteroidprefabs;
    public float asteroidcount;
    public Vector3 spread;
    public List<Transform> asteroids;
    public bool debug;
    public bool defaultstepvalue;
    public TRANSFORMSLIST chunks = new TRANSFORMSLIST();
    public float chunknumber = 8;
    public float renderdistancel;
    public GameObject lowerquality;
    public GameObject higherquiality;
    public Vector3 asteroidscale;

    //calls on first frame
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        //defaultstep value bool is true define distance step
        if(defaultstepvalue)
        {
            //defines distance step as 2pi radians divided by asteroid count so when the script iterates over the asteroids, if it increases by distancestep each iteration, by the end the angle of the asteroid will be 2pi making a full circle of asteroids
            distancestep = 6.28319f/asteroidcount;
        }
        //create a number of chunks in the chunklist equal to the chunknumber defined in the editor
        for(int n = 0; n < chunknumber; n++)
        {
            //adds a list within a custom list type defined by me in another script to create a nested list
            chunks.chunklist.Add(new TRANSFORMS());
        }
        //iterate untill it has iterated over all asteroids
        for (int i = 0; i < asteroidcount; i++)
        {
            //instantiate the asteroid model prefab into a new gameobject nested within the gameobject this script is attatched to
            int asteroidnumber = 0;
            Transform asteroid = Instantiate(asteroidprefabs[asteroidnumber].transform, transform);
            //change the position inorder to place the asteroids in a circle
            asteroid.localPosition = asteroidposition(i * distancestep) * radius;
            asteroids.Add(asteroid);
            //assign the asteroid to a chunk inorder to switch out its model with a less complex model when the player is far enough away for it to not matter
            chunkassign(i * distancestep, asteroid);
            //randomise the rotation and set the scale
            asteroid.localScale = asteroidscale;
            asteroid.localRotation = Quaternion.Euler(Random.Range(-360,360), Random.Range(-360, 360), Random.Range(-360, 360));


        }
        //assign position to chunk function
        chunkpositionassign();
        
    }
    //update once every frame
    private void Update()
    {
        //if debug enabled update posoition of asteroids every frame using the position assigning function 
        //allows for easy debugging and testing of value to get the asteroid field looking and feeling right
        if(debug == true)
        {
            //iterate through every asteroid, changing the position of each one
            for (int i = 0; i < asteroids.Count; i++)
            {
                float n = i;
                asteroids[i].localPosition = asteroidposition(n * distancestep) * radius;
            }
        }
        //iterate through everychunk
        for (int i = 0; i <chunknumber; i++)
        {
            //if the player is close enough to a chunk switch out the model of the asteroids in that chunk for a higher res model, if not switch the model back
            float disttoplayer = (chunks.chunklist[i].chunkposition - Camera.main.transform.position).magnitude;
            if(disttoplayer > renderdistancel)
            {
                //iterate through every transform contained the every chunk in the list of chunks
                for (int n = 0; n < chunks.chunklist[i].chunkpieces.Count; n++)
                {
                    chunks.chunklist[i].chunkpieces[n].gameObject.GetComponent<MeshFilter>().mesh = lowerquality.GetComponent<MeshFilter>().sharedMesh;
                }
            }
            else
            {
                for (int n = 0; n < chunks.chunklist[i].chunkpieces.Count; n++)
                {
                    chunks.chunklist[i].chunkpieces[n].gameObject.GetComponent<MeshFilter>().mesh = higherquiality.GetComponent<MeshFilter>().sharedMesh;
                }
            }

            
        }


    }
    //asteroid position assigner function
    Vector3 asteroidposition(float stepvalue)
    {
        //takes in the step value of the asteroid inorder to calculate the angle it should be at to create a circle
        float xvalue = stepvalue;
        float znum = Mathf.Sin(xvalue) * radius;
        float xnum = Mathf.Cos(xvalue) * radius;
        //randomise the asteroids position by a certain degree in each direction inorder to give more of a 3dimensional look to the field
        float xvariation = Random.Range(0, spread.x);
        float yvariation = Random.Range(0, spread.y);
        float zvariation = Random.Range(0, spread.z);
        //assign the position based off the previously calculated position it needs to be placed in to create a perfect circle combined with random variation
        Vector3 position = new Vector3(xnum + xvariation, yvariation, znum + zvariation);
        return position;
    }
    //asign an asteroid to a chunk based on its step value
    void chunkassign(float stepvalue, Transform asteroid)
    {
        //uses the step value of the asteroid to calculate where it is in the circle relative to the other asteroids and assigns it to certain chunk ranging from chunk to chunk(n) based on this.
        //this creates chunks throughout the asteroid field that are evenly spaced with equal amounts of asteroids in them
        float chunksize = (2 * Mathf.PI) / chunknumber;
        int listvalue = Mathf.RoundToInt((stepvalue / chunksize)-0.5f);
        chunks.chunklist[listvalue].chunkpieces.Add(asteroid);

    }
    //function to assign the position of each chunk so that the script can decide if the player is close enough to the chunk for its asteroids to have their models switched
    //much faster than iterating through every asteroid to see if it close  enough to the  player as there are around 16 chunks to iterate through rather than 5000 asteroids
    void chunkpositionassign()
        {
        //iterate through all chunks
        for (int i = 0; i < chunknumber; i++)
        {
            //calculate position
            Vector3 overallposition = new Vector3();
            for (int n = 0; n < chunks.chunklist[i].chunkpieces.Count; n++)
            {
                overallposition += (chunks.chunklist[i].chunkpieces[n].position);
            }
            Vector3 avgposition = overallposition / chunks.chunklist[i].chunkpieces.Count;
            //assign position to the chunk in the chunk list
            chunks.chunklist[i].chunkposition= avgposition;

        }

    }


}
