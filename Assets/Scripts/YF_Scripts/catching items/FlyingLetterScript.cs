using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingLetterScript : MonoBehaviour {


	public Sprite heart;
	public RuntimeAnimatorController heartpop;
	private Camera cam;

	private bool isTouched;
	private float speed;
	private int bounceCount;
	// Use this for initialization
	void Start () {
		cam = GameObject.Find("Camera").GetComponent<Camera>();
		isTouched = false;
		speed = 1.0f;
		bounceCount = 0;

		if (gameObject.transform.position.x > YFGameManager.limit.x) {
			speed *= -1;
			gameObject.transform.eulerAngles = new Vector3(0,0,10f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//fly around screen twice then out
		//either appear left or right
		//on tap, will drop a heart-->change sprite and drop down OR pop into a heart and fly upwards.
			//problem with dropping heart is if player misses the heart, how will he pick it up.
		//letter's wings should be flapping
		//upon touch, it will pop in a puff of smoke
		if (!isTouched) {
			if (bounceCount < 2) {
				if (gameObject.transform.position.x < YFGameManager.origin.x) {
					speed *= -1;
					gameObject.transform.eulerAngles = new Vector3(0,0,-10f);
					bounceCount++;

				}

				if (gameObject.transform.position.x > YFGameManager.limit.x) {
					speed *= -1;
					gameObject.transform.eulerAngles = new Vector3(0,0,10f);
					bounceCount++;
				}
			} else if (gameObject.transform.position.x > YFGameManager.limit.x + 0.5f || gameObject.transform.position.x < YFGameManager.origin.x - 0.5f) {
				GameObject.Destroy (gameObject);
			}
			gameObject.transform.Translate (Time.deltaTime * speed, 0, 0, Space.World);

			//detects touch
			foreach (Touch touch in Input.touches) {
				if (touch.phase == TouchPhase.Began) {
					Vector2 worldPoint = cam.ScreenToWorldPoint (touch.position);
					RaycastHit2D hit = Physics2D.Raycast (worldPoint, Vector3.forward, 1000f, 1 << 8);
					if (hit.collider != null) {
						Touched ();
					}
				}
			}
		} else {
			//wait until animation is done then destroy itself
		}
	}

	void Touched(){
		//animation of cloud ?
		//change sprite
		//falling heart
		//blinks or smth upon hitting agv
		//add life

		gameObject.GetComponent<SpriteRenderer>().sprite = heart;
		transform.rotation = Quaternion.identity;
		transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
		AudioManager.instance.PlaySound ("Collect Heart");
		gameObject.GetComponent<Animator> ().runtimeAnimatorController = heartpop;
		StartCoroutine (WaitAnimation ());
		isTouched = true;
	}

	IEnumerator WaitAnimation(){
		yield return new WaitForSeconds (heartpop.animationClips [0].length);
		GameObject.Find ("GameManager").SendMessage ("AddLife");
		GameObject.Destroy (gameObject);
	}
}
