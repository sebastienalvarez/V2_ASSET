using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2Launch : MonoBehaviour {
    /*  V2 Launch script for V2 Rocket Asset
     *  Author : Sebastien ALVAREZ
     *  Version : 1.0 
     *  Date : 6 may 2018
     *  Purpose : Play 2 free sounds (countdown and rocket launch), activate rocket engine particle system and add velocity to the V2 for demonstration purpose
     *  Contact : sebastien.alvarez@yahoo.fr
    */

    public AudioClip countDownSound; // Variable to get countdown sound (sound file to get in Unity Editor)
    public AudioClip rocketLaunchSound; // Variable to get rocket launch sound (sound file to get in Unity Editor)
    public GameObject exaust; // GameObject to activate rocket engine exaust
    public float mass = 13000f; // Variable for rocket mass (25000kg is real value for the V2)
    public float drag = 0.15f; // Variable for drag coefficient
    public float dryMass = 4390f; // Variable for rocket dry mass (4390kg is real value for the V2)
    public float thrust = 25000f; // Variable for rocket engine thrust (25000kg is real value for the V2, in script it is multiplied by 9.8m/s²)
    public float consumption = 125f; // Variable for fuel consumption (125kg/s is real value for the V2)
    private bool isLaunchAsked = false; // Variable for user launch command detection
    private bool isLaunchStart = false; // Variable for launch detection after the countdown 
    private AudioSource audioSourceCountDown; // AudioSource object to play countdown sound
    private AudioSource audioSourceRocketlaunch; // AudioSource object to play rocket launch sound
    private Rigidbody rocket; // Rigibody object to apply physics on the V2 rocket

	void Start () {
        audioSourceCountDown = GetComponents<AudioSource>()[0];
        audioSourceRocketlaunch = GetComponents<AudioSource>()[1];
        rocket = GetComponent<Rigidbody>();
        rocket.mass = mass;
        rocket.drag = drag;
        exaust.SetActive(false);
	}
	
	void Update () {
        // Detection of launch command from the user
        if(Input.GetKeyDown(KeyCode.Space) && !isLaunchAsked)
        {
            isLaunchAsked = true;
            // Start 10s countdown
            StartCoroutine(StartCountDown());
        }		
	}

    private void FixedUpdate()
    {
        if (isLaunchStart)
        {
            if(rocket.mass >= dryMass)
            {
                // Physics application on the rocket
                rocket.AddForce(9.8f * thrust * gameObject.transform.forward, ForceMode.Force);
                rocket.mass -= consumption * Time.deltaTime;
            }
            else
            {
                exaust.SetActive(false);
            }
        }    
    }

    // Countdown temporisation method
    IEnumerator StartCountDown()
    {
        audioSourceCountDown.volume = 0.1f;
        audioSourceCountDown.PlayOneShot(countDownSound);
        yield return new WaitForSeconds(10.0f);
        StartCoroutine(StartRocketLaunch());
    }

    IEnumerator StartRocketLaunch()
    {
        audioSourceRocketlaunch.volume = 0.8f;
        audioSourceRocketlaunch.PlayOneShot(rocketLaunchSound);
        yield return new WaitForSeconds(7f);
        exaust.SetActive(true);
        yield return new WaitForSeconds(1f);
        isLaunchStart = true;
    }
}
