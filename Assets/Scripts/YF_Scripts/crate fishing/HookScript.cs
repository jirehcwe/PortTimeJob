using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour {

	public Sprite hookWithContainer;

	public float speed;
	public float sidewaysSpeed;
	private Vector3 target;

	private Sprite container;

	private GameObject main;
	private PlayerInput mainScript;
//	private GameObject scroller;
//	private Scroller scrollerScript;
	public float hookYPosition;

	private bool endReached;
	private bool isEndingAnimation = false;
	private bool stopCallForAnimation = false;
	private bool hookStartingSoundStopped = false;

	// Use this for initialization
	void Start () {
		main = GameObject.Find ("PlayerInput");
		mainScript = main.GetComponent<PlayerInput>();

//		scroller = GameObject.Find ("Background");
//		scrollerScript = main.GetComponent<Scroller>();

		hookYPosition = mainScript.originY + (mainScript.limitY-mainScript.originY) * 3 / 7;		//position of hook relative to screen size
		speed = 0;
		target = new Vector3 (gameObject.transform.position.x, hookYPosition, gameObject.transform.position.z);
		endReached = false;
	}

	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y > hookYPosition) {
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, target, speed*2f);
			return;
		}

		if (!hookStartingSoundStopped) {
			AudioManager.instance.StopSound ("Hook Moving");
			hookStartingSoundStopped = true;
		}

		//after hook reaches middle position, tilting the device will control lateral movement of hook.
		transform.Translate (Input.acceleration.x * sidewaysSpeed*Time.deltaTime*25f, 0, 0);

		//prevents hook from going out of screen
		if (transform.position.x <= mainScript.originX) {
			transform.position = new Vector3 (mainScript.originX, transform.position.y, transform.position.z);
		}
		if (transform.position.x >= mainScript.limitX) {
			transform.position = new Vector3 (mainScript.limitX, transform.position.y, transform.position.z);
		}

		if (endReached) {
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, target, speed);
			if (isEndingAnimation && gameObject.transform.position == target) {
				if (!stopCallForAnimation) {
					main.SendMessage ("StartEndingCoroutine");
					AudioManager.instance.StopSound ("Control Hook");
					stopCallForAnimation = true;
				}
			}

			return;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		other.gameObject.SendMessage ("CollisionDetected");
		main.SendMessage ("MinusLife");
	}

	void ChangeSprite(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = hookWithContainer;
		AudioManager.instance.PlaySound ("Pick up container");
		gameObject.transform.localScale = new Vector3 (0.1863818f, 0.1863818f, 0.1863818f);
		hookYPosition = mainScript.originY + (mainScript.limitY-mainScript.originY) * 5 / 7;		//hook moves up
		target.y = hookYPosition;
	}

	void ControlHook(){
		sidewaysSpeed = 0;	//stops the player from being able to control hook
		float hookXPosition = mainScript.originX + (mainScript.limitX-mainScript.originX) / 2f;
		hookYPosition = mainScript.originY + (mainScript.limitY-mainScript.originY) * 1 / 4;	//hook moves down to the ship
		target = new Vector3 (hookXPosition, hookYPosition, gameObject.transform.position.z);
		endReached = true;
		speed *= 2;

		gameObject.GetComponent<BoxCollider2D> ().isTrigger = false;
	}

	void MoveHookUp(){
		hookYPosition = mainScript.limitY + gameObject.GetComponent<SpriteRenderer> ().bounds.size.y*0.2f;
		target.y = hookYPosition;
		isEndingAnimation = true;
	}
}
