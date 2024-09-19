using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class glowtrigger : MonoBehaviour
{
    public GameObject player; //reference to the player GameObject
    public IEnumerator changeIntensity; //coroutine to change intensity, defined in another script
    public List<Text> planetTexts; //list to hold UI text elements for planets
    public List<Transform> pList; //list to track objects that have been collided with


    //calls when this object's collider overlaps with another collider (The object this script is attatched to is the radar scan. Because the radar scan is made to expand in order to detect when it passes by planet I just put this script on it  to detect when it passes through an object
    private void OnCollisionEnter(Collision collision)
    {
        //check if the collided object is tagged as "dangerous"
        if (collision.gameObject.tag == "dangerous")
        {
            bool containsObject = false; //define a new bool named containsobject as false

            // If the collision object is not already in the list
            if (!pList.Contains(collision.transform))
            {
                //iterate through its children to check for objects with the tag "Object"
                foreach (Transform child in collision.transform)
                {
                    if (child.tag == "Object")
                    {
                        containsObject = true; //define the bool contains object as true if the thing the radar collided with has a child with the object tag
                        pList.Add(collision.transform); //add the collision object to the list so it wont be iterated through again later on for defining what the planet texts in the top right say
                    }
                }
            }
            else //if already in the list, still check for children
            {
                foreach (Transform child in collision.transform)
                {
                    if (child.tag == "Object")
                    {
                        containsObject = true; // Found a tagged object
                    }
                }
            }

            //if a valid object was found, trigger the ability's changeIntensity coroutine
            if (containsObject)
            {
                Abilities abilities = player.GetComponent<Abilities>();
                StartCoroutine(abilities.changeintensity(collision.transform));
            }
        }

        //check for collision with the Earth object specifically as earth has the tag gravity and therfore wouldnt be detected with the tag "dangerous"
        if (collision.gameObject.name == "Earth")
        {
            bool containsObject = false;

            //similar check for children as before
            if (!pList.Contains(collision.transform))
            {
                foreach (Transform child in collision.transform)
                {
                    if (child.tag == "Object")
                    {
                        containsObject = true;
                    }
                }
            }
            else
            {
                foreach (Transform child in collision.transform)
                {
                    if (child.tag == "Object")
                    {
                        containsObject = true;
                    }
                }
            }

            //if a valid object was found, add it to the list and trigger a coroutine to change the light intensity of the planet the radar has collided with
            if (containsObject)
            {
                if (!pList.Contains(collision.transform))
                {
                    pList.Add(collision.transform);
                }
                //accessing ability script to change brightness at that is where the light intensity change coroutine is located
                Abilities abilities = player.GetComponent<Abilities>();
                StartCoroutine(abilities.changeintensity(collision.transform));
            }
        }
    }

    private void Update()
    {
        //update the UI text elements to display names of collided objects
        if (pList.Count >= 1)
        {
            for (int i = 0; i < pList.Count; i++)
            {
                planetTexts[i].text = pList[i].name; //set text to the name of the collided object
            }
        }
    }

    //function to reset this script
    public void Reset()
    {
        pList.Clear(); //clear the list of collided objects
        for (int i = 0; i < planetTexts.Count; i++)
        {
            planetTexts[i].text = "////"; // Reset UI text
        }
    }
}