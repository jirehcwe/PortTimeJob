using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	static public LevelManager instance = null;
	static public int gameCount = 0;
	public int maxGames;
	static public int fishingLives;

	void Awake(){
		maxGames = 3;
		fishingLives = 3;
	}

	void Start(){
		if (instance != null) {
			GameObject.Destroy (gameObject);
		} else {
			GameObject.DontDestroyOnLoad (gameObject);
			instance = this;
		}
		AudioManager.instance.PlaySound ("Crate Fishing BGM");

	}

	void GameEnded(){
		if (SceneManager.GetActiveScene ().name == "CrateFishing") {
			gameCount += 1;
			if (gameCount < maxGames) {
				ReloadScene ();
			} else {
				//play animation
				SceneManager.LoadScene ("CatchingItems");
				GameObject.Destroy (gameObject);
			}
		}
		if (SceneManager.GetActiveScene ().name == "CatchingItems") {

		}
	}

	void ReloadScene(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name, LoadSceneMode.Single);
	}

	public void DestroyLevelManager(){
		GameObject.Destroy (gameObject);
	}

	public void GameWon(){
		if (PlayerPrefs.GetInt ("haveWonGame1") == 0) {

		}

	}

	public void GameLost(){
		if (PlayerPrefs.GetInt ("haveWonGame1") == 0 && PlayerPrefs.GetInt ("haveLostGame1") == 0) {

		}
	}
}