using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatscript : MonoBehaviour
{
    public GameObject sun;

    void Update()
    {
        transform.forward = (sun.transform.position - transform.position).normalized;
    }
}
