using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){

		/*
		switch (other.name) {
		case "Parcel1(Clone)":
		case "Parcel2(Clone)":
		case "Parcel3(Clone)":
		case "Parcel4(Clone)":
		case "Parcel5(Clone)":
			other.gameObject.SendMessage ("ParcelCaught");
			break;
		case "Junk1(Clone)":
		case "Junk2(Clone)":
			other.gameObject.SendMessage ("JunkCaught");
			break;
		case "FlyingLetter(Clone)":
			other.gameObject.SendMessage ("FlyingLetterCaught");
		default:
			Debug.Log ("ContainerScript switch statement error");
			break;
		}
		*/

		other.gameObject.SendMessage ("ItemCaught");
	}
}
