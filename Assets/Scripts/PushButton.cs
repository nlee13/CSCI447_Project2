using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour {

	public GameObject emlights;
	public GameObject lights;
	public GameObject door;
	private bool nearButton;
	public GameObject source;
	public AudioClip click;
	public AudioClip bootup;
	public GameObject creepy;
	public GameObject background_sound;
	public GameObject background_sound2;

	void Update() {
		if (nearButton && Input.GetKeyUp (KeyCode.E)) {
			//Debug.Log ("Button Push");
			door.SetActive (false);
			lights.SetActive (true);
			emlights.SetActive (false);
			AudioSource.PlayClipAtPoint (bootup, source.transform.position);
			AudioSource.PlayClipAtPoint (click, source.transform.position);
			creepy.SetActive (true);
			AudioSource.Destroy (background_sound);
			AudioSource.Destroy (background_sound2);


		}
	}

	void OnTriggerEnter() {
		//Debug.Log ("Hello");
		nearButton = true;
	}

	void OnTriggerExit() {
		//Debug.Log ("Goodbye");
		nearButton = false;
	}
}
