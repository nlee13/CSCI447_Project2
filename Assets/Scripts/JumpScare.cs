using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour {

	public GameObject source;

	public AudioClip alienSound;
	//AudioSource audio;
		
	//void Start(){
	//	alienSound = GetComponent<AudioSource> ();
	//}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			AudioSource.PlayClipAtPoint (alienSound, source.transform.position);
		}
	}
}
