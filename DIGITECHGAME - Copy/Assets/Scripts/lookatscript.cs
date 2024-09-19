using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatscript : MonoBehaviour
{
    //define sun in a variable
    public GameObject sun;

    //every frame update the object with this script on it to look in the direction of the sun
    void Update()
    {
        transform.forward = (sun.transform.position - transform.position).normalized;
    }
}
