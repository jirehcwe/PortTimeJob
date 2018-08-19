using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleScript : MonoBehaviour {

	static public float speed;
	private float leftBound,rightBound;
	// Use this for initialization
	void Start () {
		speed = YFGameManager.baseSpeed * 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		speed = YFGameManager.baseSpeed * 0.5f;

		transform.Translate (Input.acceleration.x * speed*Time.deltaTime * 25f, 0, 0);

		leftBound = YFGameManager.origin.x - 0.75f*gameObject.GetComponentInChildren<BoxCollider2D> ().bounds.size.x;
		rightBound = YFGameManager.limit.x - 0.25f*gameObject.GetComponentInChildren<BoxCollider2D> ().bounds.size.x;// gameObject.GetComponentInChildren<BoxCollider2D> ().bounds.size.x;

		if (transform.position.x <= leftBound) {
			transform.position = new Vector3 (leftBound, transform.position.y, transform.position.z);
		}
		if (transform.position.x >= rightBound) {
			transform.position = new Vector3 (rightBound, transform.position.y, transform.position.z);
		}

		
	}
}
