using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {
    /*  Camera Tracking script for V2 Rocket Asset
    *  Author : Sebastien ALVAREZ
    *  Version : 1.0 
    *  Date : 6 may 2018
    *  Purpose : Hide canvas and camera tracking of V2 rockets
    *  Contact : sebastien.alvarez@yahoo.fr
    */

    public Canvas canvas; // Canvas object to hide the canvas with asset description and command instruction
    public GameObject V2; // V2 Gameobject for tracking purpose
    private bool isLaunchAsked = false; // Variable for user launch command detection
    public float rotationSpeed = 0.2f; // Variable to parameter the rotation speed when waiting for launch
    private Vector3 initialPosition; // variable to save initial position of the camera


    private void Start()
    {
        initialPosition = gameObject.transform.position;
    }

    void Update () {
        // Rotate the camera around the V2 rockets when waiting for space bar pressing
        if (!isLaunchAsked)
        {
            gameObject.transform.position = Quaternion.AngleAxis(rotationSpeed, Vector3.up) * gameObject.transform.position;
        }

        // Hide the canvas when the user press Space Bar to start asset demonstration
        if (Input.GetKeyDown(KeyCode.Space) && !isLaunchAsked)
        {
            // ===> Uncomment the following code line to get the initial camera position at launch
            //gameObject.transform.position = initialPosition;
            isLaunchAsked = true;
            canvas.enabled = false;
        }

        // Camera tracking of V2 rockets
        gameObject.transform.rotation = Quaternion.LookRotation(V2.transform.position - gameObject.transform.position);
    }
}
