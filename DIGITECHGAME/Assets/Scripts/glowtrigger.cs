using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class glowtrigger : MonoBehaviour
{
    public GameObject player;
    public IEnumerator changeintensity;
    public List<Text> planettexts;
    public List<Transform> plist;
    public GameObject objectivesystem;
    // Start is called before the first frame update=
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "dangerous")
        {
            bool containsobject = false;
            if (!plist.Contains(collision.transform))
            {
                foreach (Transform child in collision.transform)
                {
                    if (child.tag == "Object")
                    {
                        containsobject = true;

                        plist.Add(collision.transform);

                    }

                }
            }
            else
            {
                foreach (Transform child in collision.transform)
                {
                    if (child.tag == "Object")
                    {
                        containsobject = true;

                    }

                }
            }
            if (containsobject)
            {

                Abilities abilities = player.GetComponent<Abilities>();
                StartCoroutine(abilities.changeintensity(collision.transform));
            }
        }

        if(collision.gameObject.name == "Earth")
        {
            bool containsobject = false;
            if (!plist.Contains(collision.transform))
            {
                foreach (Transform child in collision.transform)
                {
                    if (child.tag == "Object")
                    {
                        containsobject = true;
                    }

                }
            }
            else
            {
                foreach (Transform child in collision.transform)
                {
                    if (child.tag == "Object")
                    {
                        containsobject = true;

                    }

                }
            }
            if (containsobject)
            {
                if (!plist.Contains(collision.transform))
                {
                    plist.Add(collision.transform);
                }
                Abilities abilities = player.GetComponent<Abilities>();
                StartCoroutine(abilities.changeintensity(collision.transform));
            }
        }
    }
    private void Update()
    {




        Debug.Log(planettexts.Count);
        if (plist.Count >= 1)
        {
            for (int i = 0; i < plist.Count; i++)
            {
                Debug.Log(i);
                planettexts[i].text = plist[i].name;
                
            }
        }
   

    }
    public void Reset()
    {
        plist.Clear();
        for (int i = 0; i < planettexts.Count; i++)
        {
            planettexts[i].text = "////";
        }

    }
}
