using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour {

	public Text text;

	float timer = 0;
	float timeToWait = 5.0f;
	bool checkingTime = false;
	bool timerDone = false;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			text.text = "You Have Escaped";
			checkingTime = true;
		}	
	}

	void Update() {
		if (checkingTime) {
			timer += Time.deltaTime;
			if (timer >= timeToWait) {
				timerDone = true;
				checkingTime = false;
				timer = 0;
			}
		}

		if (timerDone || Input.GetKeyDown("escape")) {
			Application.Quit ();
		}
			
	}
}
