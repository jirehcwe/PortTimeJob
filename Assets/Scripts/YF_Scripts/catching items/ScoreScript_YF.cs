using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript_YF : MonoBehaviour {

	[SerializeField]
	public int score;
	private int changeScore;

	public bool doneLoading = false;

	// Use this for initialization
	void Start () {
		score = YFGameManager.score;
	}

	private void Update()
	{
		if(changeScore < score)
		{
			GetComponent<Text>().text = "" + changeScore;
			changeScore+=123;
		}
		else
		{
			GetComponent<Text>().text = "" + score;
			doneLoading = true;
		}
	}
}
