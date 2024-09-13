using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Abilities : MonoBehaviour
{
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
    bool radaroncooldown = false;
    float RTIME;
    public  float  radarcooldown;
    public Text cooldowntext;
    float CTIME;
    private void Start()
    {
        RTIME = radarcooldown;
        currentintensity = 1;
        blackhole.gameObject.SetActive(false);
    }
    private void Update()
    {
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
        d = (blackhole.transform.position - transform.position).magnitude;
        blackholetimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.T) && !blackhole.gameObject.active)
        {
            Blackhole();
        }

        if (Input.GetKeyDown(KeyCode.R) && RTIME > radarcooldown)
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
        StartCoroutine(scale());
        radaroncooldown = true;
        RTIME = 0;
    }
    public IEnumerator changeintensity(Transform Planet)
    {
        List<Color> colours = new List<Color>();
        List<Material> materials = new List<Material>();
        for (int i = 0; i < Planet.GetComponent<Gravity>().materialholders.Count; i++)
        {
            Material planetmat = Planet.GetComponent<Gravity>().materialholders[i].GetComponent<Renderer>().material;
            Color newcolor = Planet.GetComponent<Gravity>().materialholders[i].GetComponent<Renderer>().material.GetColor("_Color");
            colours.Add(newcolor);
            materials.Add(planetmat);

        }
        float elapsedtime = 0;

        while (elapsedtime < lerptime)
        {
            elapsedtime += Time.deltaTime;
            float s = (targetintensity - 1) / lerptime;
            float multiplier = s * elapsedtime + 1;
            for (int i = 0; i < colours.Count; i++)
            {
                materials[i].SetColor("_Color", colours[i] * multiplier);
            }

            yield return null;
        }
        while (elapsedtime > lerptime && elapsedtime < (2 * lerptime))
        {
            elapsedtime += Time.deltaTime;
            float s = (targetintensity - 1) / lerptime;
            float multiplier = -s * (elapsedtime - lerptime) + targetintensity;
            for (int i = 0; i < colours.Count; i++)
            {
                materials[i].SetColor("_Color", colours[i] * multiplier);
            }
            yield return null;
        }



    }
    public IEnumerator scale()
    {
        float elapsedtime = 0;  
        radarparticlees.transform.rotation = transform.rotation;
        while (elapsedtime < radartime)
        {
            radarparticlees.transform.position = transform.position;

            elapsedtime += Time.deltaTime;
            float rate = Mathf.Pow(radarexpandspeedtarget,1/( radartime - horizontalshift));

            float speed = Mathf.Pow(rate, elapsedtime - horizontalshift);
            radarparticlees.transform.localScale += new Vector3(speed, speed, speed) * Time.deltaTime;

            yield return null;
        }   
        radarparticlees.transform.localScale = new Vector3(0, 0, 0) * Time.deltaTime;
    }
}