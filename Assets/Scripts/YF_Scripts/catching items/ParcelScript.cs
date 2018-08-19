using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelScript : MonoBehaviour {

	public RuntimeAnimatorController parcelPoints;

	public int parcelScore;
	public float frequency;
	private Vector3 from, to;
	private float time;
	public float speed;

	// Use this for initialization
	void Start () {

		//for the rotation of sprites
		from = new Vector3 (0, 0, -5f);
		to = new Vector3 (0, 0, 5f);
		frequency = 0.4f;
		time = 0;
		speed = YFGameManager.baseSpeed;

		switch (gameObject.name) {
		case "Parcel1(Clone)":
			parcelScore = 100;
			break;
		case "Parcel2(Clone)":
			parcelScore = 200;
			break;
		case "Parcel3(Clone)":
			parcelScore = 300;
			speed = 0.7f * speed;
			break;
		case "Parcel4(Clone)":
			parcelScore = 500;
			speed = 1.7f * speed;
			break;
		case "Parcel5(Clone)":
			parcelScore = 1000;
			speed = 2.5f * speed;
			break;
		default:
			Debug.Log ("ParcelScript switch statement error");
			break;
		}
			
	}

	// Update is called once per frame
	void Update () {

		//for the oscillation of the rotation. Lerp = a point between 2 points, using sine function makes it bounce between.
		time += Time.deltaTime;	//this is so that each object will start oscillating when instantiated, and not all synchronised
		float t = (Mathf.Sin (time * frequency * Mathf.PI * 2.0f) + 1.0f) / 2.0f;
		gameObject.transform.eulerAngles = Vector3.Lerp (from, to, t);

		//to move parcels downwards
		gameObject.transform.Translate(0, -Time.deltaTime * speed, 0, Space.Self);



	}

	//upon trigger with a container
	void ItemCaught(){
		YFGameManager.score += parcelScore;
		YFGameManager.parcelsCaught++;
		AudioManager.instance.PlaySound ("Catch Parcel");
		speed = 0;
		float displacement = gameObject.GetComponent<SpriteRenderer> ().bounds.size.y;
		gameObject.transform.Translate(0, -displacement, 0, Space.Self);
		gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
		gameObject.GetComponent<Animator> ().runtimeAnimatorController = parcelPoints;
		gameObject.GetComponent<Animator> ().enabled = true;
		StartCoroutine (WaitAnimation ());
	}

	//upon trigger with the floor of background
	void HitGround(){
		speed = 0;
		int x = Random.Range (0, 2);
		switch (x) {
		case 0:
			AudioManager.instance.PlaySound ("Parcel Drop 1");
			break;
		case 1:
			AudioManager.instance.PlaySound ("Parcel Drop 2");
			break;
		}

		float displacement = gameObject.GetComponent<SpriteRenderer> ().bounds.size.y;
		gameObject.transform.Translate(0, -displacement, 0, Space.Self);
		gameObject.transform.localScale = new Vector3 (0.1260495f, 0.1260495f, 0.1260495f);
		gameObject.GetComponent<Animator> ().enabled = true;
		StartCoroutine (WaitAnimationMinus ());
	}

	IEnumerator WaitAnimation(){
		yield return new WaitForSeconds ((5f/6f)*gameObject.GetComponent<Animator> ().runtimeAnimatorController.animationClips [0].length);
		GameObject.Destroy (gameObject);
	}

	IEnumerator WaitAnimationMinus(){
		yield return new WaitForSeconds ((5f/6f)*gameObject.GetComponent<Animator> ().runtimeAnimatorController.animationClips [0].length);
		GameObject.Find ("GameManager").SendMessage ("MinusLife");
		GameObject.Destroy (gameObject);
	}
}