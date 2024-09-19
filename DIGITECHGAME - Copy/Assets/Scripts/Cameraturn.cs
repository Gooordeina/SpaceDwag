using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraturn : MonoBehaviour
{
    // variables
    public Transform player;
    public float sensitivityx;
    public float sensitivityy;
    float camerahorizontalrotation;
    float modifiedsensitivity;
    public float rollsensitivity;
    public Vector3 playerrotation;
    float yintial;
    public float speed;
    public GameObject pausegame;
    float camverticalrotation;
    // Start function called at start of frame to set a few variables to the values they are required to be for the script to function properly
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yintial = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        // define scripts inorder to access them later
        PauseM pausescript = pausegame.GetComponent<PauseM>();
        Movement move = player.gameObject.GetComponent<Movement>();
        // Only execute if game not paused
        if(!pausescript.GamePause && !move.gameOver)
        {

            // Check the player's rotation around the Z-axis
            if (player.transform.eulerAngles.z > 179 && player.transform.eulerAngles.z < 181)
            {
                // If the Z-axis rotation is close to 180 degrees, invert the sensitivity for the X-axis input
                modifiedsensitivity = -1 * sensitivityx;
            }
            else
            {
                //Otherwise, use the normal sensitivity for the X-axis input
                modifiedsensitivity = sensitivityx;
            }

            // Get the mouse input for X and Y axes and adjust them based on the sensitivity
            float x = Input.GetAxis("Mouse X") * modifiedsensitivity; // Adjusted X-axis movement
            float y = Input.GetAxis("Mouse Y") * -sensitivityy;       // Adjusted Y-axis movement (inverted)


            camverticalrotation += y;   // Increase the vertical value of the players rotation
            camerahorizontalrotation += x; // Increase the horizontal value of the players rotation

            // Smoothly interpolate the camera's local position based on input
            transform.localPosition = Vector3.Lerp(
                new Vector3(x / 4, yintial + y / 4, transform.localPosition.z), // Target position with adjustments
                new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), // Current position
                0.95f // Lerp factor (smoothness)
            );

            // Update the player's rotation based on the new camera rotations
            player.rotation = Quaternion.Euler(camverticalrotation, camerahorizontalrotation, player.transform.rotation.y);

            // Store the player's current rotation in the `playerrotation` variable
            playerrotation = player.transform.eulerAngles;
        }

        // Check if the game is over
        if (move.gameOver)
        {
            // Move the transform backward along its forward direction
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
    }
}
