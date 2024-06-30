using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameraturn : MonoBehaviour
{
    public Transform player;
    public float sensitivityx;
    public float sensitivityy;
    float camerahorizontalrotation;
    float modifiedsensitivity;
    public float rollsensitivity;
    float cameraroll;
    public Vector3 playerrotation;
    float yintial;
    float camverticalrotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yintial = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.eulerAngles.z >179 && player.transform.eulerAngles.z < 181)
        {
            modifiedsensitivity = -1 * sensitivityx;
        }
        else
        {
            modifiedsensitivity = sensitivityx;
        }
        float x = Input.GetAxis("Mouse X") * modifiedsensitivity;
        float y = Input.GetAxis("Mouse Y") * -sensitivityy;
        float r = Input.GetAxis("Roll") * rollsensitivity;
        camverticalrotation += y;
        camerahorizontalrotation += x;
        cameraroll += r;
        transform.localPosition = Vector3.Lerp(new Vector3(x/4, yintial +  y/4, transform.localPosition.z), new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), 0.95f);

        player.rotation = Quaternion.Euler(camverticalrotation, camerahorizontalrotation, player.transform.rotation.y);
        playerrotation = player.transform.eulerAngles;

    }
}
