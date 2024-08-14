using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Movment : MonoBehaviour
{
    public float basespeed;
    public float speed;
    Rigidbody self;
    Vector3 bc;
    public List<GameObject> planets;
    float sockspeed;
    public float distance;
    public float accelerateamount;
    bool blackholing = false;
    Volume vol;
    public GameObject centre;
    float interum;
    public GameObject volume;

    ChromaticAberration chromab;
    
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        self = transform.GetComponent<Rigidbody>();
        speed = basespeed;


    }
    private void FixedUpdate()
    {

        interum = 0;
        foreach (GameObject planet in planets)
        {
            float dist = (transform.position - planet.transform.position).magnitude;
            if (dist < planet.GetComponent<Gravity>().DetectiveRadius)
            {
                basespeed = 15;
                if(Input.GetKey(KeyCode.LeftShift) && speed < 50)
                {
                    speed += accelerateamount;

                }
            else{
                if(speed > basespeed)
                {
                    speed -= accelerateamount;
                }
            
            }
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                self.velocity = transform.forward * speed * vertical + transform.right * speed * horizontal + planet.transform.GetComponent<Rigidbody>().velocity;    
                blackholing = false;
                interum+=1;
                
            }
        }
        if(interum == 0)
        {
             basespeed = 50;
            if(Input.GetKey(KeyCode.LeftShift) && speed < 200)
                {
            speed += accelerateamount;
                }
            else{
                if(speed > basespeed)
                {
                    speed -= accelerateamount;
                }
            
            }
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                self.velocity = transform.forward * speed * vertical + transform.right * speed * horizontal;    
                blackholing = false;
               
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
        
        vol = volume.GetComponent<Volume>();
        vol.profile.TryGet(out chromab);

    }
    public void blackholed(float suckspeed, Vector3 blackholecentre)
    {
        direction = blackholecentre - transform.position;
        sockspeed = suckspeed;
        blackholing = true;
        bc = blackholecentre;
    }

     void OnCollisionEnter(Collision col)
     {
        if (col.gameObject.tag == "CO")
        {
            Scoresystem.scoreCount += 1;
        }

        if (col.gameObject.tag == "Coin")
        {
            Coinsystem.coinCount += 1;
        }
     }
     
}
