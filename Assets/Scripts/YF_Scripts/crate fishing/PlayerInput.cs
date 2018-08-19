using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//named thus as initial plan was for this to just record input. things changed.
public class PlayerInput : MonoBehaviour {

	public GameObject levelManager;
	public Camera cam;
	public Canvas gameCanvas;

	//for referencing other GameObjects and scripts attached to them
	private Scroller scrollerScript;
	private GameObject background;
	private HookScript hookScript;
	private GameObject hook;

	//variables related to obstacles
	public GameObject[] obstacle;
	private GameObject[] spawnedObstacles;	//array which stores all instantiated obstacles
	private int maxObstacles;
	private int obstacleCount;

	public int containerDepth;						//depth at which container appears.

	private bool gameStarted;
	private bool gameOver;
	public bool stopSpawning;
	private bool startedFading;


	//origin and limit coordinates relative to camera view. Use Camera.ViewportToWorldPoint
	[HideInInspector]public float originX, originY, originZ, limitX, limitY, limitZ;


	private float spawnTimeOne, spawnTimeTwo, spawnTimeThree, spawnTimeFour;

	//Awake is called before all Starts.
	//variables that other scripts need in their Start() calls
	void Awake(){
		if (LevelManager.instance == null) {
			Instantiate (levelManager);
		}

		Time.timeScale = 1;

		//origin and limit coordinates
		originX = cam.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		originY = cam.ViewportToWorldPoint (new Vector3 (0, 0, 0)).y;
		originZ = cam.ViewportToWorldPoint (new Vector3 (0, 0, 0)).z;
		limitX = cam.ViewportToWorldPoint (new Vector3 (1, 1, 1)).x;
		limitY = cam.ViewportToWorldPoint (new Vector3 (1, 1, 1)).y;
		limitZ = cam.ViewportToWorldPoint (new Vector3 (1, 1, 1)).z;
	}

	void Start () {
		//referencing scripts
		background = GameObject.Find ("Background");
		scrollerScript = background.GetComponent<Scroller> ();
		hook = GameObject.Find ("Hook");
		hookScript = hook.GetComponent<HookScript> ();

		//variables
		scrollerScript.speed = 0;
		gameOver = false;
		gameStarted = false;
		stopSpawning = false;
		startedFading = false;

		//obstacle variables
		maxObstacles = 10;
		spawnedObstacles = new GameObject[maxObstacles];
		obstacleCount = 0;

		containerDepth = 100 + LevelManager.gameCount * 60;


		spawnTimeOne = 0; spawnTimeTwo = 0; spawnTimeThree = 0; spawnTimeFour = 0;

		for (int i = 0; i < LevelManager.fishingLives; i++) {
			gameObject.transform.GetChild (i).gameObject.SetActive (true);
		}

	}

	// Update is called once per frame

	void Update () {

        // THIS IS FOR TESTING !!!!!
        // PRESS W TO WIN
        if (Input.GetKeyDown(KeyCode.W))
        {
            // makes sure that scenes are not loaded onto each other ??
            //SceneManager.LoadScene("Menu",LoadSceneMode.Single);

            //PlayerPrefs.SetInt("chatScene",1);
			AudioManager.instance.PlayCommonSound("Yoshi");
			LevelManager.gameCount = 2;
			hook.GetComponent<BoxCollider2D> ().isTrigger = false;
			Pause ();
        }

		// PRESS L TO LOSE
        if (Input.GetKeyDown(KeyCode.L))
		{
//            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
//
//			PlayerPrefs.SetInt("chatScene", 2);
			Resume();
        }

		if (gameOver) {
			if (Input.touchCount != 0) {
				AudioManager.instance.PlayCommonSound ("click");
				LevelManager.gameCount = 0;
				LevelManager.fishingLives = 3;
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name, LoadSceneMode.Single);
			}
		}

		//moves hook down to central position(EDIT IN HOOKSCRIPT.CS). setting speed to non zero starts move towards function in hookscript.
		if (hookScript.speed == 0) {
			if (LevelManager.gameCount == 0) {
				gameObject.transform.GetChild (3).gameObject.SetActive(true);
				if (Input.touchCount != 0) {
					Instructions ();
				}
			} else {
				if (Input.touchCount != 0) {
					hookScript.speed = 0.05f;
				}
			}
			return;
		}
			
		ScrollerManager ();

