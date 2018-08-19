using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [Header("Intro and Gameplay Objects")]
    public GameObject introSceneTrucks;
    public GameObject truckSpawner;
	public GameObject buttons;
    public GameObject startingTruck;
    public GameObject startingTruckPrefab;
    public GameObject lights;
    public bool gameIsActive;

    [Header("UI")]
    public GameObject restartScreen;
    public GameObject Canvas;
    public GameObject tapInstructions;
    public GameObject tapToStart;
    public GameObject score;
    public Text scoreText;
    public GameObject Lives;
    public GameObject skipButton;
    public static int gameScore = 0;
    public GameObject pauseCanvas;
    public GameObject pauseButton;
    public Image[] gameLivesArray;

    float ColorInterpolator = 0;
    public static int gameLives = 3;
    public static int noOfButtons = 4;


    [Header("Misc")]
    public GameObject map;
    public GameObject MainCamera;

    [Header("Audio")]
    public bool BGMPlaying;


    Text flashText;

    public static bool introSceneFinished = false;
	public static bool startingTruckSequenceFinished = false;
    public static bool wavesOnScreen = true;
    public static bool gamePaused = false;
    AudioSource truckMoveSound;

    void Awake()
    {
        introSceneFinished = false;
        TruckSpawner.trucksActive = 0;
        GameManager.gameLives = 3;
        ButtonScript.truckCrash = false;
        GameManager.startingTruckSequenceFinished = false;
        map.SetActive(true);
        MainCamera.SetActive(false);
        tapToStart.SetActive(false);
        wavesOnScreen = true;
        BGMPlaying = false;
        introSceneTrucks.SetActive(false);
        gamePaused = false;
        ColorInterpolator = 0;
    }

	void Start(){
        truckMoveSound = AudioManager.instance.GetSource("truckMoveLoop");
        introSceneFinished = false;
        TruckSpawner.trucksActive = 0;
        GameManager.gameLives = 3;
        ButtonScript.truckCrash = false;
        GameManager.startingTruckSequenceFinished = false;
        StartingTruckBehaviour.startingTruckIsOffScreen = true;
        wavesOnScreen = false;
        BGMPlaying = false;
        introSceneTrucks.SetActive(false);
        gamePaused = false;
        pauseCanvas.SetActive(true);
        ColorInterpolator = 0;

        tapToStart.SetActive(false);
        map.SetActive(true);
        MainCamera.SetActive(false);
        gameScore = 0;
        score.SetActive(false);
        Lives.SetActive(false);
        truckSpawner.SetActive (false);
		flashText = tapToStart.GetComponentInChildren<Text> ();
		flashText.text = "TAP TO START";
//        InvokeRepeating("flashTheText", 14f, 0.4f);
        buttons.SetActive (false);


        gameLivesArray = Lives.GetComponentsInChildren<Image>();


        AudioManager.instance.PlaySound("truckMoveLoop");
        truckMoveSound.volume = 0.3f;
    }
    // Update is called once per frame
    void Update() {
        /*
		// THIS IS FOR TESTING!!!
		// PRESS W TO WIN
		if (Input.GetKeyDown(KeyCode.W))
		{

//            PlayerPrefs.SetInt("toScene", 0);
            PlayerPrefs.SetInt("chatScene", 3);
//            SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);

        }

		// PRESS L TO LOSE
		if (Input.GetKeyDown(KeyCode.L))
		{
 //           PlayerPrefs.SetInt("toScene", 0);
            PlayerPrefs.SetInt("chatScene", 4);
            //           SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }

        if (Input.GetKey("s"))
        {
            gameScore += 20000;
        }
        */
        if (truckMoveSound.volume != 0.8)
        {
            truckMoveSound.volume += 0.02f * Time.deltaTime;
        }
        
        if (introSceneFinished == true)
        {
            if (!BGMPlaying)
            {
                BGMPlaying = true;
                AudioManager.instance.PlaySound("BGM");
            }
            startScene();
            AudioManager.instance.StopSound("truckMoveLoop");
//            AudioManager.instance.PlaySound("BGM");
        }

        
        

        scoreText.text = "" + gameScore;

        for (int i = 3; i > gameLives; i--)
        {
            gameLivesArray[i].gameObject.SetActive(false);
        }

        if (gameLives == 0)
        {
//          Debug.Log("score screen");
            ButtonScript.truckCrash = true;
            restartScreen.SetActive(true);
//            Debug.Log("score screen");
        }

        if ((TruckSpawner.GetLevel() == 1 || TruckSpawner.GetLevel() == 2) && startingTruckSequenceFinished == true)
        {
            tapInstructions.SetActive(true);
        } else
        {
            tapInstructions.SetActive(false);
        }
//        Debug.Log("starting seq " + startingTruckSequenceFinished);
//       Debug.Log("starting seq off screen " + StartingTruckBehaviour.startingTruckIsOffScreen);
//       Debug.Log("starting seq finished " + startingTruckSequenceFinished);
    }

	void flashTheText(){
		if (tapToStart.activeInHierarchy){
			tapToStart.SetActive (false);
		} 
		else {
			tapToStart.SetActive (true);
		}
	}

	void startScene(){

			CancelInvoke ("flashTheText");
			tapToStart.SetActive (false);
			gameIsActive = true;

            skipButton.SetActive(false);

            if (StartingTruckBehaviour.startingTruckIsOffScreen == true){
                startingTruck = Instantiate(startingTruckPrefab, Canvas.transform);
//              Debug.Log("spawning starting truck");
				StartingTruckBehaviour.startingTruckIsOffScreen = false;
			}

		if (startingTruckSequenceFinished == true) { 
			truckSpawner.SetActive (true);
            score.SetActive(true);
            Lives.SetActive(true);
            buttons.SetActive(true);
            lights.SetActive(false);
            pauseCanvas.SetActive(true);
            //pauseButton.transform.GetChild(0).GetComponent<Image>().color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), 0.3f*ColorInterpolator);
           // pauseButton.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), 0.3f*ColorInterpolator);
            ColorInterpolator += Time.deltaTime;

            if (0.3f*ColorInterpolator > 1)
            {
               // Destroy(pauseButton.transform.GetChild(0).transform.gameObject);
            }
        }
    }

    public void skipIntro()
    {
        map.SetActive(false);
        MainCamera.transform.SetParent(null);
        MainCamera.transform.SetPositionAndRotation(new Vector3(0, 0, -10), Quaternion.identity);
        MainCamera.GetComponent<Camera>().fieldOfView = 60;
        MainCamera.SetActive(true);
        introSceneFinished = true;
        skipButton.SetActive(false);
        AudioManager.instance.PlaySound("introFinished");
        introSceneTrucks.SetActive(false);
    }

    public void Pause()
    {

            Time.timeScale = 0;
            gamePaused = true;
    }
    

    public void Resume()
    {
        Time.timeScale = 1;
        gamePaused = false;
    }
}
