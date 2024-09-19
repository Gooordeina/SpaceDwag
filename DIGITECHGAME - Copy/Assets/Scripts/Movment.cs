using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Movement : MonoBehaviour
{
    public float baseSpeed; //base speed of the player
    public float speed; //current speed of the player
    Rigidbody self; //Rigidbody component for physics interactions
    Vector3 bc; //Backward center point for black hole interactions
    public Color thrustColor; //Color for thrust effect
    public Color normalColor; //Color for normal state
    public List<GameObject> planets; //List of planets for gravity detection
    public GameObject playerModel; //Default player model
    public GameObject playerModel1; //Advanced player model
    public GameObject playerModel2; //Supreme player model
    float sockSpeed; //Speed related to black hole suction
    public Vector3 dirc; //Direction vector for black hole
    public GameObject pauseMenu; //Reference to pause menu
    public Material myMat; //Material for the player model's emission
    public Vector3 dirv; //Normalized velocity direction
    public GameObject explosionParticles; //Particle effect for explosions
    public float dirDif; //Difference between two directions
    public float borderRadius; //Distance from the center for movement limits
    public float distance; //Distance to the black hole
    public float accelerateAmount = 1; //Amount to increase speed
    public bool gameOver; //Bool to check if the game is over
    [SerializeField] private Volume vol; //Volume for post-processing effects
    public GameObject center; //Reference to the center object for black hole

    //Maximum speeds for different states
    public float fastPlanetMaxSpeed = 50;
    public float slowPlanetMaxSpeed = 15;
    public float fastSpaceSpeed = 200;
    public float slowSpaceSpeed = 50;

    Vector3 direction; //Direction for movement towards black hole

    //Start is called before the first frame update
    void Start()
    {
        self = transform.GetComponent<Rigidbody>(); //Get Rigidbody component
        speed = baseSpeed; //Initialize speed to base speed
        int num = savesystem.loadship().shipnum; //Load selected ship number

        //Set player model and speeds based on selected ship
        if (num == 0)
        {
            playerModel.SetActive(true);
            playerModel1.SetActive(false);
            playerModel2.SetActive(false);
            fastPlanetMaxSpeed = 50;
            slowPlanetMaxSpeed = 15;
            fastSpaceSpeed = 200;
            slowSpaceSpeed = 50;
            accelerateAmount = 1;
        }
        else if (num == 1)
        {
            playerModel.SetActive(false);
            playerModel1.SetActive(true);
            playerModel2.SetActive(false);
            fastPlanetMaxSpeed = 75;
            slowPlanetMaxSpeed = 30;
            fastSpaceSpeed = 230;
            slowSpaceSpeed = 75;
            accelerateAmount = 2f;
        }
        else if (num == 2)
        {
            playerModel.SetActive(false);
            playerModel1.SetActive(false);
            playerModel2.SetActive(true);
            fastPlanetMaxSpeed = 130;
            slowPlanetMaxSpeed = 50;
            fastSpaceSpeed = 320;
            slowSpaceSpeed = 130;
            accelerateAmount = 4;
        }
    }
    //update every fixedtime (independant of framerate so works just as well for laggy devices; important for physics related updates)
    private void FixedUpdate()
    {
        float interim = 0; //Counter for gravity detection, if 0 the player is not near planets, if its 1 the player is+

        //Check distance to each planet for gravity influence
        foreach (GameObject planet in planets)
        {
            float dist = (transform.position - planet.transform.position).magnitude;
            if (dist < planet.GetComponent<Gravity>().DetectiveRadius) //Within gravitational influence
            {
                baseSpeed = slowPlanetMaxSpeed; //Set base speed for slow movement
                if (Input.GetKey(KeyCode.LeftShift)) //Accelerate when holding shift
                {
                    if (speed < fastPlanetMaxSpeed) //If speed is below max, increase
                    {
                        speed += accelerateAmount;
                    }
                    if (speed > fastPlanetMaxSpeed) //If speed exceeds max, decrease rapidly
                    {
                        speed -= accelerateAmount * 10;
                    }
                    //Adjust visual effects for thrust
                    if (vol.profile.TryGet(out ChromaticAberration chromab))
                    {
                        chromab.intensity.value = 1; //Increase chromatic aberration effect
                    }
                    myMat.SetColor("_Emission", thrustColor * 20); //Set thrust color
                }
                else //Not speedboosting
                {
                    if (speed > baseSpeed) //Reduce speed if above base
                    {
                        speed -= accelerateAmount;
                    }
                    if(speed < baseSpeed)
                    {
                        speed += accelerateAmount;
                    }
                    if (vol.profile.TryGet(out ChromaticAberration chromab))
                    {
                        chromab.intensity.value = 0.25f; //Set normal visual effect
                    }
                    myMat.SetColor("_Emission", normalColor * 20); //Set normal color
                }

                //Handle player movement
                if (!gameOver)
                {
                    float horizontal = Input.GetAxisRaw("Horizontal");
                    float vertical = Input.GetAxisRaw("Vertical");
                    //Apply movement based on input and planet velocity
                    self.velocity = transform.forward * speed * vertical + transform.right * speed * horizontal + planet.transform.GetComponent<Rigidbody>().velocity;

                    interim += 1; //counter for gravity detection
                }
            }
        }

        //If not near any planets, use space movement logic
        if (interim == 0)
        {
            baseSpeed = slowSpaceSpeed; //Set base speed for space

            if (Input.GetKey(KeyCode.LeftShift)) //Accelerate when holding shift
            {
                if (speed < fastSpaceSpeed)
                {
                    speed += accelerateAmount; //Increase speed
                }
                //Adjust visual effects for thrust
                if (vol.profile.TryGet(out ChromaticAberration chromab))
                {
                    chromab.intensity.value = 1; //Increase chromatic aberration effect
                }
                myMat.SetColor("_Emission", thrustColor * 20); //Set thrust color
            }
            else //Not speedboosting
            {
                if (speed > baseSpeed) //Reduce speed if above base
                {
                    speed -= accelerateAmount * 10; //Slow down
                }
                if (speed < baseSpeed) //If speed is below base, increase
                {
                    speed += accelerateAmount;
                }
                if (vol.profile.TryGet(out ChromaticAberration chromab))
                {
                    chromab.intensity.value = 0.25f; //Set normal visual effect
                }
                myMat.SetColor("_Emission", normalColor * 20); //Set normal color
            }

            //Direction calculations for black hole movement
            dirc = transform.position.normalized; //Normalized direction of player position
            dirv = self.velocity.normalized; //Normalized velocity direction
            dirDif = (dirc - dirv).magnitude; //Difference between directions

            //Apply border effects if the player is too far from center
            if (transform.position.magnitude > borderRadius && dirDif <= 1)
            {
                speed = baseSpeed * 1 / (transform.position.magnitude - borderRadius);
            }

            //Handle player movement
            if (!gameOver)
            {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                self.velocity = transform.forward * speed * vertical + transform.right * speed * horizontal; //Apply movement
            }
        }

        //Handle distance-related actions for blavc hole
        if (distance <= 21 && distance > 1)
        {
            transform.position += direction * sockSpeed * Time.deltaTime * 0.1f; //Move towards the black hole
        }
        if (distance <= 1 && distance > 0.5f)
        {
            center.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f); //Scale the center object
        }
        if (distance <= 1)
        {
            transform.position += direction * (sockSpeed * 0.1f) * Time.deltaTime * 0.1f; //Continue moving towards the black hole
        }

        //Calculate distance to black hole
        Vector3 direction2 = bc - transform.position;
        distance = direction2.magnitude;
    }

    //Method to handle black hole interactions
    public void blackholed(float suckSpeed, Vector3 blackHoleCenter)
    {
        direction = blackHoleCenter - transform.position; //Set direction towards black hole
        sockSpeed = suckSpeed; //Set suction speed
        bc = blackHoleCenter; //Update center point
    }

    //Handle collisions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dangerous" || collision.gameObject.tag == "Gravity")
        {
            StartCoroutine(endscreenTrigger()); //Trigger end screen on collision
        }
    }

    //Coroutine for handling end screen logic
    public IEnumerator endscreenTrigger()
    {
        explosionParticles.SetActive(true); //Activate explosion effect
        gameOver = true; //Set game over flag
        //Disable player models
        playerModel.SetActive(false);
        playerModel1.SetActive(false);
        playerModel2.SetActive(false);
        yield return new WaitForSeconds(1.5f); //Wait before triggering end screen
        pauseMenu.GetComponent<PauseM>().endscreen();
    }
}