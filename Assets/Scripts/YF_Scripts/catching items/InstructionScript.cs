using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionScript : MonoBehaviour {

	public Sprite[] instructions;
	private int arrayIndex;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer> ().sprite = instructions [0];
		arrayIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int NextSprite(){
		arrayIndex++;
		if (arrayIndex < instructions.Length) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = instructions [arrayIndex];
			return arrayIndex;
		} else {
			return -1;
		}
	}
}