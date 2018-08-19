using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreScript_YF : MonoBehaviour {

	[SerializeField]
	public string highscore;
	[SerializeField]
	private GameObject scoreLocation;

	private void Awake()
	{
		//PlayerPrefs.SetInt("Symmetry Highscore", 0);
		if (YFGameManager.score > PlayerPrefs.GetInt(highscore))
		{
			PlayerPrefs.SetInt(highscore, YFGameManager.score);
			AudioManager.instance.PlayCommonSound ("Highscore");
		}

		GetComponent<Text>().text = "" + PlayerPrefs.GetInt(highscore);
	}
}

