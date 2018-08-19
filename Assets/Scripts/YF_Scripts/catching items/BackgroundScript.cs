using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		other.gameObject.SendMessage("HitGround");
	}
}