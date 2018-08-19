using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YFGameManager : MonoBehaviour {

	//variables used by other classes
	static public Vector3 origin, limit;
	static public bool gameOver = false;
	//scores
	static public int score = 0;
	static public int endScore = 0;
	static public int parcelsCaught = 0;
	static public int junkCaught = 0;
	static public int lives = 3;

	public int parcelCount = 0;
	public GameObject[] parcels;
	public GameObject[] junk;
	public GameObject flyingLetter;
	private float parcelSpawnTime = 0;
	private float junkSpawnTime = 0;
	static public float baseSpeed;

	private bool gameStarted = false;
	public Text scoreText;
	public Canvas gameCanvas;


	public GameObject[] hearts;
	private int fps = 90;

	public GameObject scoreScreen;

	// Use this for initialization
	void Start () {

		//setting static variables
		origin = GameObject.Find ("Camera").GetComponent<Camera> ().ViewportToWorldPoint (new Vector3 (0, 0, 0));
		limit = GameObject.Find ("Camera").GetComponent<Camera> ().ViewportToWorldPoint (new Vector3 (1, 1, 1));
		gameOver = false;
		score = 0;
		endScore = 0;
		parcelsCaught = 0;
		junkCaught = 0;
		lives = 3;
		baseSpeed = 0;


		for (int i = 0; i < lives; i++) {
			hearts [i].SetActive(true);
		}

		AudioManager.instance.PlaySound ("Catching Items BGM");
	}
	
    public void WinHack()
    {
		AudioManager.instance.PlayCommonSound("Yoshi");
		score = 10001;
		parcelsCaught = 20;
		junkCaught = 5;
		transform.GetChild(0).gameObject.SetActive(false);
		gameStarted = true;
		gameOver = true;
		scoreScreen.SetActive(true);
    }

	// Update is called once per frame
	void Update () {

		// THIS IS FOR TESTING !!!!!
		// PRESS W TO WIN
		if (Input.GetKeyDown(KeyCode.W))
		{
			score = 10001;
			parcelsCaught = 20;
			junkCaught = 5;
			transform.GetChild (0).gameObject.SetActive (false);
			gameStarted = true;
			gameOver = true;
			scoreScreen.SetActive (true);
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			Pause ();
		}
		if (Input.GetKeyDown (KeyCode.U)) {
			Resume ();
		}


		if (!gameStarted) {
			//display instruction text
			//sets gameStarted to true after showing all instructions
			Instructions();

		} else {
			if (gameOver) {
				return;
			}

			scoreText.text = "Score: " + score.ToString ();

			//start spawning parcels and junk
			ParcelManager();
			JunkManager ();
			FlyingLetterManager ();

			baseSpeed = 1.0f + Mathf.Log10 ((score+200) / 200);
		}
	}

	void ParcelManager(){
		//parcels spawn faster as score gets higher
		float difficulty = 6f - Mathf.Log(score/100+1,3);
		//higher scores will unlock new parcels which are worth more points
		if (difficulty < 0.5f) {
			difficulty = 0.5f;
		}

		int typesOfParcels = 2;
		if (score > 4000) {
			typesOfParcels = 5;
		} else if (score > 2000) {
			typesOfParcels = 4;
		} else if (score > 1000) {
			typesOfParcels = 3;
		} else {
			typesOfParcels = 2;
		}
		if (parcelSpawnTime < Time.time) {
			int parcelIndex = Random.Range (0, typesOfParcels);
			float xPos = Random.Range (origin.x, limit.x);
			Instantiate (parcels [parcelIndex], new Vector3 (xPos, 1.5f*limit.y, -0.5f), Quaternion.identity);
			parcelSpawnTime = Time.time + Random.Range (0.2f, difficulty);
		}
	}

	void JunkManager(){
		if (score < 1000) {
			return;
		}

		//junk items spawn faster as score gets higher
		float difficulty = 13f - Mathf.Log(score/100+1,5);
		if (difficulty < 1.3f) {
			difficulty = 1.3f;
		}

		//higher scores will unlock new junk which are deduct more points
		//possible to include more junk item types
		int typesOfJunk = 1;
		/*if (score > 75) {
			typesOfParcels = 5;
		} else if (score > 50) {
			typesOfParcels = 4;
		} else */if (score> 2500) {
			typesOfJunk = 2;
		}

		if (junkSpawnTime < Time.time) {
			int junkIndex = Random.Range (0, typesOfJunk);
			float xPos = Random.Range (origin.x, limit.x);
			Instantiate (junk [junkIndex], new Vector3 (xPos, limit.y, -0.5f),junk[junkIndex].transform.rotation);
			junkSpawnTime = Time.time + Random.Range (0.5f, difficulty);
		}
	}

	void FlyingLetterManager(){
		//spawns a flying letter an average rate of ~ one per min
		int random1 = Random.Range(0,fps*120);

		//check value of random1 against an arbituary constant
		if (random1 == 345) {
			Instantiate (flyingLetter, new Vector3 (origin.x - 0.5f, limit.y * 0.6f, -0.5f), Quaternion.identity);
		} else if (random1 == 9000) {
			Instantiate (flyingLetter, new Vector3 (limit.x + 0.5f, limit.y * 0.6f, -0.5f), Quaternion.identity);
		}
	}

	void MinusLife(){
		hearts [lives-1].SetActive(false);
		lives--;
		if (lives <= 0) {
			Time.timeScale = 0;
			baseSpeed = 0;
			gameOver = true;
			scoreScreen.SetActive (true);
		}
	}

	void AddLife(){
		if(lives>=5)
			return;
		hearts [lives].SetActive (true);
		lives++;
	}

	void Instructions(){
		if (Input.touchCount != 0) {
			if (Input.touches [0].phase == TouchPhase.Began) {
				gameObject.GetComponent<FadeInScript> ().enabled = false;
				AudioManager.instance.PlayCommonSound ("click");
				int n = transform.GetChild (0).GetComponent<InstructionScript> ().NextSprite ();
				if (n == -1) {
					transform.GetChild (0).gameObject.SetActive (false);
					gameCanvas.gameObject.SetActive (true);
					baseSpeed = 1.0f;
					gameStarted = true;
				}

				/*if (instructionStringIndex < instructionString.Length) {
					instructionsText.text = instructionString [instructionStringIndex];
					instructionStringIndex++;
				} else {
					instructionsText.gameObject.SetActive (false);
					gameStarted = true;
				}*/
			}
		}
	}

	public void Pause(){
		Time.timeScale = 0;
	}
	public void Resume(){
		Time.timeScale = 1;
	}
}