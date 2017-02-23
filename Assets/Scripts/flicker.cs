using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flicker : MonoBehaviour {
	
	private Light flashlightSource;

	[SerializeField]
	float lights_on = 99.999f;
	[SerializeField]
	float min_power = 3f;
	[SerializeField]
	float max_power = 8f;

	float power = 100f;
	// Use this for initialization
	void Start ()
	{
		flashlightSource = GetComponent<Light>();
	}

	// Update is called once per frame
	void Update ()
	{
		float chance = Random.Range (0, 101);
		if ( chance < lights_on) 
		{
			if(chance < lights_on/4)
			{
				power = Random.Range(min_power,max_power);
			}
			flashlightSource.intensity = power;		
		}
		else
		{
			flashlightSource.intensity = 0.0f;
		}	
	}
}