		//spanws obstacles by calling each manager
		if (!stopSpawning) {
			
			if (scrollerScript.depth > 0) {
			
				Obstacle1Manager ();

				if (scrollerScript.depth > 50) {
					Obstacle2Manager ();
				}

				if (scrollerScript.depth > 100) {
					Obstacle3Manager ();
				}

				if (scrollerScript.depth > 140) {
					Obstacle4Manager ();
				}
			}
		}

	}

	//destroys obstacle, frees up index in spawnedObstacle array.
	void DestroyObstacle(int index){
		obstacleCount--;
		Destroy (spawnedObstacles [index]);
		spawnedObstacles [index] = null;
	}

	//instantiate obstacle into the index of the spawnedObstacles array
	void InstantiateObstacle(int obstacleNumber, int index){
		//random x position along bottom of camera
		float xPos = cam.ViewportToWorldPoint (new Vector3 (Random.Range (0f, 1.0f), 0, 0)).x;
		//instantiates
		spawnedObstacles [index] = Instantiate (obstacle[obstacleNumber], new Vector3 (xPos, originY, hook.transform.position.z), Quaternion.identity);
		//randoms direction
		if (Random.Range (0,2) == 0) {
			spawnedObstacles [index].GetComponent<ObstacleScriptLeft> ().isLeft = true;
		} else {
			spawnedObstacles [index].GetComponent<ObstacleScriptLeft> ().isLeft = false;
		}
		//saves index
		spawnedObstacles [index].GetComponent<ObstacleScriptLeft> ().index = index;

		obstacleCount++;
	}


	void Obstacle1Manager(){
		if (Time.time < spawnTimeOne || obstacleCount >= maxObstacles) {
			return;
		}

		int i;
		//finding an empty "null" index to store new obstacle in
		for (i = 0; i < maxObstacles; i++) {
			if (spawnedObstacles [i] == null) {
				break;
			}
		}
		InstantiateObstacle (0,i);

		spawnTimeOne = Time.time + Random.Range (0f, 3f);		//possible to use variables to vary frequency

	}

	void Obstacle2Manager(){
		if (Time.time < spawnTimeTwo || obstacleCount >= maxObstacles) {
			return;
		}

		int i;
		//finding an empty "null" index to store new obstacle in
		for (i = 0; i < maxObstacles; i++) {
			if (spawnedObstacles [i] == null) {
				break;
			}
		}
		InstantiateObstacle (1,i);

		spawnTimeTwo = Time.time + Random.Range (2f, 5f);		//possible to use variables to vary frequency

	}

	void Obstacle3Manager(){
		if (Time.time < spawnTimeThree || obstacleCount >= maxObstacles) {
			return;
		}

		int i;
		//finding an empty "null" index to store new obstacle in
		for (i = 0; i < maxObstacles; i++) {
			if (spawnedObstacles [i] == null) {
				break;
			}
		}
		InstantiateObstacle (2,i);

		spawnTimeThree = Time.time + Random.Range (3f, 6f);		//possible to use variables to vary frequency

	}

	void Obstacle4Manager(){
		if (Time.time < spawnTimeFour || obstacleCount >= maxObstacles) {
			return;
		}

		int i;
		//finding an empty "null" index to store new obstacle in
		for (i = 0; i < maxObstacles; i++) {
			if (spawnedObstacles [i] == null) {
				break;
			}
		}
		InstantiateObstacle (3,i);

		spawnTimeFour = Time.time + Random.Range (3f, 5f);		//possible to use variables to vary frequency

	}

	void ScrollerManager(){
		if (!gameStarted) {
			if (hook.transform.position.y <= hookScript.hookYPosition) {
				background.SendMessage ("StartScroll");
				gameStarted = true;
			} else
				return;
		} else {
			if (scrollerScript.depth > containerDepth) {
				background.SendMessage ("BottomReached");
				stopSpawning = true;
			}
		}
	}

	void MinusLife(){
		if (!stopSpawning) {
			gameObject.transform.GetChild (LevelManager.fishingLives - 1).gameObject.SetActive (false);
			LevelManager.fishingLives--;
			if (LevelManager.fishingLives == 0) {
				Time.timeScale = 0;
				gameCanvas.gameObject.transform.GetChild (3).gameObject.SetActive (true);
				gameOver = true;

			}
		}
	}

	void Instructions(){
		if (Input.touches [0].phase == TouchPhase.Began) {
			int i = gameObject.transform.GetChild (3).GetComponent<FishInstructionScript>().NextPage();
			if (i == -1) {
				AudioManager.instance.PlaySound ("Hook Moving");
				hookScript.speed = 0.05f;	//starts hook moving
				gameObject.transform.GetChild (3).gameObject.SetActive(false);
			}
		}
	}

	void DeactivateHearts(){
		gameObject.transform.GetChild (0).gameObject.SetActive (false);
		gameObject.transform.GetChild (1).gameObject.SetActive (false);
		gameObject.transform.GetChild (2).gameObject.SetActive (false);
	}

	void StartEndingCoroutine(){
		if (!startedFading) {
			StartCoroutine (FadeOut ());
			startedFading = true;
		}
	}

	IEnumerator FadeOut(){
		Debug.Log ("activating sprite");
		gameCanvas.transform.GetChild (4).gameObject.SetActive (true);
		AudioManager.instance.PlaySound ("Explosion");
		yield return new WaitForSecondsRealtime (1.2f);

		gameObject.GetComponent<FadingScript> ().enabled = true;
		//float fadeTime = gameObject.GetComponent<FadingScript> ().BeginFade (-1);
		yield return new WaitForSecondsRealtime (1.5f);
		LevelManager.instance.SendMessage ("GameEnded");
	}

//	void PhoneCheat(){
//		AudioManager.instance.PlayCommonSound ("Yoshi");
//		LevelManager.gameCount = 2;
//		hook.GetComponent<BoxCollider2D> ().isTrigger = false;
//		Pause ();
//	}

	public void Pause(){
		Time.timeScale = 0;
	}

	public void Resume(){
		Time.timeScale = 1;
	}
}