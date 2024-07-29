using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    public float speed;
    Rigidbody self;
    Vector3 bc;
    public List<GameObject> planets;
    float sockspeed;
    public float distance;
    bool blackholing = false;
    public GameObject centre;
    float interum;
    
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        self = transform.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        interum = 0;
        foreach (GameObject planet in planets)
        {
            float dist = (transform.position - planet.transform.position).magnitude;
            if (dist < planet.GetComponent<Gravity>().DetectiveRadius)
            {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                self.velocity = transform.forward * speed * vertical + transform.right * speed * horizontal + planet.transform.GetComponent<Rigidbody>().velocity;    
                blackholing = false;
                interum+=1;
                speed = 15;
            }
            
        }
        if(interum == 0)
        {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                self.velocity = transform.forward * speed * vertical + transform.right * speed * horizontal;    
                blackholing = false;
                speed = 100;
        }
        if (distance >21)
        {

        }
        if(distance <=  21 && distance  > 1)
        {
            transform.position += direction * sockspeed * Time.deltaTime  * 0.1f;
        }
        if (distance <= 1 && distance  > 0.5f)
        {
            centre.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        }
        if (distance <= 1)
        {
            transform.position += direction * (sockspeed * 0.1f) * Time.deltaTime * 0.1f;
        }


        Vector3 direction2 = bc - transform.position;
        distance = direction2.magnitude;
        


    }
    public void blackholed(float suckspeed, Vector3 blackholecentre)
    {
        direction = blackholecentre - transform.position;
        sockspeed = suckspeed;
        blackholing = true;
        bc = blackholecentre;
    }
}
