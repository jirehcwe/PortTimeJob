using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckBehaviour : MonoBehaviour {

	public static float maxTruckSpeed;
//	public float truckDeceleration = 0.2f;
//	public float truckSpeed;
	public float brakeDistance;
	public bool isBraking = false;
	public float atBrakeY= 0;
    float velocityInterpolator=0;
    public float deceleration = 0.5f;

	bool crashSoundPlayed = false;

	void Start () {
	}

	void Update () {
		if (ButtonScript.truckCrash == false) {
			truckMovement ();
		}
		else if (ButtonScript.truckCrash == true){   
            // game over screen
			truckDecelerating ();
			if (crashSoundPlayed == false) {
				crashSoundPlayed = true;
			}
		}
	}
		
	void truckMovement(){
		this.transform.position += Vector3.down * Time.deltaTime * maxTruckSpeed;
	}

	void truckDecelerating(){
//		brakeDistance = 2.3f;
//		if (isBraking == false) {
//			atBrakeY = this.transform.position.y;
//			isBraking = true;
//		}
//		if (this.transform.position.y > atBrakeY - brakeDistance) {
//			Debug.Log ("deceler");
			this.transform.position += new Vector3 (0, Mathf.Lerp(-Time.deltaTime * maxTruckSpeed, 0, velocityInterpolator), 0);
            velocityInterpolator += Time.deltaTime;
		
	}

}
