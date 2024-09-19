using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

//Class to manage thrust effects for the player’s ship
public class Thrustereffects : MonoBehaviour
{
    public GameObject[] thrusterplates; //Array to hold thruster plate GameObjects
    public float fov = 60; //Default field of view for the camera
    public AudioSource thrusternoise; //Audio source for thrust sound effects

    //Update is called once per frame
    void Update()
    {
        //Get vertical and horizontal input axes from keyboard 
        float v = Input.GetAxis("Vertical"); //Forward/backward movement
        float h = Input.GetAxis("Horizontal"); //Left/right movement

        //Adjust the camera's field of view based on vertical input (thrust)
        Camera.main.fieldOfView = fov + (v * 20);

        //Iterate through each thruster plate to adjust its scale based on thrust
        foreach (GameObject thruster in thrusterplates)
        {
            //Scale the thruster's Z-axis based on the absolute value of vertical input
            thruster.transform.localScale = new Vector3(
                thruster.transform.localScale.x, //Keep the original X scale
                thruster.transform.localScale.y, //Keep the original Y scale
                50 + Mathf.Abs(v) * 200 //Set Z scale based on thrust (vertical input)
            );
        }

        //Modify the pitch of the thrust sound based on vertical input
        thrusternoise.pitch = -0.4f - (v * 0.8f);
    }
}