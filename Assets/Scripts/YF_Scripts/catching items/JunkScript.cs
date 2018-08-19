using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkScript : MonoBehaviour {

	public RuntimeAnimatorController junkPoints;

	private float speed = YFGameManager.baseSpeed;
	private int pointsReduction;
	// Use this for initialization
	void Start () {
		
		switch(gameObject.name){
		case "Junk1(Clone)":
			speed = 1.5f*speed;
			pointsReduction = 300;
			break;
		case "Junk2(Clone)":
			speed = 0.5f*speed;
			pointsReduction = 500;
			break;
		default:
			Debug.Log ("JunkScript switch(name) error");
			break;
		}

		if (Random.Range (0, 2) == 0) {
			gameObject.GetComponent<SpriteRenderer> ().flipX = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(0, -Time.deltaTime * speed, 0, Space.World);
	}

	//upon trigger with container
	void ItemCaught(){
		speed = 0;
		YFGameManager.score -= pointsReduction;
		YFGameManager.junkCaught++;
		gameObject.GetComponent<Animator> ().runtimeAnimatorController = junkPoints;
		gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
		gameObject.transform.rotation = Quaternion.identity;
		gameObject.GetComponent<SpriteRenderer> ().flipX = false;
		gameObject.GetComponent<Animator> ().enabled = true;
		AudioManager.instance.PlaySound ("splat");
		StartCoroutine (WaitAnimation ());
	}

	//upon trigger with the floor of background
	void HitGround(){
		GameObject.Destroy (gameObject);
	}

	IEnumerator WaitAnimation(){
		yield return new WaitForSeconds ((5f/6f)*gameObject.GetComponent<Animator> ().runtimeAnimatorController.animationClips [0].length);
		GameObject.Destroy (gameObject);
	}
}
