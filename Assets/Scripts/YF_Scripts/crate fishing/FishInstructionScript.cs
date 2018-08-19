using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInstructionScript : MonoBehaviour {

	public Sprite[] instructions;
	int pageIndex;

	// Use this for initialization
	void Start () {
		pageIndex = 0;
	}

	public int NextPage(){
		pageIndex++;
		AudioManager.instance.PlayCommonSound ("click");
		if (pageIndex < instructions.Length) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = instructions [pageIndex];
			return pageIndex;
		} else {
			return -1;
		}
	}
}
