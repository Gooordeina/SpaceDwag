using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public Transform blackhole;
    public float distance;
    public Transform lookobject;
    float blackholetimer = 36;
    float d;
    public GameObject glowparticle;
    public GameObject player;

    public GameObject planets;
    public GameObject radarparticlees;

    public float acelerationspeed = 50;
    private void Start()
    {
        blackhole.gameObject.SetActive(false);
    }
    private void Update()
    {
        d = (blackhole.transform.position - transform.position).magnitude;
        blackholetimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.T) && !blackhole.gameObject.active)
        {
            Blackhole();
        }
        Debug.Log(blackholetimer);

        if(Input.GetKeyDown(KeyCode.R))
        {
            radar();
        }
    }

    // Update is called once per frame
    void Blackhole()
    {
        blackholetimer = 0f;
        blackhole.transform.position = transform.position + transform.forward * distance;
        blackhole.localScale = new Vector3(20, 20, 20);
        Movment move = player.GetComponent<Movment>();
        move.blackholed(acelerationspeed, blackhole.transform.position);
        lookobject.right = transform.forward;
        blackhole.gameObject.SetActive(true);
    }
    void radar()
    {
        foreach(Transform planet in planets.transform)
        {

            float dist = (transform.position - planet.transform.position).magnitude;
            if(dist < 1000)
            {
                GameObject particle = Instantiate(glowparticle, planet.transform);
                particle.transform.position = planet.transform.position;
            }

        }
        GameObject radarparticle = Instantiate(radarparticlees);
        radarparticle.transform.position = transform.position;
    }

}
