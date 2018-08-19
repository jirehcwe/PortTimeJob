using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMSpawner : MonoBehaviour {

	public GameObject audioManager;

	// Use this for initialization
	void Start () {
		if (AudioManager.instance == null) {
			Instantiate (audioManager, transform.position, transform.rotation);
		} else
			Destroy (gameObject);

	}
}