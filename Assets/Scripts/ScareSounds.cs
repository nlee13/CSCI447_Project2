using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareSounds : MonoBehaviour {

	public GameObject source;

	public AudioClip alienSounds;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			//AudioSource.enabled = true;
			AudioSource.PlayClipAtPoint (alienSounds, source.transform.position);
		}	
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			//AudioSource.enabled = false;
		}
	}
}
