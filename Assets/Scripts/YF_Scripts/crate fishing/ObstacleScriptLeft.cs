using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is named thus as the initial plan was to have a script for both left and right. that was stupid. but it changed.
public class ObstacleScriptLeft : MonoBehaviour {


	Scroller scrollerScript;
	GameObject background;

	PlayerInput mainScript;					//to access limitCoordinates Vector3.
	GameObject main;

	private float upwardSpeed;
	private float sidewaysSpeed;
	private string audioClipName;
	public RuntimeAnimatorController collisionAnimation;
	[HideInInspector] public bool isLeft; 	//true if going left the next frame. false if right. value is set upon instatiation
	[HideInInspector] public int index;		//index of the object in the array in PlayerInput

	// Use this for initialization
	void Start () {
		//referencing speed from background scroller, so that it is "moving upwards" at the same speed as background
		background = GameObject.Find ("Background");
		scrollerScript = background.GetComponent<Scroller> ();
		this.upwardSpeed = scrollerScript.maxSpeed;
		sidewaysSpeed = Random.Range(0.5f,1f);

		//referencing main script
		main = GameObject.Find ("PlayerInput");
		mainScript = main.GetComponent<PlayerInput> ();

		if (!isLeft) {
			gameObject.GetComponent<SpriteRenderer> ().flipX = true;
		}

		//find out which obstacle the script is attached to. change values of speed accordingly
		switch (gameObject.name) {
		case "Obstacle1(Clone)":			//bird
			audioClipName = "Bird";
			break;
		case "Obstacle2(Clone)":			//balloon
			audioClipName = "Balloon";
			this.upwardSpeed *= 2;
			break;
		case "Obstacle3(Clone)":			//superman
			audioClipName = "Superman";
			sidewaysSpeed *= 3;
			break;
		case "Obstacle4(Clone)":			//bee
			audioClipName = "Bee";
			sidewaysSpeed /= 2;
			break;
		default:
			Debug.Log ("error " + gameObject.name);
			break;
		}

	}
	
	// Update is called once per frame
	void Update () {
		//script to move object left or right
		if (isLeft) {
			gameObject.transform.Translate(-Time.deltaTime * sidewaysSpeed, Time.deltaTime * upwardSpeed, 0, Space.Self);
		} else {
			gameObject.transform.Translate(Time.deltaTime * sidewaysSpeed, Time.deltaTime * upwardSpeed, 0, Space.Self);
		}

		//when object hits left or right border, change isLeft value.
		//change orientation of sprite to face the correct direction
		if (transform.position.x <= mainScript.originX) {
			transform.position = new Vector3 (mainScript.originX, transform.position.y, transform.position.z);
			isLeft = false;
			gameObject.GetComponent<SpriteRenderer> ().flipX = true;
		}

		if (transform.position.x >= mainScript.limitX) {
			transform.position = new Vector3 (mainScript.limitX, transform.position.y, transform.position.z);
			isLeft = true;
			gameObject.GetComponent<SpriteRenderer> ().flipX = false;
		}

		if (scrollerScript.depth > mainScript.containerDepth) {
			upwardSpeed = scrollerScript.maxSpeed *3;
		}

		if (transform.position.y >= (mainScript.limitY + GetComponent<SpriteRenderer> ().size.y*gameObject.transform.localScale.y)) {		//when object flies above screen, destroy it
			main.SendMessage ("DestroyObstacle", index);		//calls DestroyObstacle function in PlayerInput

		}
	}

	void CollisionDetected(){
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		AudioManager.instance.PlaySound (audioClipName);
		if (collisionAnimation != null) {
			gameObject.GetComponent<Animator> ().runtimeAnimatorController = collisionAnimation;
			gameObject.GetComponent<Animator> ().enabled = true;

			StartCoroutine (WaitSeconds(collisionAnimation.animationClips [0].length));
		}
	}

	IEnumerator WaitSeconds(float seconds){
		yield return new WaitForSecondsRealtime (seconds);
		Destroy (gameObject);
	}
}