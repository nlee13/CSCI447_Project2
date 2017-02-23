using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour {

	// Update is called once per frame
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "jet")
			Destroy (other.gameObject);
	}
}
