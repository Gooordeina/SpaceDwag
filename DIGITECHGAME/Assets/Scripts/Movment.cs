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
    public Color thrustcolour;
    public Color normalcolour;
    public List<GameObject> planets;
    float sockspeed;
    public Vector3 dirc;

    public Material mymat;
    public  Vector3 dirv;

    public float dirdif;
    public float borderradius;
    public float distance;
    public float accelerateamount;
    bool blackholing = false;
    [SerializeField] private Volume vol;
    public GameObject centre;
    float interum;



    
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
                if(Input.GetKey(KeyCode.LeftShift) )
                {
                    if(speed < 50)
                    {
                        speed += accelerateamount;
                    }
                    if (speed > 50)
                    {
                        speed -= accelerateamount *10;
                    }
                    if (vol.profile.TryGet(out ChromaticAberration chromab))
                    {
                        chromab.intensity.value = 1;
                    }
                    mymat.SetColor("_Emission", thrustcolour * 20);
                }
            else{
                if(speed > basespeed)
                {
                    speed -= accelerateamount;
                    
                }
                    if(vol.profile.TryGet(out ChromaticAberration chromab))
                    {
                        chromab.intensity.value = 0.25f;
                    }
            mymat.SetColor("_Emission", normalcolour * 20);
            
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
                if(Input.GetKey(KeyCode.LeftShift) )
                {
                    if(speed < 200)
                    {
                        speed += accelerateamount;
                    }
                    
                    if(vol.profile.TryGet(out ChromaticAberration chromab))
                    {
                        chromab.intensity.value = 1;
                    }
                    mymat.SetColor("_Emission", thrustcolour * 20);
                }
            else{
                    if(speed > basespeed)
                    {
                        speed -= accelerateamount * 10;
                        
                    }
                if (speed < basespeed)
                {
                    speed += accelerateamount;

                }
                if (vol.profile.TryGet(out ChromaticAberration chromab))
                    {
                        chromab.intensity.value = 0.25f;
                    }
                    mymat.SetColor("_Emission", normalcolour * 20);
                    
            
                }
            dirc = transform.position.normalized;
            dirv = self.velocity.normalized;
            dirdif = (dirc-dirv).magnitude;
            if(transform.position.magnitude > borderradius && dirdif <= 1)
                {
                    speed = basespeed * 1/(transform.position.magnitude - borderradius);
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
        

    }
    public void blackholed(float suckspeed, Vector3 blackholecentre)
    {
        direction = blackholecentre - transform.position;
        sockspeed = suckspeed;
        blackholing = true;
        bc = blackholecentre;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "dangerous")
        {
            Debug.Log("Bob");
            transform.gameObject.SetActive(false);
        }
    }

}
