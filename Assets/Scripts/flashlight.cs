/*
Credit for this script goes to the Unity forum users Collin_Patrick and Chasing_Wyatt
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight : MonoBehaviour {

    //Flashlight Object
    private Light flashlightSource;

    //Flashlight power variables
    public float power = 100.0f;
    public float maxPower = 100.0f;
    public float minPower = 0.0f;
    private float powerDrain = 1.0f;

    //Usability variable
    public bool usable = true;

    // Use this for initialization
    void Start ()
    {
        power = maxPower;
        flashlightSource = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Flashlight intensity will decrease with battery life
        flashlightSource.intensity = power;
        
        //When the Scroll Wheel is pressed, flashlight turns on 
        if (Input.GetMouseButtonUp(0))
        {
            flashlightSource.enabled = !flashlightSource.enabled;
        }

		/*
        //While flashlight is on, power will drain
        if (flashlightSource.enabled)
        {
            power -= Time.deltaTime * powerDrain;
        }

        //Makes sure power can never be over 100
        if (power > maxPower)
        {
            power = maxPower;
        }

        //Disables flashligh usability if flashlight is out of batteries
        if (power < minPower)
        {
            power = minPower;
            flashlightSource.enabled = false;
            usable = false;
        }
		*/
   	}

}
