using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrustereffects : MonoBehaviour
{
    public GameObject[] thrusterplates;
    public float fov = 60;
    public AudioSource thrusternoise;
    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Camera.main.fieldOfView = fov + (v * 20);
        foreach (GameObject thruster  in thrusterplates)
        {
            thruster.transform.localScale = new Vector3(thruster.transform.localScale.x, thruster.transform.localScale.y, 50 + Mathf.Abs(v) * 200);
        }

        thrusternoise.pitch = -0.4f - (v*0.8f);
        
    }
}
