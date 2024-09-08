using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidsystemscript : MonoBehaviour
{
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
    public TRANSFORMS bob = new TRANSFORMS();


    void Start()
    {
        if(defaultstepvalue)
        {
            distancestep = 6.28319f/asteroidcount;
        }
        for(int n = 0; n < chunknumber; n++)
        {
            chunks.chunklist.Add(new TRANSFORMS());
        }
        for (int i = 0; i < asteroidcount; i++)
        {
           
            int asteroidnumber = Random.Range(0, asteroidprefabs.Count);
            Transform asteroid = Instantiate(asteroidprefabs[asteroidnumber].transform, transform);
            asteroid.localPosition = asteroidposition(i * distancestep) * radius;
            asteroids.Add(asteroid);
            chunkassign(i * distancestep, asteroid);


        }
        
    }

    private void Update()
    {
        if(debug == true)
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                float n = i;
                asteroids[i].localPosition = asteroidposition(n * distancestep) * radius;
            }
        }
        for (int i = 0; i < asteroids.Count; i++)
        {
            float dist = (asteroids[i].position - Camera.main.transform.position).magnitude;
            Debug.Log(dist);
        }


    }
    Vector3 asteroidposition(float stepvalue)
    {
        float xvalue = stepvalue;
        float znum = Mathf.Sin(xvalue) * radius;
        float xnum = Mathf.Cos(xvalue) * radius;
        float xvariation = Random.Range(0, spread.x);
        float yvariation = Random.Range(0, spread.y);
        float zvariation = Random.Range(0, spread.z);
        Vector3 position = new Vector3(xnum + xvariation, yvariation, znum + zvariation);
        return position;
    }
    void chunkassign(float stepvalue, Transform asteroid)
    {
        float chunksize = (2 * Mathf.PI) / chunknumber;
        if (0 <= stepvalue && stepvalue < chunksize)
        {



        }

    }

}
