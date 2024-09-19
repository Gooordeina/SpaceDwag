using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

//Overall script combines to create a custom variable that stores a list of a list of transforms

//Serializable class to hold a list of Transform objects and their position
[System.Serializable]
public class TRANSFORMS
{
    public List<Transform> chunkpieces = new List<Transform>(); //List to store individual chunks (Transform components)
    public Vector3 chunkposition = new Vector3(); //Position of the chunk

    //Method to add a Transform to the chunkpieces list
    public void AddList(Transform transform)
    {
        chunkpieces.Add(transform); //Adds the given Transform to the list
    }
}

//Serializable class to hold a list of TRANSFORMS objects
[System.Serializable]
public class TRANSFORMSLIST
{
    public List<TRANSFORMS> chunklist = new List<TRANSFORMS>(); //List to store multiple TRANSFORMS objects

    //Method to add a TRANSFORMS object to the chunklist
    public void AddList(TRANSFORMS transforms)
    {
        chunklist.Add(transforms); //Adds the given TRANSFORMS object to the list
    }
}